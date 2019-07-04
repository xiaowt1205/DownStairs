using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCreator : MonoBehaviour
{
    [Header("Ground")]
    public GameObject Ground;

    [Header("GroundCreator Basic Setup")]
    [SerializeField]
    private float GCMoveSpeed;
    [SerializeField]
    private float GCMoveDistance;
    [SerializeField]
    private int GroundNowQuantity;
    [SerializeField]
    private int GroundQuantityLimit;

    [SerializeField]
    [Range(0.5f, 2f)]
    private float SetGroundCDTimer;
    [SerializeField]
    private bool SetGroundCD = false;
    private int RandomSetGround;



    void Start()
    {

    }


    void Update()
    {
        if (GameManager.Instance.GameStatus == GameManager.Status.Play)
        {
            this.gameObject.transform.position = new Vector3(Mathf.PingPong(-GCMoveDistance * GCMoveSpeed * Time.time, GCMoveDistance), 0, 0);
            SetGround4TenPieces();
        }
    }

    #region 一開始先發出10塊地板
    void SetGround4TenPieces()
    {
        if (GroundNowQuantity < GroundQuantityLimit)
        {
            RandomSetGround = Random.Range(0, 3);
            if (RandomSetGround >= 1 && !SetGroundCD)
            {
                GameObject Inst_Ground = Instantiate(Ground, this.gameObject.transform);
                GroundNowQuantity++;
                Inst_Ground.transform.parent = null;
                SetGroundCD = true;
                StartCoroutine(SetGroundCDCounter(SetGroundCDTimer));
            }
        }
    }
    #endregion
    IEnumerator SetGroundCDCounter(float timer)
    {
        yield return new WaitForSeconds(timer);
        SetGroundCD = false;
    }
   
}
