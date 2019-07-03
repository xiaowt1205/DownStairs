using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    [Header("GameManager Status")]
    public bool GMReady;
    public enum Status { Menu, Play, Over };
    public Status GameStatus;

    [Header("GameManager Get Partner")]
    public GroundManager m_GroundManager = null;
    public Player m_Player = null;


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

    public void InitGame()
    {
        if (m_GroundManager != null && m_Player != null)
        {
            GMReady = true;
        }
    }
    void Start()
    {
        m_GroundManager = gameObject.transform.Find("GroundManager").gameObject.GetComponent<GroundManager>();
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        InitGame();
    }


    void Update()
    {
        #region  Now Game Status
        if (GameStatus == Status.Menu)
        {
            Debug.Log("GM : Now in Menu");
        }
        if (GameStatus == Status.Play)
        {
            Debug.Log("GM : Now in Play");
        }
        if (GameStatus == Status.Over)
        {
            Debug.Log("GM : Now in Over");
        }
        #endregion

        #region 作弊鍵
        if (Input.GetKeyDown(KeyCode.P)) {
            GameStatus = Status.Play;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameStatus = Status.Menu;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameStatus = Status.Over;
        }
        #endregion
    }
}
