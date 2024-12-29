using System.Threading.Tasks;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    // 最初にセットされたCanvasGroup.
    private CanvasGroup CanvasGroup;

    [SerializeField, Tooltip("志望ログ")]
    private GameObject gameObject;
    [SerializeField, Tooltip("フェードインの時間")]
    private float inFadeSpeed = 1.0f;

    private void Start()
    {
        CanvasGroup = gameObject.AddComponent<CanvasGroup>();
        CanvasGroup.alpha = 0f;
    }

    // ボタンをクリックしたときに非同期メソッドを呼び出す
    public async void OnFadeInButtonClick()
    {
        await FadeIn();
    }

    // フェードイン処理.
    public async Task FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < inFadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / inFadeSpeed;

            // 徐々にアルファ値を上げる.
            CanvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            await Task.Yield(); // フレームごとに待機.
        }

        // 最終位置を確定.
        CanvasGroup.alpha = 1f;
    }
}
