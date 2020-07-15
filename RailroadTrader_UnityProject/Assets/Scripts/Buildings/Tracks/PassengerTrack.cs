using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerTrack : Building
{
    private NpcMovement NPCs;
    public Passanger m_PassangerType = Passanger.COMMUTER;
    public bool inStation { get; protected set; }

    protected override void DestroyBuilding()
    {
        throw new System.NotImplementedException();
    }

    protected override void Start()
    {
        m_Type = BuildingType.TRAINPLATFORM;
        base.Start();
        NPCs = FindObjectOfType<NpcMovement>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("S");
            TrainArrived();
        }
    }

    private void TrainArrived()
    {
        StartCoroutine(NPCs.SpawnNpcGroup(m_NpcAmount, m_PassangerType, m_NpcMovePoint));        
    }
}
