/**
 * @file DeathCauseLog.cs.
 * @brief ボタン選択マネージャー.
 * @author ふぅ.
 * @date 2024/12/29.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectManager : MonoBehaviour
{
    public StageData StageData;
    private SelectButton[] Buttons;

    private bool IsActiveFlg;
    public TMP_Text StageText;

    void Start()
    {
        // すべてのステージボタンを取得
        Buttons = FindObjectsOfType<SelectButton>();

        // ステージデータから解放すべきステージを確認し、ボタンの状態を更新
        StageData.UnlockNextStages();  // 次のステージまで解放

        // ボタンの状態を初期化
        foreach (var Button in Buttons)
        {
            bool IsCleared = StageData.IsStageCleared(Button.StageIndex);
            Button.IsCleared = IsCleared;
            Button.UpdateInteractable();  // ボタンのインタラクションを更新
        }
    }

    public void OnStageSelected(int StageIndex)
    {
        //タイトルを設定.
        StageText.text = $"ステージ{StageIndex}以外ないゲーム";
    }

    public void DeselectAll()
    {
        Debug.Log("すべてのボタンを非選択状態にします");
        foreach (var Button in Buttons)
        {
            Button.Deselect();
        }
    }

    public void HideOtherButton(int StageIndex)
    {
        foreach (var Button in Buttons)
        {
            if (Button.StageIndex != StageIndex)
            {
                //ボタンを非表示.
                Button.gameObject.SetActive(false);
            }
        }
    }

    public bool IsActive
    {
        get { return IsActiveFlg; }
        set { IsActiveFlg = value; }
    }
}
