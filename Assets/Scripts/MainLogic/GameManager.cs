using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private bool isClear = false;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    // リトライボタンが押されて時の処理.
    public void OnRetryButton()
    {
        /*
        Scene currenScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currenScene.name);
        */
        FadeSystem.Instance.LoadScene("Game");
    }


    // clearフラグ取得、設定プロパティ.
    public bool IsClear
    {
        get { return isClear; }
        set { isClear = value; }
    }
}
