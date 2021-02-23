using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// スタート画面操作用のクラス
/// </summary>
public class StartController : MonoBehaviour
{
    /// <summary>
    /// 音源
    /// </summary>
    [SerializeField]
    private AudioSource audioSource;

    /// <summary>
    /// クリックされたか
    /// </summary>
    private bool isClicked;

    /// <summary>
    /// 最初のUpdate前に呼び出される関数
    /// </summary>
    void Start()
    {
        isClicked = false;
    }

    /// <summary>
    /// 1フレームごとに呼ばれる関数
    /// </summary>
    void Update()
    {
        // 左クリックしたとき
        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            isClicked = true;
            audioSource.PlayOneShot(audioSource.clip);
            StartCoroutine(LoadGameScene());
        }
    }

    /// <summary>
    /// ゲーム画面をロードする
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene("GameScene");
    }
}
