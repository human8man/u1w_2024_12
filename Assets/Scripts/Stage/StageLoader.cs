using System;
using UnityEngine;

namespace Stage
{
    public class StageLoader : MonoBehaviour
    {
        // インデックスが表すステージのゲームオブジェクト
        [SerializeField] GameObject[] _stages;
        [SerializeField] Vector2[] _playerBeginningPosition;
        [SerializeField] StageInfo _stageInfo;
        [SerializeField] Transform _playerTransform;

        public StageObject[] NowStageObjects { get; private set; }
        
        void Awake()
        {
            LoadStage(_stageInfo.LoadingStageNum);
        }

        public void LoadStage(int stageNum)
        {
            Debug.Log(stageNum);
            _stages[stageNum].SetActive(true);
            NowStageObjects = _stages[stageNum].GetComponentsInChildren<StageObject>(true);
            _playerTransform.position = _playerBeginningPosition[stageNum];
        }
    }
}