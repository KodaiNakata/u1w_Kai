using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 爆弾のクラス
/// </summary>
public class Bomb : MonoBehaviour
{
    /// <summary>
    /// 爆破間隔
    /// </summary>
    private const float BLAST_SPAN = 10f;

    /// <summary>
    /// 頂点のy座標
    /// </summary>
    private float TOP_Y = 2f;

    /// <summary>
    /// 爆弾のアニメータ
    /// </summary>
    private Animator bombAnimator;

    /// <summary>
    /// 爆発するまでのタイム
    /// </summary>
    private float time;

    /// <summary>
    /// 爆弾の音源
    /// </summary>
    private AudioSource bombAudioSource;

    /// <summary>
    /// 手の中にあるか
    /// </summary>
    public static bool inHand { get; set; }

    /// <summary>
    /// 爆発するか
    /// </summary>
    public static bool doBlast { get; set; }

    /// <summary>
    /// 開始位置
    /// </summary>
    public static Vector3 startPos { get; set; }

    /// <summary>
    /// 終点位置
    /// </summary>
    public static Vector3 endPos { get; set; }

    /// <summary>
    /// 手から投げたか
    /// </summary>
    public static bool isThrown { get; set; }

    /// <summary>
    /// 衝突した瞬間の処理
    /// </summary>
    /// <param name="collision">衝突したオブジェクト</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーのオブジェクトと衝突したとき
        if (collision.CompareTag("Player"))
        {
            Player.hasBomb = true;
            startPos = collision.transform.position;
            transform.position = startPos;
        }
        // COMのオブジェクトと衝突したとき
        else if (collision.CompareTag("COM"))
        {
            collision.GetComponent<Computer>().hasBomb = true;
            startPos = collision.transform.position;
            transform.position = startPos;
        }
        inHand = true;
        isThrown = false;
    }

    /// <summary>
    /// 衝突から外れた瞬間の処理
    /// </summary>
    /// <param name="collision">衝突したオブジェクト</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // プレイヤーのオブジェクトから衝突が外れたとき
        if (collision.CompareTag("Player"))
        {
            Player.hasBomb = false;
        }
        // COMのオブジェクトから衝突が外れたとき
        else if (collision.CompareTag("COM"))
        {
            collision.GetComponent<Computer>().hasBomb = false;
        }
        inHand = false;
    }

    /// <summary>
    /// 最初のUpdate前に呼び出される関数
    /// </summary>
    void OnEnable()
    {
        time = 0f;
        bombAnimator = GetComponent<Animator>();
        bombAudioSource = GetComponent<AudioSource>();
        inHand = false;
        transform.position = new Vector3(0f, 0f);
        startPos = transform.position;
        endPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        isThrown = true;
        doBlast = false;
    }

    /// <summary>
    /// 1フレームごとに呼ばれる関数
    /// </summary>
    void Update()
    {
        CountBlast();
        IsThrown();
    }


    /// <summary>
    /// 爆破をカウントする
    /// </summary>
    private void CountBlast()
    {
        // 手の中で爆発していないとき
        if (inHand && !doBlast)
        {
            time += Time.deltaTime;
            // 爆破の感覚が過ぎたとき
            if (time > BLAST_SPAN)
            {
                // 次の爆破アニメーションへ移行
                bombAnimator.SetInteger("blastStage", bombAnimator.GetInteger("blastStage") + 1);
                time = 0f;
            }
            // 爆破アニメーションが最終段階のとき
            if(bombAnimator.GetInteger("blastStage") >= 4)
            {
                doBlast = true;
                isThrown = false;
            }
        }
    }

    /// <summary>
    /// 爆弾が投げられた時の処理
    /// </summary>
    private void IsThrown()
    {
        if (isThrown && !doBlast && bombAnimator.GetInteger("blastStage") < 4)
        {
            float xSpeed = 0f;
            if (startPos.x < endPos.x)
            {
                xSpeed = 0.1f;
            }
            else
            {
                xSpeed = -0.1f;
            }
            // 始点と終点の中間のx座標を求める
            float halfX = (startPos.x + endPos.x) / 2f;

            // 放物線の式を求めて画面に反映させる
            Vector3 position = transform.position;
            position.x += xSpeed;
            float a = 0f;
            if (startPos.x == halfX)
            {
                position.y = TOP_Y;
            }
            else
            {
                a = (startPos.y - TOP_Y) / Mathf.Pow((startPos.x - halfX), 2f);
                position.y = a * Mathf.Pow((position.x - halfX), 2f) + TOP_Y;
            }
            transform.position = position;
        }
    }

    /// <summary>
    /// 爆破のSEを再生する
    /// </summary>
    private void PlayBlastSE()
    {
        bombAudioSource.PlayOneShot(bombAudioSource.clip, SoundManager.instance.seVolume);
    }

    /// <summary>
    /// 爆弾を削除する
    /// </summary>
    private void DeleteBomb()
    {
        Destroy(gameObject);
    }
}
