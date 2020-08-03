using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SupplyStores : Building
{
    //[System.Serializable]
    //public class BuildingRessource
    //{
    //    public Resource type;
    //    public int maxCapacity;
    //    public int curAmount;

    //    public BuildingRessource(Resource type, int maxCapacity)
    //    {
    //        this.type = type;
    //        this.maxCapacity = maxCapacity;
    //    }
    //}
    public SupplystoreValues values;

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

        //foreach (VisitorStats vs in m_VisitorStats)
        //{
        //    vs.building = m_Type;
        //}
        foreach(BuildingRessource r in m_Ressources)
        {
            r.curAmount = r.maxCapacity;
            RC.AddToShopRessis(r.type, r.curAmount);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (totalGuestCount > 0)
            RunLeaveTimer();
    }

    protected override void WriteGDValues()
    {
        //m_BuildCost = values.BuildCost;
        UpkeepCost = values.UpkeepCost;
        m_Ressources = values.Resources;
        m_VisitorStats = values.VisitorStats;
        secondsSpendByNPC = values.SecondsSpendByNPC;

        foreach(VisitorStats vs in m_VisitorStats)
        {
            vs.curAmount = 0;
        }
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
            ManageRessources(1);
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
    }

    private VisitorStats VisitorStats(Passanger pType)
    {
       return m_VisitorStats.First(m_VisitorStats => m_VisitorStats.type == pType);
    }

    private bool HasRessources()
    {        
        foreach (BuildingRessource ressi in m_Ressources)
        {
            if (ressi.curAmount < 1)
            {
                print(this.gameObject.name + " has not enough " + ressi.type.ToString()+" to service customers");
                return false;                
            }
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
    public void RefillRessources()
    {
        foreach (BuildingRessource ressi in m_Ressources)
        {
            int amount = ressi.maxCapacity - ressi.curAmount;
            print(gameObject.name + " needs " + amount + "x " + ressi.type.ToString());

            if (RC.TakeRessourceFromCargo(ressi.type, amount) == amount)
            {
                ressi.curAmount += amount;
                RC.AddToShopRessis(ressi.type, amount);
                print("refilled "+amount+" of "+ ressi.type.ToString()+" of "+ gameObject.name);
                //RC.SubtractFromShopRessis(ressi.type, ressi.curAmount);
            }
        }
    }

    private void RefillRessources(int amount)
    {
        foreach (BuildingRessource ressi in m_Ressources)
        {
            if (RC.TakeRessourceFromCargo(ressi.type, amount) == amount)
            {
                ressi.curAmount += amount;
                RC.AddToShopRessis(ressi.type, amount);
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

    public bool ShopStocksThisResource(Resource type)
    {
        foreach(BuildingRessource r in m_Ressources)
        {
            if (r.type == type)
                return true;
        }
        return false;
    }

    public int FoodInStock()
    {
        if (!ShopStocksThisResource(Resource.FOOD))
            return 0;

        return m_Ressources.SingleOrDefault(r => r.type == Resource.FOOD).curAmount;
    }

    public int FoodMaxCapacity()
    {
        if (!ShopStocksThisResource(Resource.FOOD))
            return 0;

        return m_Ressources.SingleOrDefault(r => r.type == Resource.FOOD).maxCapacity;
    }

    public int DrinksInStock()
    {
        if (!ShopStocksThisResource(Resource.BEVERAGE))
            return 0;

        return m_Ressources.SingleOrDefault(r => r.type == Resource.BEVERAGE).curAmount;
    }

    public int DrinksMaxCapacity()
    {
        if (!ShopStocksThisResource(Resource.BEVERAGE))
            return 0;

        return m_Ressources.SingleOrDefault(r => r.type == Resource.BEVERAGE).maxCapacity;
    }

    public int CargoInStock()
    {
        if (!ShopStocksThisResource(Resource.CARGO))
            return 0;

        return m_Ressources.SingleOrDefault(r => r.type == Resource.CARGO).curAmount;
    }

    public int CargoMaxCapacity()
    {
        if (!ShopStocksThisResource(Resource.CARGO))
            return 0;

        return m_Ressources.SingleOrDefault(r => r.type == Resource.CARGO).maxCapacity;
    }
}
