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

        [SerializeField] AudioClip _BGMIngame;
        [SerializeField] AudioClip _BGM1;
        [SerializeField] AudioClip _BGMStage3;
        [SerializeField] AudioClip _BGM3;
        [SerializeField] AudioClip _BGM4;
        [SerializeField] AudioClip _BGM5;
        [SerializeField] AudioClip _BGM2;
        [SerializeField] AudioClip _noMusicGame;

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
                    SoundManager.Instance.BGMAudioSource.clip = _BGMIngame;
                    SoundManager.Instance.BGMAudioSource.Play();
                    break;
                case 2:
                    SoundManager.Instance.BGMAudioSource.clip = _BGM1;
                    SoundManager.Instance.BGMAudioSource.Play();
                    break;
                case 3:
                    SoundManager.Instance.BGMAudioSource.clip = _BGMStage3;
                    SoundManager.Instance.BGMAudioSource.Play();
                    break;
                case 4:
                    SoundManager.Instance.BGMAudioSource.clip = _BGM3;
                    SoundManager.Instance.BGMAudioSource.Play();
                    break;
                case 5:
                    SoundManager.Instance.BGMAudioSource.clip = _BGM4;
                    SoundManager.Instance.BGMAudioSource.Play();
                    break;
                case 6:
                    SoundManager.Instance.BGMAudioSource.clip = _BGM5;
                    SoundManager.Instance.BGMAudioSource.Play();
                    break;
                case 7:
                    SoundManager.Instance.BGMAudioSource.clip = _BGM1;
                    SoundManager.Instance.BGMAudioSource.Play();
                    break;
                case 8:
                    SoundManager.Instance.BGMAudioSource.clip = _BGM2;
                    SoundManager.Instance.BGMAudioSource.Play();
                    break;
                case 9:
                    SoundManager.Instance.BGMAudioSource.clip = _BGMIngame;
                    SoundManager.Instance.BGMAudioSource.Play();
                    break;
                case 10:
                    SoundManager.Instance.BGMAudioSource.clip = _noMusicGame;
                    SoundManager.Instance.BGMAudioSource.Play();
                    break;
            }

        }
    }
}