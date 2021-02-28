using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルボタンのクラス
/// </summary>
public class TitleButton : MonoBehaviour
{
    /// <summary>
    /// クリック時の処理
    /// </summary>
    public void OnClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
