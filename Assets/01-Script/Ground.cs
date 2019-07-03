using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [Header("Ground Type")]
    public GroundType groundType;
    public enum GroundType { Standard, Jump, Roll, Hurt };

    [Header("Ground Info")]
    [SerializeField]
    private float m_GroundMoveSpeed;

    void Start()
    {

    }

    void Update()
    {
        if (GameManager.Instance.GameStatus == GameManager.Status.Play)
        {
            this.gameObject.transform.Translate(transform.up * m_GroundMoveSpeed * Time.deltaTime);
        }

        #region 地板型態
        switch (groundType)
        {
            case GroundType.Standard:
                //TODO : 一般地板

                break;
            case GroundType.Jump:
                //TODO : 彈跳地版 → 讓玩家跳起來

                break;
            case GroundType.Roll:
                //TODO : 滾輪地版 → 讓玩家加/減速

                break;
            case GroundType.Hurt:
                //TODO : 刺刺地版 → 讓玩家扣血
                break;
        }
        #endregion
    }
}
