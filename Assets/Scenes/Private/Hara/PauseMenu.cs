/**
 * @file PauseMenu.cs
 * @brief ポーズメニュー画面
 * @author Hara
 * @date 2024/12/26
 */
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ポーズメニュー画面
/// </summary>
public class PauseMenu : MonoBehaviour
{
    #region 定数

    /// <summary>
    /// ポーズメニュー画面のシーン名
    /// </summary>
    private const string PAUSE_MENU_SCENE_NAME = "PauseMenu";

    /// <summary>
    /// ステージ選択画面のシーン名
    /// </summary>
    private const string STAGE_SELECT_SCENE_NAME = "Hara"; // 仮名

    #endregion

    #region メンバ変数

    /// <summary>
    /// シーンルートオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject sceneRoot = null;

    /// <summary>
    /// 最上位のキャンバス
    /// </summary>
    [SerializeField]
    private Canvas masterCanvas = null;

    /// <summary>
    /// メニューのボタン
    /// </summary>
    [SerializeField]
    private List<Button> menuButtons = null;

    #endregion

    //----------------------------------------------------
    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        menuButtons = new List<Button>();
    }
    //----------------------------------------------------

    //----------------------------------------------------
    /// <summary>
    /// ステージ選択ボタン押下時の処理
    /// </summary>
    public void OnStageSelectButton()
    {
        TransStageSelect();
    }
    //----------------------------------------------------
    
    //----------------------------------------------------
    /// <summary>
    /// ゲームに戻るボタン押下時の処理
    /// </summary>
    public void OnReturnToGameButton()
    {
        CloseOptionMenu();
    }
    //----------------------------------------------------

    //----------------------------------------------------
    /// <summary>
    /// ステージ選択へ遷移
    /// </summary>
    private void TransStageSelect()
    {
        // 仮で直接遷移
        SceneManager.LoadScene(STAGE_SELECT_SCENE_NAME);
    }
    //----------------------------------------------------

    //----------------------------------------------------
    /// <summary>
    /// ポーズメニュー画面を閉じる
    /// </summary>
    private void CloseOptionMenu()
    {
        SceneManager.UnloadSceneAsync(PAUSE_MENU_SCENE_NAME);
    }
    //----------------------------------------------------
}
