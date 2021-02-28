using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドの管理クラス
/// </summary>
public class SoundManager
{
    private static SoundManager _instance = new SoundManager();

    /// <summary>
    /// 自クラスのインスタンス
    /// </summary>
    public static SoundManager instance
    {
        get
        {
            return _instance;
        }
    }

    /// <summary>
    /// BGMの音量
    /// </summary>
    public float bgmVolume { get; set; } = 0.5f;

    /// <summary>
    /// SEの音量
    /// </summary>
    public float seVolume { get; set; } = 0.5f;
}
