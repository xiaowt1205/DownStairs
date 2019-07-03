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

    [SerializeField]
    private float MoveSpeed;

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

    }

    void FixedUpdate()
    {
        if (PlayerReady && GameManager.Instance.GameStatus == GameManager.Status.Play)
        {
            m_Rigibody.constraints = RigidbodyConstraints2D.None;
            m_Rigibody.freezeRotation = true;
            //Player Movement
            float h = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
            transform.Translate(h, 0, 0);
        }
        else {
            m_Rigibody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
