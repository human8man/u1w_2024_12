using System.Collections;
using UI;
using UnityEngine;

namespace Stage
{
    public class StageClearHandler : MonoBehaviour
    {
        [SerializeField] ClearTextController _clearTextController;
        [SerializeField] PlayerController _playerController;
        public void OnStageClear()
        {
            StartCoroutine(OnStageClearCo());
        }

        IEnumerator OnStageClearCo()
        {
            _clearTextController.Initialize();
            _playerController.NowPlayerState = PlayerController.PlayerMoveState.GameEnd;
            yield return new WaitForSeconds(2);
            FadeSystem.Instance.LoadScene("StageSelect");
        }
    }
}