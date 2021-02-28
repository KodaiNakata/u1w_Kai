using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コンピュータのクラス
/// </summary>
public class Computer : MonoBehaviour
{
    /// <summary>
    /// 手のオブジェクトのリスト
    /// </summary>
    [SerializeField]
    private List<GameObject> handObjects;

    /// <summary>
    /// コンピュータのアニメーター
    /// </summary>
    private Animator comAnimator;

    /// <summary>
    /// コンピュータの音源
    /// </summary>
    private AudioSource comAudioSource;

    /// <summary>
    /// 爆弾を投げるまでのタイム
    /// </summary>
    private float time;

    /// <summary>
    /// 爆弾を投げるまでの間隔
    /// </summary>
    private float timeSpan;

    /// <summary>
    /// 敗者か
    /// </summary>
    private bool isLoser;

    /// <summary>
    /// 爆弾を持っているか
    /// </summary>
    public bool hasBomb { get; set; }

    /// <summary>
    /// 最初のUpdate前に呼び出される関数
    /// </summary>
    void OnEnable()
    {
        comAudioSource = GetComponent<AudioSource>();
        hasBomb = false;
        comAnimator = GetComponent<Animator>();
        time = 0f;
        timeSpan = 5f;
        isLoser = false;
    }

    /// <summary>
    /// 1フレームごとに呼ばれる関数
    /// </summary>
    void Update()
    {
        BlastHand();
        ThrowBomb();
    }

    /// <summary>
    /// 爆弾を投げる
    /// </summary>
    private void ThrowBomb()
    {
        time += Time.deltaTime;

        // 爆弾を持っている状態の時
        if (!Bomb.isThrown && hasBomb && Bomb.inHand && time >= timeSpan)
        {
            ThrowRandomHand();
            timeSpan = Random.Range(5f, 8f);
            time = 0f;
        }
    }

    /// <summary>
    /// ランダムに他の手へ投げる
    /// </summary>
    /// <returns></returns>
    private void ThrowRandomHand()
    {
        if (!comAnimator.GetBool("lose"))
        {
            int randomHandListNo = Random.Range(0, handObjects.Count);
            do
            {
                if (handObjects[randomHandListNo].CompareTag("Player"))
                {
                    break;
                }
                else if (!handObjects[randomHandListNo].GetComponent<Computer>().isLoser)
                {
                    break;
                }
                randomHandListNo = Random.Range(0, handObjects.Count);
            } while (true);
            Bomb.endPos = handObjects[randomHandListNo].transform.position;
            Bomb.isThrown = true;
            comAnimator.SetBool("doThrowing", true);
        }
    }

    /// <summary>
    /// 手が爆発したか判断する
    /// </summary>
    private void BlastHand()
    {
        // 爆弾を持っていて爆発したとき
        if (hasBomb && Bomb.doBlast)
        {
            // 敗者のアニメーションへ遷移
            comAnimator.SetBool("lose", true);
            GameManager.instance.CreateBomb();
            hasBomb = false;
            isLoser = true;
        }
    }

    /// <summary>
    /// 投げるSEを再生する
    /// </summary>
    private void PlayThrowSE()
    {
        comAudioSource.PlayOneShot(comAudioSource.clip);
    }
}
