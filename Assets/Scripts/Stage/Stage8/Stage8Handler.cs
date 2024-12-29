using System;
using UnityEngine;

namespace Stage.Stage8
{
    public class Stage8Handler : MonoBehaviour
    {
        Stage8MovingBlockHandler[] _movingBlockHandlers;
        Stage8ClockMovement[] _clockMovements;
        void Start()
        {
            _movingBlockHandlers = GetComponentsInChildren<Stage8MovingBlockHandler>();
            _clockMovements = GetComponentsInChildren<Stage8ClockMovement>();
        }

        void Update()
        {
            bool isMoving = WordController.Instance.inactiveWord != "時";
            foreach (var m in _movingBlockHandlers)
            {
                m.IsMoving = isMoving;
            }
            foreach (var c in _clockMovements)
            {
                c.IsMoving = isMoving;
            }
        }
    }
}