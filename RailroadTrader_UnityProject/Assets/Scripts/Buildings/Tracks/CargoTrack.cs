using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTrack : Building
{
    [SerializeField]
    private float timeTilArrival = 45.0f;
    [SerializeField]
    private float passedTime = 0.0f;
    [SerializeField]
    private Animator anim;
    public bool inStation { get; protected set; }

    public class Cargo
    {
        public Resource type;
        public float maxCapacity;
        public float curAmount;
    }

    public HashSet<Cargo> loadedCargo;
    public HashSet<Cargo> orderedCargo;

    protected override void DestroyBuilding()
    {
        throw new System.NotImplementedException();
    }

    protected override void Start()
    {
        //m_Type = BuildingType.TRAINPLATFORM;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (!inStation)
            Timer();

        //if (Input.GetKeyDown("s"))
        //{
        //    Debug.Log("S");
        //    TrainArrived();
        //}
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
        inStation = true;
        //switch to open doors
        //wait a moment
        yield return new WaitForSeconds(0.25f);
    }

    private IEnumerator TrainLeaves()
    {
        inStation = false;
        //wait for the spawn to finish
        //start train exit anim
        //wait for it to finish
        anim.SetTrigger("exit");
        yield return new WaitForSeconds(1.0f);
    }
}
