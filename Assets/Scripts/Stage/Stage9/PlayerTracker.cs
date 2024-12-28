using System;
using UnityEngine;

namespace Stage
{
    public class PlayerTracker : MonoBehaviour
    {
        [SerializeField] Transform _playerTransform;

        void Update()
        {
            transform.position = _playerTransform.position;
        }
    }
}