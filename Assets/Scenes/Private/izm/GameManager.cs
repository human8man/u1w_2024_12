using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // リトライボタンが押されて時の処理.
    public void OnRetryButton()
    {
        Scene currenScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currenScene.name);
    }


    // clearフラグ取得、設定プロパティ.
    public bool IsClear
    {
        get { return isClear; }
        set { isClear = value; }
    }
}