﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NpcMovement: MonoBehaviour
{
    [System.Serializable]
    public struct MovementProbabilities 
    {
        public Passanger type;
        public float eatProb;
        public float drinkProb;

        public float bakeryProb;
        public float cafeProb;
        public float pubProb;
        public float barProb;
        public float loungeProb;
        public float restaurantProb;
        public float highRestaurantProb;
        
        public float shopProb;
         
        public float bookstoreProb;
        public float flowerstoreProb;
        public float travelInfoProb;
        public float travelAgencyProb;
        public float hairSalonProb;
        public float bankProb;
    }

    public class StoreInfo
    {
        public SupplyStores building;
        public Vector3 position;

        public StoreInfo(SupplyStores building, Vector3 position)
        {
            this.building = building;
            this.position = position;
        }
    }

    [SerializeField]
    private List<GameObject> _npcPrefabs;

    [SerializeField]
    private List<MovementProbabilities> _movementProbabilities;

    [SerializeField]
    private float _spawnFrequency = 0.2f;


    [SerializeField]
    private Transform StationExit;
    [SerializeField]
    private Vector3 _exitPoint { get { return new Vector3(StationExit.position.x, StationExit.position.y, StationExit.position.z); } }

    private BuildingManager _buildingManager;

    public event System.Action<Passanger, int> OnVisitorSpawn = delegate { };

    //[ReadOnly, SerializeField]
    //private List<Vector3> _spawnPoints;

    //[ReadOnly, SerializeField]
    //private List<Vector3> _buildingTargetPoints;
    //
    //[ReadOnly, SerializeField]
    //private List<Vector3> _trainTargetPoints;

    //[ReadOnly, SerializeField]
    //private List<NavMeshAgent> _npcAgents;

    // Singleton
    protected static NpcMovement _instance = null;
    public static NpcMovement Instance
    {
        get
        {
            if (_instance == null) { _instance = FindObjectOfType<NpcMovement>(); }
            return _instance;
        }
        protected set { _instance = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _buildingManager = FindObjectOfType<BuildingManager>();
    }

    //private void TrainArrived()
    //{
    //    foreach (PassengerTrack platform in _buildingManager.m_Buildings.OfType<PassengerTrack>())
    //    {
    //        StartCoroutine(SpawnNpcGroup(platform.m_NpcAmount, platform.m_PassangerType, platform.m_NpcMovePoint));
    //    }
    //}

    public virtual IEnumerator SpawnNpcGroup(int pNpcCount, Passanger pPassanger, Vector3 pSpawnPoint)
    {
        OnVisitorSpawn(pPassanger, pNpcCount);

        for (int i = 0; i < pNpcCount; i++)
        {
           //Debug.Log("Iterate");
            SpawnNpcAndMove(pSpawnPoint, CalculateGoToStore(pPassanger), (int)pPassanger); 
           yield return new WaitForSeconds(_spawnFrequency);
        }
    }

    protected virtual void SpawnNpcAndMove(Vector3 pSpawnPoint, StoreInfo store, int type) 
    {
        GameObject prefab = _npcPrefabs[type];
        GameObject npc = Instantiate(prefab ,pSpawnPoint, prefab.transform.rotation);
        //Debug.Log("Spawn");
        NpcActor actor = npc.GetComponent<NpcActor>();
        actor.SetTarget(store.position, store.building);
    }

    protected virtual StoreInfo CalculateGoToStore(Passanger pPassanger) 
    {
        MovementProbabilities probs = new MovementProbabilities();
        foreach (MovementProbabilities prob in _movementProbabilities)
        {
            if(prob.type == pPassanger) 
            {
                probs = prob;
                break;
            }
        }

        if (UnityEngine.Random.Range(0.0f, 1.0f) <= probs.eatProb)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= probs.bakeryProb)
            {
                foreach (Bakery bakery in _buildingManager.m_Buildings.OfType<Bakery>())
                {
                    if (bakery.GainCustomer(pPassanger))
                    {
                        return new StoreInfo(bakery, bakery.m_NpcMovePoint);
                    }
                }

                return new StoreInfo(null, _exitPoint);

            }
            else 
            {
                foreach (Cafe cafe in _buildingManager.m_Buildings.OfType<Cafe>())
                {
                    if (cafe.GainCustomer(pPassanger))
                    {
                        return new StoreInfo(cafe, cafe.m_NpcMovePoint);
                    }
                }

                return new StoreInfo(null, _exitPoint);
            }
         }
        else if(UnityEngine.Random.Range(0.0f, 1.0f) <= probs.drinkProb)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= probs.pubProb)
            {
                foreach (Pub pub in _buildingManager.m_Buildings.OfType<Pub>())
                {
                    if (pub.GainCustomer(pPassanger))
                    {
                        return new StoreInfo(pub, pub.m_NpcMovePoint);
                    }
                }

                return new StoreInfo(null, _exitPoint);

            }
            else if (UnityEngine.Random.Range(0.0f, 1.0f) <= probs.barProb)
            {
                foreach (Bar bar in _buildingManager.m_Buildings.OfType<Bar>())
                {
                    if (bar.GainCustomer(pPassanger))
                    {
                        return new StoreInfo(bar, bar.m_NpcMovePoint);
                    }
                }

                return new StoreInfo(null, _exitPoint);
            }
            else
            {
                foreach (Lounge lounge in _buildingManager.m_Buildings.OfType<Lounge>())
                {
                    if (lounge.GainCustomer(pPassanger))
                    {
                        return new StoreInfo(lounge, lounge.m_NpcMovePoint);
                    }
                }

                return new StoreInfo(null, _exitPoint);
            }
        }
        else 
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= probs.restaurantProb)
            {
                foreach (Restaurant restaurant in _buildingManager.m_Buildings.OfType<Restaurant>())
                {
                    if (restaurant.GainCustomer(pPassanger))
                    {
                        return new StoreInfo(restaurant, restaurant.m_NpcMovePoint);
                    }
                }

                return new StoreInfo(null, _exitPoint);

            }
            else
            {
                foreach (HighclassRestaurant restaurant in _buildingManager.m_Buildings.OfType<HighclassRestaurant>())
                {
                    if (restaurant.GainCustomer(pPassanger))
                    {
                        return new StoreInfo(restaurant, restaurant.m_NpcMovePoint);
                    }
                }

                return new StoreInfo(null, _exitPoint);
            }
        }
    }

    protected virtual void CalculateGoToShop(Passanger pPassanger)
    {
        //float random = 0.0f;
    }
}
