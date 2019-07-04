using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [Header("Reload Ground")]
    [SerializeField]
    public Transform m_GroundCreator;
    public bool ReloadGround;
    private Collider2D m_GroundCollider;


    [Header("Ground Type")]
    public GroundType groundType;
    public enum GroundType { Standard, Jump, Roll, Hurt };
    private SpriteRenderer m_SpriteRenderer;

    [Header("彈跳地板")]
    [SerializeField]
    private float PlayerJumpForce;
    [Header("滾輪地板")]
    [SerializeField]
    private bool RightSide;
    [SerializeField]
    private float PlayerSpeedSlow;
    [Header("刺刺地板")]
    [SerializeField]
    private bool HurtOnce;
    [SerializeField]
    private float HurtDamage;

    [Header("Ground Info")]
    [SerializeField]
    private float m_GroundMoveSpeed;
    [SerializeField]
    private GameObject m_player;

    void Start()
    {
        m_SpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_GroundCreator = GameObject.Find("GroundCreator").transform;
        m_GroundCollider = this.gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        #region 地板向上移動
        if (GameManager.Instance.GameStatus == GameManager.Status.Play && !ReloadGround)
        {
            this.gameObject.transform.Translate(transform.up * m_GroundMoveSpeed * Time.deltaTime);
        }
        #endregion
        #region 地板型態
        switch (groundType)
        {
            case GroundType.Standard:
                //TODO : 一般地板
                m_SpriteRenderer.color = Color.white;
                break;
            case GroundType.Jump:
                //TODO : 彈跳地版 → 讓玩家跳起來
                m_SpriteRenderer.color = Color.green;
                break;
            case GroundType.Roll:
                //TODO : 滾輪地版 → 讓玩家加/減速
                m_SpriteRenderer.color = Color.gray;
                break;
            case GroundType.Hurt:
                //TODO : 刺刺地版 → 讓玩家扣血
                m_SpriteRenderer.color = Color.red;
                break;
        }
        #endregion
        ResetGround(ReloadGround);
        #region 作弊鍵
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ReloadGround)
                ReloadGround = false;
            else
                ReloadGround = true;
        }
        #endregion
    }


    public void ResetGround(bool active)
    {
        if (active)
        {
            if (groundType == GroundType.Roll)
            {
                float RollSide = Random.Range(0,2);
                if (RollSide == 1) RightSide = true;
                else if (RollSide == 0) RightSide = false;
                Debug.Log(RollSide);
            }
            if (groundType == GroundType.Hurt)
            {
                if (HurtOnce) HurtOnce = false;
            }
                
            this.gameObject.transform.parent = m_GroundCreator;
            this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            groundType = (GroundType)Random.Range(0, 4);
            StartCoroutine(ReSetGroundCounter(3f));
        }
    }
    IEnumerator ReSetGroundCounter(float RandomTime) {
        yield return new WaitForSeconds(RandomTime);
        this.gameObject.transform.parent = null;
        ReloadGround = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (groundType == GroundType.Standard)
            {
                Player.Instance.PlayerHp += 1;
            }
            if (groundType == GroundType.Jump)
            {
                m_player.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * PlayerJumpForce);
            }
            if (groundType == GroundType.Roll)
            {
                //在OnCollisionStay2D上處理加/減速
            }
            if (groundType == GroundType.Hurt)
            {
                if (!HurtOnce)
                {
                    Player.Instance.PlayerHp -= HurtDamage;
                    HurtOnce = true;
                }
            }
        }
        if (other.gameObject.CompareTag("Scene"))
        {
            ReloadGround = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (groundType == GroundType.Roll)
        {
            if (!RightSide)
                m_player.gameObject.transform.Translate(-1 * Vector3.right * PlayerSpeedSlow * Time.deltaTime);
            else
                m_player.gameObject.transform.Translate(Vector3.right * PlayerSpeedSlow * Time.deltaTime);
        }
    }

}
