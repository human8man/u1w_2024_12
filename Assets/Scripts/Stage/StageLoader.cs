using System;
using TMPro;
using UnityEngine;

namespace Stage
{
    public class StageLoader : MonoBehaviour
    {
        [Serializable]
        class StageInfoTuple
        {
            public GameObject StageGameObject;
            public Vector2 PlayerBeginningPosition;
            public int TermCount;
            public string BeginningDeletedWord = "〇〇";
        }
        
        // インデックスが表すステージの情報
        [SerializeField] StageInfoTuple[] _stages;
        [SerializeField] StageInfo _stageInfo;
        [SerializeField] Transform _playerTransform;
        [SerializeField] WordController _wordController;
        [SerializeField] TextMeshProUGUI _termCountText;

        public StageObject[] NowStageObjects { get; private set; }
        
        void Awake()
        {
            LoadStage(_stageInfo.LoadingStageNum);
        }

        public void LoadStage(int stageNum)
        {
            //プレイヤーの初期位置やステージオブジェクトの取得などを行う
            StageInfoTuple nowStageInfoTuple = _stages[stageNum];
            nowStageInfoTuple.StageGameObject.SetActive(true);
            NowStageObjects = nowStageInfoTuple.StageGameObject.GetComponentsInChildren<StageObject>(true);
            _playerTransform.position = nowStageInfoTuple.PlayerBeginningPosition;
            _wordController.initInactiveWord = nowStageInfoTuple.BeginningDeletedWord;
            _termCountText.text = nowStageInfoTuple.TermCount.ToString();

            switch (stageNum)
            {
                case 1:
                    SoundManager.Instance.PlayLoop("BGM_Ingame");
                    break;
                case 2:
                    SoundManager.Instance.PlayLoop("BGM_1");
                    break;
                case 3:
                    SoundManager.Instance.PlayLoop("BGM_2");
                    break;
                case 4:
                    SoundManager.Instance.PlayLoop("BGM_3");
                    break;
                case 5:
                    SoundManager.Instance.PlayLoop("BGM_4");
                    break;
                case 6:
                    SoundManager.Instance.PlayLoop("BGM_5");
                    break;
                case 7:
                    SoundManager.Instance.PlayLoop("BGM_1");
                    break;
                case 8:
                    SoundManager.Instance.PlayLoop("BGM_2");
                    break;
                case 9:
                    SoundManager.Instance.PlayLoop("BGM_Ingame");
                    break;
                case 10:
                    SoundManager.Instance.PlayLoop("NoMusicGame");
                    break;
            }

        }
    }
}