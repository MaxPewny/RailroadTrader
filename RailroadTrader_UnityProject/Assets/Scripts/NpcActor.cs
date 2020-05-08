using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcActor : MonoBehaviour
{
    [SerializeField]
    float _destinationReachedTreshold;
    [SerializeField]
    float _currencyGain;
    [ReadOnly, SerializeField]
    Vector3 _target;
    private NavMeshAgent _agent;

    bool hasTarget;

    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTarget)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target);
            if (distanceToTarget < _destinationReachedTreshold)
            {
                Resources.Instance.AddCurrency(_currencyGain);
                Destroy(this.gameObject);
            }
        }
    }

    public void SetTarget(Vector3 pTarget) 
    {
        _target = pTarget;
        _agent.SetDestination(_target);
        hasTarget = true;
    }
}
