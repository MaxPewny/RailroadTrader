using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [ReadOnly]
    public List<Building> m_Buildings;

    //// Singleton
    //protected static BuildingManager _instance = null;
    //public static BuildingManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null) { _instance = FindObjectOfType<BuildingManager>(); }
    //        return _instance;
    //    }
    //    protected set { _instance = value; }
    //}

    void TrainArrived() 
    {
        foreach(PassengerTrack platform in m_Buildings.OfType<PassengerTrack>())
        {
            //StartCoroutine(NpcMovement.Instance.SpawnNpcGroup(platform.m_NpcMovePoint, platform.m_NpcAmount));
        }
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
}
