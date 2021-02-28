using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのクラス
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのアニメータ
    /// </summary>
    private Animator playerAnimator;

    /// <summary>
    /// 爆弾を持っているか
    /// </summary>
    public static bool hasBomb { get; set; }

    /// <summary>
    /// プレイヤーの音源
    /// </summary>
    private AudioSource playerAudioSource;

    /// <summary>
    /// 最初のUpdate前に呼び出される関数
    /// </summary>
    void OnEnable()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        hasBomb = true;
    }

    /// <summary>
    /// 1フレームごとに呼ばれる関数
    /// </summary>
    void Update()
    {
        ThrowBomb();
        BlastHand();
    }

    /// <summary>
    /// 爆弾を投げる
    /// </summary>
    private void ThrowBomb()
    {
        // 爆弾を持っている状態で左クリックした瞬間のとき
        if (hasBomb && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d.transform != null)
            {
                // クリックしたオブジェクトがコンピュータで敗者でないとき
                if (hit2d.transform.gameObject.CompareTag("COM") && !hit2d.transform.GetComponent<Animator>().GetBool("lose"))
                {
                    // 爆弾を投げる
                    playerAnimator.SetBool("doThrowing", true);
                    Bomb.isThrown = true;
                    Bomb.endPos = hit2d.transform.position;
                }
            }
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
            playerAnimator.SetBool("lose", true);
        }
    }

    /// <summary>
    /// 投げるSEを再生する
    /// </summary>
    private void PlayThrowSE()
    {
        playerAudioSource.PlayOneShot(playerAudioSource.clip, SoundManager.instance.seVolume);
    }
}
