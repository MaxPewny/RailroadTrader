using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPlatform : Building
{
    public Passanger m_PassangerType = Passanger.COMMUTER;

    protected override void DestroyBuilding()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Type = BuildingType.TRAINPLATFORM;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
