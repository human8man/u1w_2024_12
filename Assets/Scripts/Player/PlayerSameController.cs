using UnityEngine;

namespace Player
{
    public class PlayerSameController : MonoBehaviour
    {
        public Rigidbody2D Rb;
        
        // スプライト.
        public Sprite SpriteIdle;
        public Sprite SpriteUp;
        public Sprite SpriteDown;
        public Sprite SpriteLeft;
        public Sprite SpriteRight;

        private SpriteRenderer SpriteRenderer;
        [SerializeField] Rigidbody2D _realPlayerRb;

        // Start is called before the first frame update
        void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            // 移動処理
            Rb.velocity = _realPlayerRb.velocity;

            // 移動による画像変更.
            ChangeSprite();
        }

        // 移動による画像変更.
        void ChangeSprite()
        {
            // 入力の取得
            var axis = Rb.velocity;
            float horizontalMove = axis.x;
            float verticalMove = axis.y;

            if (verticalMove > 0)
            {
                SpriteRenderer.sprite = SpriteUp; // 上.
            }
            else if (horizontalMove < 0)
            {
                SpriteRenderer.sprite = SpriteLeft; // 左.
            }
            else if (verticalMove < 0)
            {
                SpriteRenderer.sprite = SpriteDown; // 下.
            }
            else if (horizontalMove > 0)
            {
                SpriteRenderer.sprite = SpriteRight; // 右.
            }
        }
    }
}