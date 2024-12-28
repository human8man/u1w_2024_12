using System;
using UnityEngine;

namespace Stage
{
    [CreateAssetMenu(menuName = "ScriptableObject/Stage")]
    public class StageInfo : ScriptableObject
    {
        [SerializeField] int _loadingStageNum;
        public int LoadingStageNum { get; set; } = 1;

        void OnEnable()
        {
            LoadingStageNum = _loadingStageNum;
        }
    }
}