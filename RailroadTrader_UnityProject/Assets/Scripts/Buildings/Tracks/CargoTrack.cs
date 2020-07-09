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
