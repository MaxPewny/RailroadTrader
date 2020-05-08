using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcMovement: MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _npcPrefabs;

    [SerializeField]
    private float _spawnFrequency = 1.0f;

    //[ReadOnly, SerializeField]
    //private List<Vector3> _spawnPoints;

    [ReadOnly, SerializeField]
    private List<Vector3> _buildingTargetPoints;

    [ReadOnly, SerializeField]
    private List<Vector3> _trainTargetPoints;

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
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public virtual void AddSpawnPoint(Vector3 pSpawnPoint) 
    //{
    //    _spawnPoints.Add(pSpawnPoint);
    //}

    public virtual void AddBuildingTargetPoint(Vector3 pTargetPoint)
    {
        _buildingTargetPoints.Add(pTargetPoint);
    }

    public virtual void AddTrainTargetPoint(Vector3 pTargetPoint)
    {
        _trainTargetPoints.Add(pTargetPoint);
    }

    public virtual IEnumerator SpawnNpcGroup(Vector3 pSpawnPoint, int pNpcCount)
    {
        for (int i = 0; i <= pNpcCount; i++)
        {
            Debug.Log("Iterate");
            SpawnNpcAndMove(pSpawnPoint, _buildingTargetPoints[Random.Range(0, _buildingTargetPoints.Count)], _npcPrefabs[Random.Range(0, _npcPrefabs.Count)]);
            yield return new WaitForSeconds(_spawnFrequency);
        }
    }

    protected virtual void SpawnNpcAndMove(Vector3 pSpawnPoint, Vector3 pMoveTarget, GameObject pPrefab) 
    {
        Debug.Log("Spawn");
        GameObject npc = Instantiate(pPrefab,pSpawnPoint, pPrefab.transform.rotation);
        NpcActor actor = npc.GetComponent<NpcActor>();
        actor.SetTarget(pMoveTarget);
        //_npcAgents.Add(agent);

    }

    //public virtual void SpawnNpcGroup(float pNpcCount)
    //{
    //}
}
