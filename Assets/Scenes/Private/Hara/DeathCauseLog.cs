/**
 * @file DeathCauseLog.cs
 * @brief 死因記録
 * @author Hara
 * @date 2024/12/28
 */
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 死因記録
/// </summary>
public class DeathCauseLog : MonoBehaviour
{
    #region メンバ変数
    
    /// <summary>
    /// 死因履歴
    /// </summary>
    private List<string> m_death_cause_history = null;

    #endregion
    
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Awake()
    {
        m_death_cause_history = new List<string>();
    }

    /// <summary>
    /// 死因を記録 
    /// </summary>
    public void RecordDeathCause(string deathCause)
    {
        m_death_cause_history.Add(deathCause);
    }

    /// <summary>
    /// 最後の死因を取得
    /// </summary>
    public string GetLastDeathCause()
    {
        if (m_death_cause_history.Count == 0)
        {
            return "";
        }
        return m_death_cause_history.Last();
    }

    /// <summary>
    /// 死因履歴をクリア
    /// </summary>
    public void ClearDeathCauseHistory()
    {
        m_death_cause_history.Clear();
    }
}
