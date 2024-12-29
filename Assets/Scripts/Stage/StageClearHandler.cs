using System.Collections;
using UI;
using UnityEngine;

namespace Stage
{
    public class StageClearHandler : MonoBehaviour
    {
        [SerializeField] ClearTextController _clearTextController;
        [SerializeField] PlayerController _playerController;
        [SerializeField] StageInfo _info;
        public void OnStageClear()
        {
            StartCoroutine(OnStageClearCo());
        }

        IEnumerator OnStageClearCo()
        {
            _clearTextController.Initialize();
            _playerController.NowPlayerState = PlayerController.PlayerMoveState.GameEnd;
            yield return new WaitForSeconds(2);
            if (_info.LoadingStageNum == 10)
            {
                _info.LoadingStageNum = 11;
                FadeSystem.Instance.LoadScene("Game");
            }
            else
            {
                FadeSystem.Instance.LoadScene("StageSelect");   
            }
        }
    }
}