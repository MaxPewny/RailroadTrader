using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerTrack : Building
{
    public PassengerTrackValues values;

    public Passanger m_PassangerType = Passanger.COMMUTER;
    public bool inStation { get; protected set; }

    [SerializeField]
    private float timeTilArrival = 45.0f;
    [SerializeField]
    private float passedArrivalTime = 0.0f;
    [SerializeField]
    private float passedDepartureTime = 0.0f;
    [SerializeField]
    private float timeTilDeparture = 10.0f;
    private float trainAnimTime = 2.0f;
    [SerializeField]
    private Animator anim;
       

    private NpcMovement NPCs;

    protected override void DestroyBuilding()
    {
        throw new System.NotImplementedException();
    }

    protected override void Start()
    {
        m_Type = BuildingType.PASSENGERTRAIN;
        base.Start();   
        NPCs = FindObjectOfType<NpcMovement>();
        anim = GetComponentInChildren<Animator>();
        inStation = false;
    }

    protected override void Update()
    {
        base.Update();

        if (!inStation)
            ArrivalTimer();
        else if (inStation)
            DepartureTimer();

        //if (Input.GetKeyDown("s"))
        //{
        //    Debug.Log("S");
        //    TrainArrived();
        //}
    }

    //public int CurArrivalTime()
    //{

    
    //}

    //public int CurDepartureTime()
    //{
    //    timeTilDeparture + trainAnimTime
    //}

    protected override void WriteGDValues()
    {
        base.WriteGDValues();
        //m_BuildCost = values.BuildCost;
        UpkeepCost = values.UpkeepCost;
        m_PassangerType = values.PassangerType;
        timeTilArrival = values.TimeTilArrival;
    }

    private void ArrivalTimer()
    {
        passedArrivalTime += Time.deltaTime;
        if (passedArrivalTime >= timeTilArrival)
        {
            inStation = true;
            passedArrivalTime = 0.0f;
            StartCoroutine(TrainArrived());
        }
    }

    private void DepartureTimer()
    {
        passedDepartureTime += Time.deltaTime;
        if (passedDepartureTime >= timeTilDeparture)
        {
            inStation = false;
            passedArrivalTime = 0.0f;
            StartCoroutine(TrainArrived());
        }
    }

    private IEnumerator TrainArrived()
    {
        //start train enter anim
        //wait for it to finish
        anim.SetTrigger("enter");
        yield return new WaitForSeconds(1.0f);        
        //switch to open doors
        //wait a moment     
        StartCoroutine(NPCs.SpawnNpcGroup(m_NpcAmount, m_PassangerType, m_NpcMovePoint));
        yield return new WaitForSeconds(timeTilDeparture);        
        //wait for the spawn to finish
        //start train exit anim
        //wait for it to finish
        anim.SetTrigger("exit");
        yield return new WaitForSeconds(1.0f);  
        inStation = false;
    }
}
