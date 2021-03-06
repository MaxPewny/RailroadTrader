﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GenericValues values;

    private int maxShopAmount = 10;
    private int waxTrackAmount = 6;
    private int upkeepPerCargotrack = 10;
    public List<Building> m_Buildings { get; protected set; }

    public static event System.Action<int> OnUpkeepDue = delegate { };
    public static event System.Action<List<CargoTrack>> OnCargoTrackCountChange = delegate { };
    public static event System.Action<List<SupplyStores>> OnSupplyStoreCountChange = delegate { };
    public static event System.Action<List<SupplyStores>> ListOfAllSupplyStores = delegate { };

    //public static System.Func<List<SupplyStores>> ListOfAllSupplyStores = delegate { return new List<SupplyStores>();};


    private void Awake()
    {
        m_Buildings = new List<Building>();
        WriteGDValues();
    }

    private void WriteGDValues()
    {
        maxShopAmount = values.MaxShopAmount;
        waxTrackAmount = values.MaxTrackAmount;
        upkeepPerCargotrack = values.CargoUpkeepCost;
    }

    private void Start()
    {
        TimeController.OnDayEnd += PayUpkeepCosts;
        RessourceController.OnRefillStores += StoresToRefill;
        Building.OnInitialize += AddBuilding;
        RessourceController.ResourceInStock += AmountInSupplystores;
    }

    private void StoresToRefill()
    {
        ListOfAllSupplyStores(AllSupplyStores());
        //ListOfAllSupplyStores = delegate () { return AllSupplyStores(); };
        //ListOfAllSupplyStores = () => AllSupplyStores();

        //List <SupplyStores> stores = AllSupplyStores();
        //foreach(SupplyStores store in stores)
        //{
        //    store.RefillRessources();
        //}
    }

    private int AmountInSupplystores(Resource ressi)
    {
        List<SupplyStores> allStores = AllSupplyStores();
        int amount = 0;

        foreach(SupplyStores store in allStores)
        {
            foreach (BuildingRessource br in store.m_Ressources)
            {
                if (br.type == ressi)
                    amount += br.curAmount;
                else
                    continue;
            }
        }
        return amount;
    }

    public void AddBuilding(Building building)
    {
        m_Buildings.Add(building);

        if (building.m_Type == BuildingType.CARGOTRAIN)
            OnCargoTrackCountChange(AllCargoTracks());
        else if (building.m_StoreType == StoreType.SUPPLYSTORE)
            OnSupplyStoreCountChange(AllSupplyStores());

    }

    public void RemoveBuilding(Building building)
    {
        m_Buildings.Remove(building);
        if (building.m_Type == BuildingType.CARGOTRAIN)
            OnCargoTrackCountChange(AllCargoTracks());
    }

    public void PayUpkeepCosts()
    {
        OnUpkeepDue(UpkeepAllBuildings());
    }

    private int UpkeepAllBuildings()
    {
        int sum = 0;
        foreach(Building b in m_Buildings)
        {
            if (b.m_Type == BuildingType.CARGOTRAIN)
            {
                sum += upkeepPerCargotrack;
            }
            else
            {
                sum += b.UpkeepCost;
            }
        }
        print("pay upkeep for this day: "+sum);
        return sum;
    }

    public bool CanBuildMoreStores()
    {
        List<SupplyStores> stores = AllSupplyStores();
        if (stores.Count < maxShopAmount)
            return true;
        else
            return false;
    }
    public bool CanBuildMoreTracks()
    {
        if (TrackCount() < waxTrackAmount)
            return true;
        else
            return false;
    }

    public List<SupplyStores> AllSupplyStores()
    {
        List<SupplyStores> ss = new List<SupplyStores>();
        foreach (SupplyStores shop in m_Buildings.OfType<SupplyStores>())
        {
            ss.Add(shop);
        }
        return ss;
    }

    public int TrackCount()
    {
        int count = 0;

        foreach (Building b in m_Buildings)
        {
            if (b.m_Type == BuildingType.CARGOTRAIN || b.m_Type == BuildingType.PASSENGERTRAIN)
                ++count;
        }
        return count;
    }

    public List<CargoTrack> AllCargoTracks()
    {
        List<CargoTrack> cts = new List<CargoTrack>();
        foreach (CargoTrack track in m_Buildings.OfType<CargoTrack>())
        {
            cts.Add(track);
        }
        return cts;
    }

    public int AllBuildings()
    {
        int count = 0;
        foreach(Building b in m_Buildings)
        {
            ++count;
        }
        return count;
    }
}
