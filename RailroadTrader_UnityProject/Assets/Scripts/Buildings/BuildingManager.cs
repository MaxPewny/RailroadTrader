using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [ReadOnly]
    public List<Building> m_Buildings;

    public static event System.Action<int> OnUpkeepDue = delegate { };

    private void Start()
    {
        TimeController.OnDayEnd += PayUpkeepCosts;
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
            sum += b.UpkeepCost;
        }
        print("pay upkeep for this day: "+sum);
        return sum;
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
