using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageObject : MonoBehaviour
{
   [System.Serializable]
    private struct WordList
    {
        public string mainWord;// オブえジェクトのメインとなる単語.
        public List<string> containedWords;// オブジェクト内に含まれる単語.
    }

    [SerializeField, Tooltip("単語リスト")]
    private WordList wordList;

    [SerializeField,Tooltip("触れたら死ぬかの設定")]
    private bool isDanger;

    private bool isActive = true; // オブジェクトがアクティブかどうか

    [SerializeField,Tooltip("テキストのプレハブ")]
    private Text textPrefab;                // テキストオブジェクトを生成するためのPrefab.
    private Text mainWordTextInstance;      // メイン単語のテキストインスタンス.
    private Text wordsCountTextInstance;    // 含まれている単語の数を表示するテキストインスタンス.


    void Start()
    {
        // シーン内のCanvasを取得.
        var _canvas = FindObjectOfType<Canvas>().transform.Find("ObjectWord");

        // ワールド座標をスクリーン座標に変換.
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);


        // メイン単語テキストと単語数テキストのインスタンス生成.
        mainWordTextInstance = Instantiate(textPrefab, screenPosition, Quaternion.identity);
        wordsCountTextInstance = Instantiate(textPrefab, screenPosition, Quaternion.identity);


        // テキストの親をCanvasに設定.
        mainWordTextInstance.transform.SetParent(_canvas.transform);
        wordsCountTextInstance.transform.SetParent(_canvas.transform);


        // テキスト内容を設定.
        mainWordTextInstance.text = wordList.mainWord;
        wordsCountTextInstance.text = wordList.containedWords.Count.ToString();


        // オブジェクトに含まれる単語数テキストの位置を設定.
        SetWordCountTextPosition();
    }


    // オブジェクトに含まれる単語数テキストの位置を設定.
    private void SetWordCountTextPosition()
    {
        var sRenderer = GetComponent<SpriteRenderer>();
        Vector3 size = sRenderer.bounds.size;


        // オブジェクトの左上の位置を計算.
        Vector3 topLeftWorldPosition    = transform.position + new Vector3(-size.x * 0.5f, size.y * 0.5f, 0.0f);
        Vector3 topLeftScreenPosition   = Camera.main.WorldToScreenPoint(topLeftWorldPosition);

        // 単語数テキストの位置を更新.
        wordsCountTextInstance.transform.position = topLeftScreenPosition;
    }


    // 指定した単語が含まれているか判定.
    public bool IsWordContained(string targetWord)
    {
        return wordList.containedWords.Contains(targetWord);
    }


    // オブジェクトのアクティブ状態を設定・取得するプロパティ.
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
}
