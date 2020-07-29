﻿using System.Collections;
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
    //[SerializeField]
    private float passedTime;

    public event System.Action<int> OnSatisfactionGain = delegate { };
    protected override void DestroyBuilding()
    {
        throw new System.NotImplementedException();
    }

    protected override void Start()
    {
        base.Start();
        FindObjectOfType<SatisfactionController>().ShopBuild(this);

        foreach (VisitorStats vs in m_VisitorStats)
        {
            vs.building = m_Type;
        }
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
        FC.AddShopIncome(visitor.type, visitor.earningGain);
        //FC.UpdateMonthlyRevenue(visitor.earningGain);
        OnSatisfactionGain(visitor.satisfactionGain);
        ManageRessources(1);
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

    private void ManageRessources(int amount)
    {
        UseRessources(amount);
        RefillRessources(amount);
    }

    private void UseRessources(int amount = 1)
    {
        foreach (BuildingRessource ressi in m_Ressources)
        {
            ressi.curAmount -= 1;
            RC.SubtractFromShopRessis(ressi.type, amount);
        }
    }

    private void RefillRessources(int amount)
    {
        foreach (BuildingRessource ressi in m_Ressources)
        {
            if (RC.TakeRessourceFromCargo(ressi.type, amount) == amount)
            {
                ressi.curAmount += amount;
                //RC.SubtractFromShopRessis(ressi.type, ressi.curAmount);
            }
        }
    }

    /// <summary>
    /// Refills the whole inventory to max capacity
    /// </summary>    
    public void StockUpInventory()
    {
        foreach (BuildingRessource ressi in m_Ressources)
        {
            if (ressi.curAmount < ressi.maxCapacity)
                RefillRessources(ressi.maxCapacity - ressi.curAmount);
        }
    }



    protected virtual void ResetCapacity()
    {
        foreach (VisitorStats stats in m_VisitorStats)
        {
            stats.curAmount = 0;
        }
    }

    public override int CurGuestCountOf(Passanger pType)
    {
        foreach(VisitorStats vs in m_VisitorStats)
        {
            if (vs.type == pType)
            {
                return vs.curAmount;
            }
        }
        return 0;
    }

    public override int CurTotalGuests()
    {
        int count = 0;
        foreach (VisitorStats vs in m_VisitorStats)
        {
            count += vs.curAmount;
        }
        return count;
    }
}
