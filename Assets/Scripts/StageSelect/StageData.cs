using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject2/StageData")]
public class StageData : ScriptableObject
{
    public bool[] stageCleared;   // 各ステージのクリア状態
    public int currentStageIndex; // 現在のステージ番号

    // 現在のステージ番号を取得
    public int GetCurrentStage()
    {
        return currentStageIndex;
    }

    // 次のステージ番号を取得
    public int GetNextStage()
    {
        if (currentStageIndex + 1 < stageCleared.Length)
        {
            return currentStageIndex + 1;
        }
        return -1;  // 次のステージが存在しない場合
    }

    // ステージをクリアする
    public void ClearStage(int stageIndex)
    {
        if (stageIndex >= 0 && stageIndex < stageCleared.Length)
        {
            stageCleared[stageIndex] = true;
            currentStageIndex = stageIndex; // 現在のステージを更新
        }
    }

    // ステージがクリアされているか確認
    public bool IsStageCleared(int stageIndex)
    {
        if (stageIndex >= 0 && stageIndex < stageCleared.Length)
        {
            return stageCleared[stageIndex];
        }
        return false;
    }

    // 次のステージまで解放する
    public void UnlockNextStages()
    {
        int nextStage = GetNextStage();
        if (nextStage != -1)
        {
            // 次のステージ以降をすべて解放
            for (int i = 0; i <= nextStage; i++)
            {
                stageCleared[i] = true;
            }
        }
    }
}
