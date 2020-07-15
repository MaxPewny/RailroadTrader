using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTrack : Building
{
    public class Cargo
    {
        public Resource type;
        public float maxCapacity;
        public float curAmount;
    }

    public HashSet<Cargo> loadedCargo;
    public HashSet<Cargo> orderedCargo;

    public bool inStation { get; protected set; }

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

    }
}
