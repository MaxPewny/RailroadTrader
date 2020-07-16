using System;
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
    private Passanger pType;

    [SerializeField]
    private Animator anim;

    private Vector3 savedDestination;
    
    private SupplyStores targetStore;

    private bool animSet = false;

    public static event System.Action<Passanger> OnLeftStation = delegate { };


    // Start is called before the first frame update
    void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!animSet) {
            anim.SetTrigger(GetAnimDirection());
            animSet = true;
        }

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

    String GetAnimDirection() {
        if (_agent.destination.x > transform.position.x && _agent.destination.z > transform.position.z) {
            if (_agent.destination.z - transform.position.z > _agent.destination.x - transform.position.x) {
                return "Northwest";
            } 
            
            return "Northeast";
        }

        if (_agent.destination.z - transform.position.z > _agent.destination.x - transform.position.x) {
            return "Southwest";
        }

        return "Southeast";
    }

    private void EnterShop()
    {
        if (targetStore == null)
        { 
            Destroy(this.gameObject);
            OnLeftStation(pType);
        }    
        else
        {
            Destroy(this.gameObject);
            //sprite.SetActive(false);
            targetStore.NPCEnters(pType);
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
