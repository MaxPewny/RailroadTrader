using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerTrack : Building
{
    public PassengerTrackValues values;
    public string TrainName;

    public Passanger m_PassangerType = Passanger.COMMUTER;
    public bool inStation { get; protected set; }

    [SerializeField]
    private float timeTilArrival = 45.0f;
    [SerializeField]
    private float passedArrivalTime = 0.00f;
    [SerializeField]
    private float passedDepartureTime = 0.00f;
    [SerializeField]
    private float timeTilDeparture = 10.0f;
    private float trainAnimTime = 1.0f;
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
        else
            DepartureTimer();

        //if (Input.GetKeyDown("s"))
        //{
        //    Debug.Log("S");
        //    TrainArrived();
        //}
    }

    public float CurArrivalTime()
    {

        if (!inStation)
        {
            //train is gone
            return timeTilArrival - passedArrivalTime;
        }
        else
        {
            //train is there
            return 0.00f;
        }
    }

    public float CurDepartureTime()
    {
        if (!inStation)
        {
            //train is gone
            return 0.00f;
        }
        else 
        {
            //train is there
            return timeTilDeparture - passedDepartureTime;
        }
    }

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
            passedArrivalTime = 0.00f;
            StartCoroutine(TrainArrived());
        }
    }

    private void DepartureTimer()
    {
        passedDepartureTime += Time.deltaTime;
        if (passedDepartureTime >= timeTilDeparture)
        {
            inStation = false;
            passedDepartureTime = 0.00f;
            StartCoroutine(TrainLeaves());
        }
    }

    private IEnumerator TrainArrived()
    {
        //start train enter anim
        //wait for it to finish
        anim.SetTrigger("enter");
        yield return new WaitForSeconds(trainAnimTime + 0.25f);
 
        //switch to open doors
        //wait a moment     
        StartCoroutine(NPCs.SpawnNpcGroup(m_NpcAmount, m_PassangerType, m_NpcMovePoint));
        //yield return new WaitForSeconds(timeTilDeparture);        
        //wait for the spawn to finish
        //start train exit anim
        //wait for it to finish
    }

    private IEnumerator TrainLeaves()
    {
        anim.SetTrigger("exit");
        yield return new WaitForSeconds(1.0f);
        inStation = false;
    }
}
