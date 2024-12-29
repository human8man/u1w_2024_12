using System;
using UnityEngine;

namespace Stage.Stage8
{
    public class Stage8ClockMovement : MonoBehaviour
    {
        [SerializeField] Transform _clockLong;
        [SerializeField] Transform _clockShort;
        [SerializeField, Header("長針が何秒で一周するか")] float _longLoopTime;
        [SerializeField, Header("短針が何秒で一周するか")] float _shortLoopTime;

        float _nowTime = 0;
        float _velocityShort;
        float _velocityLong;

        public bool IsMoving = true;

        void Start()
        {
            _velocityShort = 360 / _shortLoopTime;
            _velocityLong = 360 / _longLoopTime;
        }

        void Update()
        {
            if (IsMoving) _nowTime += Time.deltaTime;
            Vector3 nowEulerLong = _clockLong.transform.localEulerAngles;
            _clockLong.transform.localEulerAngles = new Vector3(nowEulerLong.x, nowEulerLong.y, -_velocityLong * _nowTime);
            Vector3 nowEulerShort = _clockShort.transform.localEulerAngles;
            _clockShort.transform.localEulerAngles =
                new Vector3(nowEulerShort.x, nowEulerShort.y, -_velocityShort * _nowTime);
        }
    }
}