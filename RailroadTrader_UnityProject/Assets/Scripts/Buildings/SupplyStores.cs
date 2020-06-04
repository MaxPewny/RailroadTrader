using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyStores : Building
{
    [System.Serializable]
    public class BuildingResource
    {
        public Resource type;
        public float capacity;
        public float amount;
    }

    [System.Serializable]
    public class BuildingVisitorStats
    {
        public Passanger type;
        public int maxCapacity;
        public int capacity;
        public float earningGain;
        public float satisfactionGain;
    }

    public List<BuildingResource> m_Resources;
    public List<BuildingVisitorStats> m_VisitorStats;

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

    }

    public virtual bool GainCustomer(Passanger pPassanger) 
    {
        for (int i = 0; i < m_VisitorStats.Count; i++)
        {
            if (m_VisitorStats[i].type == pPassanger)
            {
                if (m_VisitorStats[i].capacity < m_VisitorStats[i].maxCapacity)
                {
                    ++m_VisitorStats[i].capacity;
                    Resources.Instance.AddCurrency(m_VisitorStats[i].earningGain);
                    return true;
                }
                return false;
            }
        }
        return false;
    }

    protected virtual void ResetCapacity()
    {
        foreach (BuildingVisitorStats stats in m_VisitorStats)
        {
            stats.capacity = 0;
        }
    }
}
