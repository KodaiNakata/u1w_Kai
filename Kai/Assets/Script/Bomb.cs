using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 爆弾のクラス
/// </summary>
public class Bomb : MonoBehaviour
{
    /// <summary>
    /// 爆破タイム
    /// </summary>
    private const float BLAST_TIME = 6000f;

    /// <summary>
    /// 爆発するまでのタイム
    /// </summary>
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    /// <summary>
    /// 1フレームごとに呼ばれる関数
    /// </summary>
    void Update()
    {
        time += Time.deltaTime;
        if (time > BLAST_TIME)
        {
            //TODO：爆破アニメーションへ

        }
    }
}
