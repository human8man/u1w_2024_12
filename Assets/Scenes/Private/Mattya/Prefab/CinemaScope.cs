using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/**************************************
*    シーン開始時にフェードイン、フェードアウトを行う.
*    担当：淵脇 未来
**************************************/
public class CinemaScope : MonoBehaviour
{
    // 上下の黒帯用のImageオブジェクト.
    [SerializeField, Tooltip("上帯")]
    private Image topBar;
    [SerializeField, Tooltip("下帯")]
    private Image bottomBar;

    private float topPositionY;
    private float bottomPositionY;
    private float imageHeight;

    [SerializeField, Tooltip("フェードインの時間")]
    private float inFadeSpeed = 1.0f;
    [SerializeField, Tooltip("フェードアウトの時間")]
    private float outFadeSpeed = 1.0f;

    // 最初にセットされたCanvasGroup.
    private CanvasGroup topCanvasGroup;
    private CanvasGroup bottomCanvasGroup;

    public Action OnFadeOutEnd { get; set; }
    public Action OnFadeInEnd { get; set;}

    private void Start()
    {
        // 初期化処理.
        Init();
    }

    public async void FadeInAndOut()
    {
        await OnFadeInButtonClick();
        OnFadeOutEnd?.Invoke();
        await OnFadeOutButtonClick();
        OnFadeInEnd?.Invoke();
    }

    public void Init()
    {
        // 高さを取得.
        imageHeight = topBar.gameObject.GetComponent<RectTransform>().rect.height;

        topPositionY = topBar.rectTransform.anchoredPosition.y;
        bottomPositionY = bottomBar.rectTransform.anchoredPosition.y;
    }

    // ボタンをクリックしたときに非同期メソッドを呼び出す
    public async Task OnFadeInButtonClick()
    {
        await FadeIn();
    }

    // ボタンをクリックしたときに非同期メソッドを呼び出す
    public async Task OnFadeOutButtonClick()
    {
        await FadeOut();
    }

    // フェードイン処理.
    public async Task FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < inFadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / inFadeSpeed;

            // 上帯は画面上端に、下帯は画面下端にスライドイン.
            topBar.rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(topPositionY, topPositionY - imageHeight, t));
            bottomBar.rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(bottomPositionY, bottomPositionY + imageHeight, t));

            await Task.Yield(); // フレームごとに待機.
        }
    }

    // フェードアウト処理.
    public async Task FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < outFadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / outFadeSpeed;

            // 上帯は画面外に、下帯は画面外にスライドアウト.
            topBar.rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(topPositionY - imageHeight, topPositionY, t));
            bottomBar.rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(bottomPositionY + imageHeight, bottomPositionY, t));

            await Task.Yield(); // フレームごとに待機.
        }

        // 元の位置に戻す.
        topBar.rectTransform.anchoredPosition = new Vector2(0, topPositionY);
        bottomBar.rectTransform.anchoredPosition = new Vector2(0, bottomPositionY);
    }
}