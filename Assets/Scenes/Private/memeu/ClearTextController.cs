using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class ClearTextController : MonoBehaviour
{
    public GameObject textPrefab;
    public Transform parent;


    [Tooltip("クリア時に表示される文字")]
    public string message = "Congratulations!";
    
    [Tooltip("文字間の幅")]
    public float spacing = 100f;
    
    [Tooltip("フェードアウト開始時間（1が最大の割合）")]
    public float fadeStartTime = 0.7f;
    
    [Tooltip("各文字の落ちきるまでの時間")]
    public float AnimeTimeMax = 0.5f;
    
    [Tooltip("文字ごとの遅延時間")]
    public float TextDelay= 0.05f;



    public void Initialize()
    {
        // 初期位置を画面上部に.
        Vector3 StartPos = parent.position + Vector3.up * Screen.height * 0.5f;  
        
        // 文字数分.
        for (int i = 0; i < message.Length; i++)
        {
            // プレハブを生成して親に設定.
            GameObject textObj = Instantiate(textPrefab, parent);
            TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
            text.text = message[i].ToString();

            // RectTransformで調整するために必要.
            RectTransform rectTransform = text.GetComponent<RectTransform>();

            // 横並びに配置(ここで文字間隔を反映).
            rectTransform.anchoredPosition = new Vector2(i * spacing, 6000);

            // 各文字の初期位置、終了位置を設定.
            Vector3 start = StartPos + Vector3.right * i * spacing;
            Vector3 end = start - Vector3.up * Screen.height * 0.5f;

            // アニメーション開始.
            StartCoroutine(AnimateText(textObj.transform, text, start, end, i * TextDelay));
        }
    }


    // 文字のアニメーション.
    System.Collections.IEnumerator AnimateText(
        Transform textdata, TextMeshProUGUI text, Vector3 start, Vector3 end, float delay)
    {
        // 指定された遅延時間だけ待機.
        yield return new WaitForSeconds(delay);

        // 初期化.
        float time = 0f;
        float alpha = 0f;

        while (time < AnimeTimeMax)
        {
            float t = time / AnimeTimeMax;
            textdata.position = Vector3.Lerp(start, end, t);
            
            // 経過時間がフェード開始の割合に達していた場合.
            if (fadeStartTime <= t / AnimeTimeMax)
            {
                // 時間経過の割合とフェード開始の割合を引く.
                alpha = t / AnimeTimeMax - fadeStartTime;
            }

            text.alpha = alpha;
            time += Time.deltaTime;
            yield return null;
        }
        textdata.position = end;
        text.alpha = 1f;
    }
}
