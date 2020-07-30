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
    private float passedTime = 0.0f;
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
        
        if(!inStation)
            Timer();

        //if (Input.GetKeyDown("s"))
        //{
        //    Debug.Log("S");
        //    TrainArrived();
        //}
    }
    protected override void WriteGDValues()
    {
        base.WriteGDValues();
        m_BuildCost = values.BuildCost;
        UpkeepCost = values.UpkeepCost;
        m_PassangerType = values.PassangerType;
        timeTilArrival = values.TimeTilArrival;
    }

    private void Timer()
    {
        passedTime += Time.deltaTime;
        if (passedTime >= timeTilArrival)
        {
            inStation = true;
            StartCoroutine(TrainArrived());
            passedTime = 0.0f;
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
        yield return new WaitForSeconds(0.25f);        
        StartCoroutine(NPCs.SpawnNpcGroup(m_NpcAmount, m_PassangerType, m_NpcMovePoint));
        yield return new WaitForSeconds(0.5f);        
        //wait for the spawn to finish
        //start train exit anim
        //wait for it to finish
        anim.SetTrigger("exit");
        yield return new WaitForSeconds(1.0f);  
        inStation = false;
    }
}
