using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class StageObject : MonoBehaviour
{
   [System.Serializable]
    private struct WordList
    {
        public string mainWord;             // オブえジェクトのメインとなる単語.
        public List<string> containedWords; // オブジェクト内に含まれる単語.
    }

    [SerializeField, Tooltip("単語リスト")]
    private WordList wordList;

    [SerializeField,Tooltip("触れたら死ぬかの設定")]
    private bool isDanger;

    private bool isActive = true;// オブジェクトがアクティブかどうか.

    // 指定した単語が含まれているか判定.
    public bool IsWordContained(string targetWord)
    {
        return wordList.containedWords.Contains(targetWord);
    }

    // 指定した単語が含まれている単語を取得.
    public List<string> GetContainedWords()
    {
        return wordList.containedWords;
    }

    // オブジェクトのアクティブ状態を設定・取得するプロパティ.
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    // 触れたら死ぬかオブジェクトかの取得するプロパティ.
    public bool IsDanger
    {
        get { return isDanger; }
    }
}
