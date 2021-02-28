using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 音量設定のクラス
/// </summary>
public class VolumeSetting : MonoBehaviour
{
    void OnEnable()
    {
        if (gameObject.name == "BGMSlider")
        {
            GetComponent<Slider>().value = SoundManager.instance.bgmVolume;
        }
        else if (gameObject.name == "SESlider")
        {
            GetComponent<Slider>().value = SoundManager.instance.seVolume;
        }
    }
    /// <summary>
    /// 値変更時の処理
    /// </summary>
    /// <param name="slider">スライダー</param>
    public void OnValueChanged(Slider slider)
    {
        if (slider.name == "BGMSlider")
        {
            SoundManager.instance.bgmVolume = slider.value;
        }
        else if (slider.name == "SESlider")
        {
            SoundManager.instance.seVolume = slider.value;
        }
    }
}