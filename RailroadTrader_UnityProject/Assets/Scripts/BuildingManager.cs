using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [ReadOnly]
    public List<Building> m_Buildings;

    // Singleton
    protected static BuildingManager _instance = null;
    public static BuildingManager Instance
    {
        get
        {
            if (_instance == null) { _instance = FindObjectOfType<BuildingManager>(); }
            return _instance;
        }
        protected set { _instance = value; }
    }

    void TrainArrived() 
    {
        foreach(TrainPlatform platform in m_Buildings.OfType<TrainPlatform>())
        {
            //StartCoroutine(NpcMovement.Instance.SpawnNpcGroup(platform.m_NpcMovePoint, platform.m_NpcAmount));
        }
    }
}
