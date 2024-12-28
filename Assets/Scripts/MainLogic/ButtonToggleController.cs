using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggleController : MonoBehaviour
{
    [SerializeField] private WordController wordController;
    private int lastClickedButtonIndex = -1;   // 前回クリックされたボタンのインデックス.
    private Color defaultColor = Color.white; // ボタンのデフォルトカラー.
    private Color activeColor = Color.gray;  // ボタンがONのときのカラー.

    private void Start()
    {
        Button[] buttons = wordController.buttons;

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(() => OnButtonClick(index));
            ResetButtonColor(index);
        }
    }


    // ボタンクリック時の処理.
    private void OnButtonClick(int index)
    {
        Button buttons = wordController.buttons[index];
        TextMeshProUGUI buttonText = buttons.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        // テキストが空なら処理しない.
        if (string.IsNullOrEmpty(buttonText.text))
        {
            return;
        }

        // 同じボタンを押した場合.
        if (lastClickedButtonIndex == index)
        {
            ToggleButtonState(index); // ON/OFF切り替え.
        }
        else
        {
            // 別のボタンを押した場合.
            if (lastClickedButtonIndex != -1)
            {
                ResetButtonColor(lastClickedButtonIndex); // 前回のボタンをOFF.
            }

            SetButtonColor(index, true); // 今回のボタンをON.
            lastClickedButtonIndex = index; // 更新.
        }
    }

    // ボタンのON/OFF状態を切り替える処理.
    private void ToggleButtonState(int index)
    {
        Image buttonImage = wordController.buttons[index].GetComponent<Image>();
        if (buttonImage.color == activeColor)
        {
            ResetButtonColor(index); // OFFにする.
            lastClickedButtonIndex = -1; // ボタンの選択を解除.
        }
        else
        {
            SetButtonColor(index, true); // ONにする.
            lastClickedButtonIndex = index; // 更新.
        }
    }

    // ボタンの色をデフォルトカラーにリセット (OFF状態).
    private void ResetButtonColor(int index)
    {
        SetButtonColor(index, false);
    }

    // ボタンの色を設定 (ON/OFF).
    private void SetButtonColor(int index, bool isActive)
    {
        Image buttonImage = wordController.buttons[index].GetComponent<Image>();
        buttonImage.color = isActive ? activeColor : defaultColor;
    }
}
