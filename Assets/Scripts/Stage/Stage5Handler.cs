using System;
using UnityEngine;

namespace Stage
{
    public class Stage5Handler : MonoBehaviour
    {
        [SerializeField] PlayerController _playerController;

        void Update()
        {
            if (WordController.Instance.inactiveWord == "止まる")
            {
                if (_playerController.NowPlayerState is PlayerController.PlayerMoveState.CannotMove or PlayerController.PlayerMoveState.Normal)
                {
                    _playerController.NowPlayerState = PlayerController.PlayerMoveState.MoveOnlyStopping;
                }
            }
            else if (WordController.Instance.inactiveWord == "移動")
            {
                _playerController.NowPlayerState = PlayerController.PlayerMoveState.CannotMove;
            }
        }
    }
}