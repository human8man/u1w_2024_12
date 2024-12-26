using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // スプライト.
    public Sprite SpriteIdle;
    public Sprite SpriteUp;
    public Sprite SpriteDown;
    public Sprite SpriteLeft;
    public Sprite SpriteRight;

    private SpriteRenderer SpriteRenderer;

    public Rigidbody2D Rb;
    Vector2 BeforeVel;             //前回の移動.

    private const float Speed = 7.0f;   // 移動スピード.

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        BeforeVel = Rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        // キー入力.
        KeyInput();
    }

    // キー入力.
    void KeyInput()
    {
        // 入力の取得
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");


        // 移動処理
        Vector2 moveDirection = new Vector2(horizontalMove, verticalMove);
        Rb.velocity = moveDirection * Speed;

        // プレイヤーの画像変更.
        ChangeSprite();
    }

    void ChangeSprite()
    {
        Vector2 nowVel = Rb.velocity;

        if (BeforeVel == nowVel)
        {
            SpriteRenderer.sprite = SpriteIdle;     // アイドル状態.
            return;
        }

        if (BeforeVel.x < nowVel.x)
        {
            SpriteRenderer.sprite = SpriteRight;    // 右.
        }
        else if (BeforeVel.x > nowVel.x)
        {
            SpriteRenderer.sprite = SpriteLeft;     // 左.
        }
        else if (BeforeVel.y < nowVel.y)
        {
            SpriteRenderer.sprite = SpriteUp;       // 上.
        }
        else if (BeforeVel.x > nowVel.x)
        {
            SpriteRenderer.sprite = SpriteDown;     // 下.
        }

        BeforeVel = nowVel;
    }
}
