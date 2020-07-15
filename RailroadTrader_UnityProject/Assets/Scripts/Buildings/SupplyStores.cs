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

    public List<BuildingRessource> m_Ressources;
    public List<VisitorStats> m_VisitorStats;

    [SerializeField]
    [Tooltip("Seconds til 1 NPC leaves the shop")]
    private float secondsSpendByNPC = 30.0f;
    [SerializeField]
    private float passedTime;

    protected override void DestroyBuilding()
    {
        throw new System.NotImplementedException();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (totalGuestCount > 0)
            RunLeaveTimer();
    }

    protected void RunLeaveTimer()
    {     
        passedTime += Time.deltaTime;
        if (passedTime >= secondsSpendByNPC)
        {
            passedTime = 0.0f;
            GuestLeaves();
        }
    }

    //protected virtual bool HasGuests()
    //{
    //    foreach(BuildingVisitorStats guest in m_VisitorStats)
    //    {
    //        if (guest.curAmount > 0)
    //            return true;
    //    }
    //    return false;
    //} 

    protected virtual void GuestLeaves()
    {
        print("a guest leaves");
        foreach (VisitorStats guest in m_VisitorStats)
        {
            if (guest.curAmount > 0)
            {
                --guest.curAmount;
                --totalGuestCount;
                print(guest.type.ToString() + " has left");
                return;
            }
        }
    }

    public virtual bool GainCustomer(Passanger pPassanger) 
    {
        if (!HasRessources())
            return false;

        VisitorStats visitor = VisitorStats(pPassanger);

        if (visitor.curAmount < visitor.maxCapacity)
        {
            ++visitor.curAmount;
            ++totalGuestCount;
            return true;
        }
        else
            return false;
    }

    public virtual void NPCEnters(Passanger passanger)
    {
        VisitorStats visitor = VisitorStats(passanger);
        FC.AddCurrency(visitor.earningGain);
        FC.UpdateMonthlyRevenue(visitor.earningGain);
        RefillRessources(1);
    }

    private VisitorStats VisitorStats(Passanger pType)
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
        foreach (VisitorStats stats in m_VisitorStats)
        {
            stats.curAmount = 0;
        }
    }

    public virtual bool RessourceAtMaxCapacity()
    {
        return true;
    }
}
