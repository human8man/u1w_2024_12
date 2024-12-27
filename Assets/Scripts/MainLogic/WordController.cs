using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordController : MonoBehaviour
{
    const int BUTTON_MAX = 3;//ボタン最大数.

    [SerializeField] private List<string> words;// ボタンに表示する単語リスト.
    [SerializeField] private Button[] buttons = new Button[BUTTON_MAX];// ボタン.
    [SerializeField] private Text nonWordText;// ○○が無いゲームと表示するTextUI.

    private string inactiveWord = "○○"; // 非アクティブ状態の単語.
    private string originalText = "○○が無いゲーム";// テキストフォーマット.
    private int lastClickedButtonIndex;// 最後にクリックされたボタン番号.

    private void Start()
    {
        // ボタンにリスナーを追加.
        for(int i = 0;i < BUTTON_MAX; i++ )
        {
            int index = i;
            buttons[index].onClick.AddListener(() => OnClickButton(index));
        }

    }

    private void Update()
    {
        // ボタンのテキストを更新.
        for (int i = 0; i < BUTTON_MAX; i++)
        {
            UpdateButtonText(i, words[i]);
        }

        // 非アクティブ状態の単語をUIに反映.
        nonWordText.text = originalText.Replace("○○", inactiveWord);
    }

   
    // ボタンクリック時の処理.
    private void OnClickButton(int index)
    {
        Debug.Log("押されたボタン:" + index);

        Text currentText = GetButtonText(index);
        Text lastText = GetButtonText(lastClickedButtonIndex);

        StageObject[] stageObjects = FindObjectsOfType<StageObject>(true);

        // 前回と今回のボタンが異なる場合.
        if (currentText.text != lastText.text)
        {
            ToggleObjects(stageObjects, lastText.text, true);       // 前回の単語をアクティブ化.
            ToggleObjects(stageObjects, currentText.text, false);   // 今回の単語を切り替え.
            inactiveWord = currentText.text;
        }
        else
        {
            // 同じボタンが押された場合.
            ToggleObjects(stageObjects, currentText.text, false);
            inactiveWord = stageObjects[0].IsActive ? "○○" : currentText.text;
        }

        // 最後にクリックされたボタン番号を退避.
        lastClickedButtonIndex = index;
    }


    //  オブジェクトの状態切り替え.
    private void ToggleObjects(StageObject[] objects, string word, bool activate)
    {
        foreach (var obj in objects)
        {
            if (obj.IsWordContained(word))
            {
                obj.IsActive = activate ? true : !obj.IsActive;
                obj.gameObject.SetActive(obj.IsActive);
            }
        }
    }


    // ボタンのテキスト更新.
    private void UpdateButtonText(int index, string text)
    {
        Text buttonText = GetButtonText(index);
        buttonText.text = text;
    }


    // ボタンのテキストを取得.
    private Text GetButtonText(int index)
    {
        return buttons[index].transform.GetChild(0).GetComponent<Text>();
    }


    // 単語リストに追加.
    public void AddWord(string word)
    {
        if (words.Contains(word))
        {
            Debug.Log("単語" + word + "は既にリストに存在しているため、追加しませんでした。");
            return;
        }

        // 単語の追加.
        words.Add(word);
        Debug.Log("単語" + word + "をリストに追加しました。");
    }
}
