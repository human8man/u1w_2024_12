using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public  Rigidbody2D Rb;
    public bool         Dead = false;   // 死亡するフラグ（true = 死亡中、false = 生存中）.

    private Animator    Animator;       // アニメーション.
    private Vector2     BeforeVel;      // 前回の移動.

    private const float Speed = 7.0f;   // 移動スピード.


    // スプライト.
    public Sprite SpriteIdle;
    public Sprite SpriteUp;
    public Sprite SpriteDown;
    public Sprite SpriteLeft;
    public Sprite SpriteRight;

    private SpriteRenderer SpriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        Rb              = GetComponent<Rigidbody2D>();
        Animator        = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        BeforeVel = Rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        // 死亡処理.
        if (Dead) { Animator.SetTrigger("Dying"); }

        // キー入力.
        KeyInput();
    }

    // キー入力.
    void KeyInput()
    {
        // 入力の取得
        float horizontalMove    = Input.GetAxis("Horizontal");
        float verticalMove      = Input.GetAxis("Vertical");

        // 移動処理
        Vector2 moveDirection   = new Vector2(horizontalMove, verticalMove);
        Rb.velocity = moveDirection * Speed;

        // 移動による画像変更.
        ChangeSprite();
    }

    // 移動による画像変更.
    void ChangeSprite()
    {
        // 入力の取得
        float horizontalMove    = Input.GetAxis("Horizontal");
        float verticalMove      = Input.GetAxis("Vertical");

        Vector2 nowVel          = Rb.velocity;

        if (BeforeVel == nowVel)
        {
            SpriteRenderer.sprite = SpriteIdle;     // アイドル状態.
        }

        if (verticalMove > 0)
        {
            SpriteRenderer.sprite = SpriteUp;       // 上.
        }
        else if (horizontalMove < 0)
        {
            SpriteRenderer.sprite = SpriteLeft;     // 左.
        }
        else if (verticalMove < 0)
        {
            SpriteRenderer.sprite = SpriteDown;     // 下.
        }
        else if (horizontalMove > 0)
        {
            SpriteRenderer.sprite = SpriteRight;    // 右.
        }

        BeforeVel = nowVel;
    }

    // 死亡するフラグを上げる.
    void SetDeadFlg()
    {
        Dead = true;
    }
}
