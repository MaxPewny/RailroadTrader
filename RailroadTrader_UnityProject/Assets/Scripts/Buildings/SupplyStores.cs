using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SupplyStores : Building
{
    [System.Serializable]
    public class BuildingRessource
    {
        public Resource type;
        public int maxCapacity;
        public int curAmount;
    }

    [System.Serializable]
    public class BuildingVisitorStats
    {
        public Passanger type;
        public int maxCapacity;
        public int curAmount;
        public int earningGain;
        public float satisfactionGain;
    }

    public List<BuildingRessource> m_Ressources;
    public List<BuildingVisitorStats> m_VisitorStats;

    protected override void DestroyBuilding()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        //Initialize();
    }

    void Update()
    {

    }

    public virtual bool GainCustomer(Passanger pPassanger) 
    {
        if (!HasRessources())
            return false;

        BuildingVisitorStats visitor = VisitorStats(pPassanger);

        if (visitor.curAmount < visitor.maxCapacity)
        {
            ++visitor.curAmount;
            FC.AddCurrency(visitor.earningGain);
            RefillRessources(1);
            return true;
        }
        else
            return false;

    }

    private BuildingVisitorStats VisitorStats(Passanger pType)
    {
       return m_VisitorStats.First(m_VisitorStats => m_VisitorStats.type == pType);
    }

    private bool HasRessources()
    {        
        foreach (BuildingRessource ressi in m_Ressources)
        {
            if (ressi.curAmount == 0)
                return false;
        }
        return true;
    }    

    private void RefillRessources(int amount)
    {
        foreach(BuildingRessource ressi in m_Ressources)
        {
            if (RC.TakeRessourceFromCargo(ressi.type, amount) < amount)
            {
                ressi.curAmount += amount;
                RC.SubtractFromShopRessis(ressi.type, ressi.curAmount);
            }
        }
    }

    protected virtual void ResetCapacity()
    {
        foreach (BuildingVisitorStats stats in m_VisitorStats)
        {
            stats.curAmount = 0;
        }
    }

    public virtual bool RessourceAtMaxCapacity()
    {
        return true;
    }
}
