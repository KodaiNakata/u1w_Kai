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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d.collider == null)
            {
                isClicked = true;
                audioSource.PlayOneShot(audioSource.clip, SoundManager.instance.seVolume);
                StartCoroutine(LoadGameScene());
            }
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
