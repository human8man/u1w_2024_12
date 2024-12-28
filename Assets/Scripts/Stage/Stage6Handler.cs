using System;
using UnityEngine;

namespace Stage
{
    public class Stage6Handler : MonoBehaviour
    {
        [SerializeField] GameObject _playerObject;

        void Update()
        {
            _playerObject.SetActive(WordController.Instance.inactiveWord != "自分");
        }
    }
}