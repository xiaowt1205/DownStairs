using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance = null;
    public GroundCreator m_GroundCreator = null;

    public void InitGame()
    {
        m_GroundCreator = gameObject.transform.Find("GroundCreator").GetComponent<GroundCreator>();
    }
    void Start()
    {
        InitGame();
    }

    void Update()
    {

    }
}
