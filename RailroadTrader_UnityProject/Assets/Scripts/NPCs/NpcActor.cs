using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcActor : MonoBehaviour
{
    [SerializeField]
    float _destinationReachedTreshold = 0.5f;
    [ReadOnly, SerializeField]
    Vector3 _target;
    private NavMeshAgent _agent;
    bool hasTarget;

    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private Passanger type;
    private SupplyStores targetStore;

    // Start is called before the first frame update
    void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTarget)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target);
            if (distanceToTarget < _destinationReachedTreshold)
            {
                EnterShop();
                hasTarget = false;
            }
        }
    }

    private void EnterShop()
    {
        if (targetStore == null)
            Destroy(this.gameObject);
        else
        {
            Destroy(this.gameObject);
            //sprite.SetActive(false);
            targetStore.NPCEnters(type);
        }
    }

    public void SetTarget(Vector3 pTarget, SupplyStores store = null) 
    {
        targetStore = store;
        _target = pTarget;
        _agent.SetDestination(_target);
        hasTarget = true;
    }
}
