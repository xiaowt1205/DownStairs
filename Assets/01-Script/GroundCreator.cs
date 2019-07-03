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
    
   


    void Start()
    {
        
    }

    
    void Update()
    {
        if (GameManager.Instance.GameStatus == GameManager.Status.Play)
        {
            this.gameObject.transform.position =new Vector3(Mathf.PingPong(-GCMoveDistance * GCMoveSpeed * Time.time, GCMoveDistance),0,0);
        }

        SetGround();
    }



    void SetGround() {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Inst_Ground = Instantiate(Ground, this.gameObject.transform);
            Inst_Ground.transform.parent = null;
        }
    }
}
