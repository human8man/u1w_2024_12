using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
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

    [SerializeField,Tooltip("テキストのプレハブ")]
    private Text textPrefab;

    private List<Text> containedWordsCountTexts = new List<Text>();// オブジェクトに含まれる単語数のテキスト.

    void Start()
    {
        // 含まれる単語数のテキストを作成.
        CreateContainedWordsCountTexts();
    }

    void Update()
    {
        // オブジェクトに含まれる単語数テキストの位置を更新.
        UpdateWordCountTextPosition();
    }


    // オブジェクトに含まれる単語数テキストを作成.
    private void CreateContainedWordsCountTexts()
    {
        // Tilemapを取得.
        Tilemap tilemap = GetComponent<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                // 指定座標のタイルを取得.
                TileBase tile = tilemap.GetTile(cellPosition);

                // タイルが無いならスキップ.
                if (tile == null) { continue; }

                // テキストを生成
                Text textInstance = Instantiate(textPrefab, transform.position, Quaternion.identity);
                textInstance.text = wordList.containedWords.Count.ToString();

                // Canvasに追加
                var canvas = FindObjectOfType<Canvas>().transform.Find("ObjectWord");
                textInstance.transform.SetParent(canvas.transform);

                // Listに追加.
                containedWordsCountTexts.Add(textInstance);
            }
        }
    }


    // オブジェクトに含まれる単語数テキストの位置を更新.
    private void UpdateWordCountTextPosition()
    {
        // Tilemapを取得.
        Tilemap tilemap = GetComponent<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;
        int index = 0;// 更新用要素番号.

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                // 指定座標のタイルを取得.
                TileBase tile = tilemap.GetTile(cellPosition);

                // タイルが無いならスキップ.
                if(tile == null) { continue; }

                // タイルの中心位置を取得.
                Vector3 tileCenterWorldPosition = tilemap.GetCellCenterWorld(cellPosition);

                // タイルの左上のワールド座標に調整.
                Vector3 tileTopLeftWorldPosition = tileCenterWorldPosition + new Vector3(-tilemap.cellSize.x * 0.5f, tilemap.cellSize.y * 0.5f, 0);

                // スクリーン座標に変換.
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(tileTopLeftWorldPosition);

                // 座標を適量.
                containedWordsCountTexts[index].transform.position = screenPosition;

                index++;
            }
        }
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
