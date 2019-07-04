using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance = null;

    [Header("Player Basic Component")]
    public bool PlayerReady;
    [SerializeField]
    private Rigidbody2D m_Rigibody;
    [SerializeField]
    private Collider2D m_Collider;

    [Header("Player Basic Setup")]
    [SerializeField]
    private float PlayerMoveSpeed;
    public float PlayerHp;

    [Header("Player Reset Setup")]
    [SerializeField]
    private Vector3 PlayerRespwanPos;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        PlayerRespwanPos = this.gameObject.transform.position;
    }

    public void Init()
    {

        if (m_Rigibody != null && m_Collider != null)
        {
            PlayerReady = true;
        }
    }

    void Start()
    {
        m_Rigibody = this.gameObject.GetComponent<Rigidbody2D>();
        m_Collider = this.gameObject.GetComponent<Collider2D>();
        
        Init();
    }


    void Update()
    {
        if (PlayerHp >= 10) PlayerHp = 10f;

        PlayerDie();
    }

    void FixedUpdate()
    {
        if (PlayerReady && GameManager.Instance.GameStatus == GameManager.Status.Play)
        {
            m_Rigibody.constraints = RigidbodyConstraints2D.None;
            m_Rigibody.freezeRotation = true;
            float h = Input.GetAxis("Horizontal") * PlayerMoveSpeed * Time.deltaTime;
            transform.Translate(h, 0, 0);
        }
        else {
            m_Rigibody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }


    void PlayerDie() {
        if (PlayerHp <= 0) { 
            PlayerHp = 0;
            GameManager.Instance.GameStatus_Over();
            PlayerHp = 10f;
        }
    }

   public void PlayerReset() {
        this.gameObject.transform.position = PlayerRespwanPos;
        PlayerHp = 10f;
    }
}
