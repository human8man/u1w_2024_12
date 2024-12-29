using System;
using UnityEngine;

namespace Stage.Stage8
{
    public class Stage8MovingBlockHandler : MonoBehaviour
    {
        [SerializeField] Vector2 _offset;
        [SerializeField] AnimationCurve _easing;
        [SerializeField] float _loopTime;

        float _nowTime = 0;
        Vector2 _startPosition;

        public bool IsMoving = true;

        void Start()
        {
            _startPosition = transform.position;
        }

        void Update()
        {
            if (IsMoving) _nowTime += Time.deltaTime;
            Vector2 newPosition = _startPosition + _offset * _easing.Evaluate(_nowTime / _loopTime);
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }
}