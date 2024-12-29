using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Stage
{
    public class Stage10Handler : MonoBehaviour
    {
        [SerializeField] Stage10PlayerAirUI _airUI;
        [SerializeField] float _maxAirTime;
        [SerializeField] GameObject _playerObject;
        float _nowTime = 0;

        void Start()
        {
            WordController.Instance.AddWord("水");
            WordController.Instance.AddWord("液体");
        }

        void Update()
        {
            _playerObject.SetActive(WordController.Instance.inactiveWord != "固体");
            if (WordController.Instance.inactiveWord == "気体")
            {
                _airUI.gameObject.SetActive(true);
                _nowTime += Time.deltaTime;
                if (_nowTime > _maxAirTime)
                {
                    Debug.Log("死んだ");
                }
                _airUI.UpdateUI(1 - _nowTime / _maxAirTime);
            }
            else
            {
                _airUI.gameObject.SetActive(false);
                _nowTime = 0;
            }
        }
    }
}