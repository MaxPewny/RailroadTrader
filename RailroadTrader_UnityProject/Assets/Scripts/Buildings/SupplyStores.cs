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

    bool guestInShop = false;

    [SerializeField]
    [Tooltip("Seconds til 1 NPC leaves the shop")]
    private float secondsSpendByNPC = 30.0f;
    //[SerializeField]
    private float passedTime;

    private int ResourcesUsedPerCustomer = 1;

    public event System.Action<int> OnSatisfactionGain = delegate { };
    public static event System.Action<SupplyStores> OnStoreNeedsRefill = delegate { };
    public static event System.Action<Passanger, Transform> OnGuestLeft = delegate { };
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
    }

    protected override void Update()
    {
        base.Update();
        if (guestInShop && totalGuestCount > 0)
            RunLeaveTimer();
    }

    protected override void WriteGDValues()
    {        
        m_Ressources.Clear();
        m_VisitorStats.Clear();
        //m_BuildCost = values.BuildCost;
        UpkeepCost = values.UpkeepCost;      
        secondsSpendByNPC = values.SecondsSpendByNPC;

        foreach (BuildingRessource ressi in values.Resources)
        {
            m_Ressources.Add(ressi.DeppCopy());
        }
        foreach (VisitorStats stat in values.VisitorStats)
        {
            m_VisitorStats.Add(stat.DeppCopy());
        }

        foreach(VisitorStats vs in m_VisitorStats)
        {
            vs.curAmount = 0;
        }
        foreach (BuildingRessource r in m_Ressources)
        {
            r.curAmount = r.maxCapacity;
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

                if (totalGuestCount <= 0)
                {
                    passedTime = 0.0f;
                    guestInShop = false;
                }

                OnGuestLeft(guest.type, NPCEnterPoint);
                OnStoreNeedsRefill(this);
                print(guest.type.ToString() + " has left "+ m_Type.ToString());
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
            UseRessources(ResourcesUsedPerCustomer);
            ++visitor.curAmount;
            ++totalGuestCount;
            return true;
        }
        else
            return false;
    }

    public virtual void NPCEnters(Passanger passanger)
    {
        guestInShop = true;
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
            if (ressi.maxCapacity <= 0)
                continue;

            if (ressi.curAmount <= 0)
            {
                print(this.gameObject.name + " has not enough " + ressi.type.ToString()+" to service customers");
                return false;                
            }
        }
        return true;
    }

    private void UseRessources(int amount)
    {
        foreach (BuildingRessource ressi in m_Ressources)
        {
            if (ressi.maxCapacity <= 0)
                continue;

            ressi.curAmount -= amount;
            OnStoreNeedsRefill(this);
        }
    }

    public int NeededAmount(Resource ressiType)
    {
        BuildingRessource ressi = m_Ressources.FirstOrDefault(r => r.type == ressiType);
        return Mathf.Abs(ressi.curAmount - ressi.maxCapacity);
    }

    public bool AddToStock(Resource ressi, int amount)
    {
        foreach(BuildingRessource br in m_Ressources)
        {
            if (br.type == ressi)
            {
                if (br.curAmount < br.maxCapacity)
                {
                    br.curAmount += amount;
                    br.curAmount = br.curAmount > br.maxCapacity ? br.maxCapacity : br.curAmount;
                    print("added " + amount + " to " + ressi.ToString());
                    return true;
                }

                print(ressi.ToString() + " still on max");                
            }
        }
        return false;
    }

    //public void RefillRessources()
    //{
    //    foreach (BuildingRessource ressi in m_Ressources)
    //    {
    //        int amount = ressi.maxCapacity - ressi.curAmount;
    //        print(gameObject.name + " needs " + amount + "x " + ressi.type.ToString());

    //        if (RC.TakeRessourceFromCargo(ressi.type, amount) == amount)
    //        {
    //            ressi.curAmount += amount;
    //            RC.AddToShopRessis(ressi.type, amount);
    //            print("refilled "+amount+" of "+ ressi.type.ToString()+" of "+ gameObject.name);
    //            //RC.SubtractFromShopRessis(ressi.type, ressi.curAmount);
    //        }
    //    }
    //}

    //private void RefillRessources(int amount)
    //{
    //    foreach (BuildingRessource ressi in m_Ressources)
    //    {
    //        int newAmount = RC.TakeRessourceFromCargo(ressi.type, amount);

    //        //if (newAmount < amount)
    //        //{

    //        //    //RC.SubtractFromShopRessis(ressi.type, ressi.curAmount);
    //        //}
    //        //else
    //        //{
    //            ressi.curAmount += amount;
    //            RC.AddToShopRessis(ressi.type, amount);
    //        //}
    //    }
    //}

    /// <summary>
    /// Refills the whole inventory to max capacity
    /// </summary>    
    //public void StockUpInventory()
    //{
    //    foreach (BuildingRessource ressi in m_Ressources)
    //    {
    //        if (ressi.curAmount < ressi.maxCapacity)
    //            RefillRessources(ressi.maxCapacity - ressi.curAmount);
    //    }
    //}

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
