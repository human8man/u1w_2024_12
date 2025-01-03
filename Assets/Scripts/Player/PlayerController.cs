using Stage;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rb;
    public bool Dead = false;   // 死亡するフラグ（true = 死亡中、false = 生存中）.
    
    private Vector2 BeforeVel;      // 前回の移動.

    private const float Speed = 7.0f;   // 移動スピード.

    [SerializeField] StageInfo StageInfo;
    [SerializeField] StageData StageData;

    [SerializeField] GameObject DeadMessege;
    [SerializeField] StageClearHandler _stageClearHandler;

    // スプライト.
    public Sprite SpriteIdle;
    public Sprite SpriteUp;
    public Sprite SpriteDown;
    public Sprite SpriteLeft;
    public Sprite SpriteRight;
    public Sprite[] DeadSprites;

    private SpriteRenderer SpriteRenderer;

    // 単語コントローラー.
    [SerializeField]
    private WordController wordController;

    #region Stage5用の変数

    public enum PlayerMoveState
    {
        Normal,
        CannotMove,
        MoveOnlyMoving,
        MoveOnlyStopping,
        GameEnd
    }

    public PlayerMoveState NowPlayerState { get; set; } = PlayerMoveState.Normal;
    [SerializeField] PlayerMoveState p;
    #endregion

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
        if (NowPlayerState == PlayerMoveState.MoveOnlyMoving)
        {
            if (Rb.velocity == Vector2.zero) NowPlayerState = PlayerMoveState.MoveOnlyStopping;   
        }
        if (NowPlayerState == PlayerMoveState.GameEnd) {Rb.velocity = Vector2.zero; return;}
        // 死亡処理.
        if (Dead)
        {
            StartCoroutine(OnDead());
        }

        // キー入力.
        KeyInput();
    }

    // キー入力.
    void KeyInput()
    {
        // 入力の取得
        var axis = GetAxis();
        float horizontalMove = axis.x;
        float verticalMove = axis.y;

        // 移動処理
        Vector2 moveDirection = new Vector2(horizontalMove, verticalMove);
        Rb.velocity = moveDirection * Speed;

        // 移動による画像変更.
        ChangeSprite();
    }

    // 移動による画像変更.
    void ChangeSprite()
    {
        // 入力の取得
        var axis = GetAxis();
        float horizontalMove = axis.x;
        float verticalMove = axis.y;

        Vector2 nowVel = Rb.velocity;

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

    // 触れた単語を取得する.
    void AddTouchedWord(StageObject obj)
    {
        foreach (string word in obj.GetContainedWords())
        {
            wordController.AddWord(word);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (NowPlayerState == PlayerMoveState.GameEnd) {Rb.velocity = Vector2.zero; return;}
        StageObject stageObject = collision.gameObject.GetComponent<StageObject>();
        if (stageObject != null)
        {
            if (stageObject.IsDanger)
            {
                Debug.Log("死んだ");
                Dead = true;
                if (collision.gameObject.CompareTag("Fire"))
                {
                    SoundManager.Instance.PlaySound("Dead_Burnig");
                }
                else
                {
                    SoundManager.Instance.PlaySound("Dead_Common");
                }
                // TODO:ここに死亡した時の処理を記述.

            }
            // 触れた単語を取得する.
            AddTouchedWord(stageObject);
        }

        if (NowPlayerState == PlayerMoveState.MoveOnlyMoving)
        {
            NowPlayerState = PlayerMoveState.MoveOnlyStopping;
        }
    }

    Vector2 GetAxis()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        switch (WordController.Instance.inactiveWord)
        {
            case "W":
                verticalMove = Mathf.Min(0, verticalMove);
                break;
            case "S":
                verticalMove = Mathf.Max(0, verticalMove);
                break;
            case "A":
                horizontalMove = Mathf.Max(0, horizontalMove);
                break;
            case "D":
                horizontalMove = Mathf.Min(0, horizontalMove);
                break;
        }

        //ステージ5専用処理
        switch (NowPlayerState)
        {
            case PlayerMoveState.CannotMove:
                horizontalMove = 0;
                verticalMove = 0;
                break;
            case PlayerMoveState.MoveOnlyMoving:
                horizontalMove = System.Math.Sign(Rb.velocity.x);
                verticalMove = System.Math.Sign(Rb.velocity.y);
                break;
            case PlayerMoveState.MoveOnlyStopping:
                horizontalMove = 0;
                verticalMove = 0;
                if (Input.GetKey(KeyCode.W))
                {
                    verticalMove = 1;
                    NowPlayerState = PlayerMoveState.MoveOnlyMoving;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    verticalMove = -1;
                    NowPlayerState = PlayerMoveState.MoveOnlyMoving;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    horizontalMove = -1;
                    NowPlayerState = PlayerMoveState.MoveOnlyMoving;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    horizontalMove = 1;
                    NowPlayerState = PlayerMoveState.MoveOnlyMoving;
                }
                break;
        }
        return new Vector2(horizontalMove, verticalMove);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (NowPlayerState == PlayerMoveState.GameEnd) {Rb.velocity = Vector2.zero; return;}
        if (collision.gameObject.CompareTag("Water"))
        {
            SoundManager.Instance.PlaySound("Move_Water");
        }

        if (collision.gameObject.tag == "Goal")
        {
            //SceneManager.LoadScene("StageSelect");
            StageData.ClearStage(StageInfo.LoadingStageNum);
            GameManager.instance.IsClear = true;
            SoundManager.Instance.PlaySound("GetFlag");
            _stageClearHandler.OnStageClear();
        }

        StageObject stageObject = collision.gameObject.GetComponent<StageObject>();
        if (stageObject != null)
        {
            if (stageObject.IsDanger)
            {
                Debug.Log("死んだ");
                SoundManager.Instance.PlaySound("Dead_Burnig");
                // TODO:ここに死亡した時の処理を記述.
                Dead = true;
                if (DeadMessege != null) {
                    DeadMessege.gameObject.SetActive(true);
                    DeadMessege.GetComponent<FadeOut>().OnFadeInButtonClick();
                }
            }
            // 触れた単語を取得する.
            AddTouchedWord(stageObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (NowPlayerState == PlayerMoveState.GameEnd) {Rb.velocity = Vector2.zero; return;}
        if (collision.gameObject.CompareTag("Water"))
        {
            SoundManager.Instance.PlaySound("Move_Water");
        }
    }

    public IEnumerator OnDead()
    {
        Debug.Log("OnDead");
        NowPlayerState = PlayerMoveState.GameEnd;
        SpriteRenderer.sprite = DeadSprites[0];
        yield return new WaitForSeconds(0.1f);
        SpriteRenderer.sprite = DeadSprites[1];
        yield return new WaitForSeconds(0.1f);
        SpriteRenderer.sprite = DeadSprites[2];
        yield return new WaitForSeconds(0.5f);
        FadeSystem.Instance.LoadScene("Game");
    }

    public void OnButtonClick()
    {
        FadeSystem.Instance.LoadScene("StageSelect");
    }
}
