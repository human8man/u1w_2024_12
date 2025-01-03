using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Stage;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WordController : SingletonMonoBehaviour<WordController>
{
    const int BUTTON_MAX = 3; // ボタンの最大数.

    [SerializeField] private List<string> words; // ボタンに表示する単語リスト.
    [SerializeField] private TextMeshProUGUI nonWordText; // 非アクティブ単語をゲーム画面に表示するTextUI.
    [SerializeField] StageLoader _stageLoader;
    [SerializeField] PlayerController _playerController;

    public Button[] buttons = new Button[BUTTON_MAX]; // ボタン.
                                             
    public string initInactiveWord;
    public string inactiveWord; // 非アクティブ単語の初期値.
    private string originalText = "が無いゲーム"; // テキストフォーマット.
    private int lastClickedButtonIndex; // 最後にクリックされたボタンの番号.

    private void Start()
    {
        // ボタンにリスナーを追加.
        for (int i = 0; i < BUTTON_MAX; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(() => OnClickButton(index));
        }

        originalText = initInactiveWord + "が無いゲーム";
        inactiveWord = initInactiveWord;
        StageObject[] stageObjects;
        if (_stageLoader == null)
        {
            stageObjects = FindObjectsOfType<StageObject>();
        }
        else
        {
            stageObjects = _stageLoader.NowStageObjects;   
        }
        ToggleObjects(stageObjects, initInactiveWord, false);
    }

    private void Update()
    {
        // ボタンのテキストを更新.
        for (int i = 0; i < BUTTON_MAX; i++)
        {
            if (i >= words.Count) { break; }
            UpdateButtonText(i, words[i]);
        }
        
        // 非アクティブ単語の情報をUIに反映.
        nonWordText.text = originalText.Replace(initInactiveWord, inactiveWord);
    }

    // ボタンがクリックされた時の処理.
    private void OnClickButton(int index)
    {
        if (_playerController.NowPlayerState == PlayerController.PlayerMoveState.GameEnd) return;
        Debug.Log("クリックされたボタン:" + index);

        TextMeshProUGUI currentText = GetButtonText(index);
        TextMeshProUGUI lastText = GetButtonText(lastClickedButtonIndex);

        // テキストが空なら処理しない.
        if (string.IsNullOrEmpty(currentText.text))
        {
            return;
        }

        StageObject[] stageObjects;
        if (_stageLoader == null)
        {
            stageObjects = FindObjectsOfType<StageObject>();
        }
        else
        {
            stageObjects = _stageLoader.NowStageObjects;   
        }

        // 前回と今回のボタンが異なる場合.
        if (currentText.text != lastText.text)
        {
            ToggleObjects(stageObjects, lastText.text, true);      // 前回の単語をアクティブ化.
            ToggleObjects(stageObjects, currentText.text, false);  // 今回の単語を非アクティブ化.

            inactiveWord = currentText.text;
        }
        else
        {
            // 同じボタンが再びクリックされた場合.
            ToggleObjects(stageObjects, currentText.text, false);
            ToggleObjects(stageObjects, initInactiveWord, false);
            inactiveWord = IsObjectActive(stageObjects, currentText.text) ? initInactiveWord : currentText.text;
        }

        // 最後にクリックされたボタンの番号を更新.
        lastClickedButtonIndex = index;
    }

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
        TextMeshProUGUI buttonText = GetButtonText(index);
        buttonText.text = text;
    }


    // ボタンのテキストを取得.
    private TextMeshProUGUI GetButtonText(int index)
    {
        return buttons[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }


    // 単語リストに追加.
    public void AddWord(string word)
    {
        if (words.Contains(word))
        {
            if(word == "石" || word == "固体" || word == "自分") 
            {
                SoundManager.Instance.PlaySound("Blocked");
            }

            Debug.Log("単語 " + word + " は既にリストに存在するため、追加できません。");
            return;
        }

        // 単語を追加.
        words.Add(word);
        SoundManager.Instance.PlaySound("GetWord");
        Debug.Log("単語 " + word + " をリストに追加しました。");
    }


    // 現在の単語数を取得.
    public int GetWordCount()
    {
        return words.Count;
    }

    // 指定した単語オブジェクトがアクティブか.
    private bool IsObjectActive(StageObject[] objects,string word)
    {
        foreach (var obj in objects)
        {
            if (obj.IsWordContained(word))
            {
                return obj.IsActive;
            }
        }
        return false;
    }
}
