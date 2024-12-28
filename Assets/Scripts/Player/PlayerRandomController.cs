using UnityEngine;

public class PlayerRandomController : MonoBehaviour
{
    public  Rigidbody2D Rb;


    // スプライト.
    public Sprite SpriteIdle;
    public Sprite SpriteUp;
    public Sprite SpriteDown;
    public Sprite SpriteLeft;
    public Sprite SpriteRight;

    private SpriteRenderer SpriteRenderer;

    [SerializeField] float intensity;
    [SerializeField] float Speed;
    
    float randomSeedX;
    float randomSeedY;
    float timeLapsed = 0;
    // Start is called before the first frame update
    void Awake()
    {
        Rb              = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        randomSeedX = Random.Range(0, 256f);
        randomSeedY = Random.Range(0, 256f);
    }

    // Update is called once per frame
    void Update()
    {
        // 移動処理
        Vector2 moveDirection   = RandomWalk();
        Rb.velocity = moveDirection * Speed;

        // 移動による画像変更.
        ChangeSprite();
    }

    Vector2 RandomWalk()
    {
        Vector2 velocity;
        timeLapsed += Time.deltaTime;
        velocity.x = Mathf.PerlinNoise(timeLapsed * intensity, randomSeedX);
        velocity.y = Mathf.PerlinNoise(timeLapsed * intensity, randomSeedY);
        velocity.x -= 0.465f;
        velocity.y -= 0.465f;
        return velocity;
    }

    // 移動による画像変更.
    void ChangeSprite()
    {
        // 入力の取得
        var axis = Rb.velocity;
        float horizontalMove    = axis.x;
        float verticalMove      = axis.y;

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
    }
}
