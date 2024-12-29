using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Stage
{
    public class Stage10PlayerAirUI : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _renderer;
        [SerializeField] Sprite[] _sprites;
        [SerializeField] Transform _playerTransform;
        [SerializeField] Vector2 _offset;
        public void UpdateUI(float rate)
        {
            if (rate < 0)
            {
                _renderer.sprite = _sprites[0];
            }
            else
            {
                float oneRate = 1 / (float)(_sprites.Length - 1);
                int index = Mathf.CeilToInt(rate / oneRate);
                _renderer.sprite = _sprites[index];   
            }
        }

        void Update()
        {
            Vector2 newPosition = (Vector2)_playerTransform.position + _offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }
}