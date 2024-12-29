using System;
using UnityEngine;

namespace Stage
{
    public class Stage10PlayerAirUI : MonoBehaviour
    {
        [SerializeField] float _maxTime;
        float _nowTime = 0;
        public Action OnAirZero;

        void Update()
        {
            _nowTime += Time.deltaTime;
            if (_nowTime >= _maxTime)
            {
                OnAirZero?.Invoke();
                gameObject.SetActive(false);
            }
            UpdateUI(1 - _nowTime / _maxTime);
        }

        void UpdateUI(float rate)
        {
            
        }
    }
}