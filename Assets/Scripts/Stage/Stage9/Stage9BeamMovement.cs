using System;
using UnityEngine;

namespace Stage
{
    public class Stage9BeamMovement : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField, Header("ビームを消す左端")] float limitX;
        Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            _rb.velocity = new Vector2(-speed, 0);
            if (transform.position.x < limitX)
            {
                Destroy(gameObject);
            }
        }
    }
}