using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPlatform : Building
{
    protected override void DestroyBuilding()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("S");
            TrainArrived();
        }
    }

    protected override void SetNpcMovePoints()
    {
        Debug.Log("SetTarget");
        NpcMovement.Instance.AddTrainTargetPoint(transform.position + m_NpcMovePointOffset);
        //NpcMovement.Instance.AddSpawnPoint(transform.position + m_NpcStartPointOffset);
    }

    void TrainArrived() 
    {
        Debug.Log("Arrived");
        StartCoroutine(NpcMovement.Instance.SpawnNpcGroup(transform.position + m_NpcStartPointOffset, m_NpcAmount));
    }
}
