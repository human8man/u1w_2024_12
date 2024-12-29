/**
 * @file DeathCauseUI.cs
 * @brief 死因ログ表示
 * @author Hara
 * @date 2024/12/29
 */
using TMPro;
using UnityEngine;

public class DeathCauseUI : MonoBehaviour
{
    /// <summary>
    /// 死因テキスト
    /// </summary>
    [SerializeField] GameObject deathCauseText;
    
    /// <summary>
    /// 死因記録データ
    /// </summary>
    [SerializeField] DeathCauseLog deathCauseLogData;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        TextMeshProUGUI text_mesh_pro = deathCauseText.GetComponent<TextMeshProUGUI>();
        text_mesh_pro.text = deathCauseLogData.GetLastDeathCause();
    }

    private void Update()
    {
        TextMeshProUGUI text_mesh_pro = deathCauseText.GetComponent<TextMeshProUGUI>();
        text_mesh_pro.text = deathCauseLogData.GetLastDeathCause();
    }
}
