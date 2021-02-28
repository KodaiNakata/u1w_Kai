using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームの管理クラス
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 爆弾のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject bombObj = default;

    /// <summary>
    /// 自クラスのインスタンス
    /// </summary>
    public static GameManager instance { get; private set; }

    /// <summary>
    /// プレイヤーのアニメーター
    /// </summary>
    private Animator playerAnim;

    /// <summary>
    /// コンピュータのアニメーター
    /// </summary>
    private List<Animator> comAnimList;

    /// <summary>
    /// 結果のオブジェクト
    /// </summary>
    private GameObject resultObj;

    /// <summary>
    /// 最初のUpdate前に呼び出される関数
    /// </summary>
    void OnEnable()
    {
        instance = this;
        Instantiate(bombObj);
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        GameObject[] comObj = GameObject.FindGameObjectsWithTag("COM");
        comAnimList = new List<Animator>();

        foreach (GameObject com in comObj)
        {
            comAnimList.Add(com.GetComponent<Animator>());
        }
        resultObj = GameObject.FindGameObjectWithTag("Finish");
        GetComponent<AudioSource>().volume = SoundManager.instance.bgmVolume;
    }

    /// <summary>
    /// 1フレームごとに呼ばれる関数
    /// </summary>
    void Update()
    {
        JudgeLoser();
    }

    /// <summary>
    /// 敗者を判断する
    /// </summary>
    private void JudgeLoser()
    {
        if (playerAnim.GetBool("lose"))
        {
            foreach (Animator animator in comAnimList)
            {
                // 敗者になっていないCOMがいるとき
                if (!animator.GetBool("lose"))
                {
                    // 敗者になっていないCOMだけが勝者アニメーションへ
                    animator.SetBool("win", true);
                }
            }
            FinishGame("あなたの まけ");
        }
        else
        {
            int loserNum = 0;
            foreach (Animator animator in comAnimList)
            {
                // 敗者になったCOMがいるとき
                if (animator.GetBool("lose"))
                {
                    loserNum++;
                }
            }
            // COMがすべて敗者になったとき
            if (loserNum >= comAnimList.Count)
            {
                // プレイヤーのみ勝者アニメーションへ
                playerAnim.SetBool("win", true);
                FinishGame("あなたの かち");
            }
        }
    }

    /// <summary>
    /// 爆弾の作成処理
    /// </summary>
    public void CreateBomb()
    {
        if (!playerAnim.GetBool("lose") && !playerAnim.GetBool("win"))
        {
            Instantiate(bombObj);
        }
    }

    /// <summary>
    /// ゲームを終了する
    /// </summary>
    /// <param name="text">結果表示する文字</param>
    private void FinishGame(string text)
    {
        resultObj.transform.GetChild(0).gameObject.SetActive(true);
        resultObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        StartCoroutine(LoadStartScene());
    }

    /// <summary>
    /// スタート画面をロードする
    /// </summary>
    private IEnumerator LoadStartScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("StartScene");
    }
}
