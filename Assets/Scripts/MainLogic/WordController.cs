using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordController : MonoBehaviour
{
    const int BUTTON_MAX = 3; // ボタンの最大数.

    [SerializeField] private List<string> words; // ボタンに表示する単語リスト.
    [SerializeField] private Button[] buttons = new Button[BUTTON_MAX]; // ボタン.
    [SerializeField] private Text nonWordText; // 非アクティブ単語をゲーム画面に表示するTextUI.

    private string inactiveWord = "なし"; // 非アクティブ単語の初期値.
    private string originalText = "現在の非アクティブ単語:"; // テキストフォーマット.
    private int lastClickedButtonIndex; // 最後にクリックされたボタンの番号.

    private void Start()
    {
        // ボタンにリスナーを追加.
        for (int i = 0; i < BUTTON_MAX; i++)
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

        // 非アクティブ単語の情報をUIに反映.
        nonWordText.text = originalText.Replace("なし", inactiveWord);
    }

    // ボタンがクリックされた時の処理.
    private void OnClickButton(int index)
    {
        Debug.Log("クリックされたボタン:" + index);

        Text currentText = GetButtonText(index);
        Text lastText = GetButtonText(lastClickedButtonIndex);

        StageObject[] stageObjects = FindObjectsOfType<StageObject>(true);

        // 前回と今回のボタンが異なる場合.
        if (currentText.text != lastText.text)
        {
            ToggleObjects(stageObjects, lastText.text, true);       // 前回の単語をアクティブ化.
            ToggleObjects(stageObjects, currentText.text, false);  // 今回の単語を非アクティブ化.
            inactiveWord = currentText.text;
        }
        else
        {
            // 同じボタンが再びクリックされた場合.
            ToggleObjects(stageObjects, currentText.text, false);
            inactiveWord = stageObjects[0].IsActive ? "なし" : currentText.text;
        }

        // 最後にクリックされたボタンの番号を更新.
        lastClickedButtonIndex = index;
    }

    // オブジェクトの状態を切り替え.
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

    // ボタンのテキストを更新.
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
            Debug.Log("単語 " + word + " は既にリストに存在するため、追加できません。");
            return;
        }

        // 単語を追加.
        words.Add(word);
        Debug.Log("単語 " + word + " をリストに追加しました。");
    }
}
