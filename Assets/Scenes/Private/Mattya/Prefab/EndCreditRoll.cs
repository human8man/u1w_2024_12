using System.Net;
using UnityEngine;

public class EndCreditRoll : MonoBehaviour
{
    public float scrollSpeed = 50f; // スクロール速度 (単位: pixels per second)
    private bool isMove = false;

    void Update()
    {
        if (!isMove) { return; }

        // オブジェクトを上方向に移動
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        RectTransform rectTransform = GetComponent<RectTransform>();
        // オブジェクトの底辺が画面中心に来たかチェック
        if (rectTransform.anchoredPosition.y >= 3733f) {
            isMove = false; // 移動を停止
        }
    }

    public void StartMove()
    {
        isMove = true;
    }
}