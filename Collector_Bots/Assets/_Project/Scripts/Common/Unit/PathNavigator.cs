using System;
using UnityEngine;
using UnityEngine.AI;

public class PathNavigator : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    
    private Vector3 _target;

    public bool SetTarget(Vector3 target)
    {
        _target = target;
        return _agent.SetDestination(_target);
    }

    public bool HasReachedTarget()
    {
        if (_agent.pathPending) return false;
        if (_agent.remainingDistance == Mathf.Infinity) return false;
    
        return _agent.remainingDistance <= _agent.stoppingDistance + 1.5f;
    }
}
