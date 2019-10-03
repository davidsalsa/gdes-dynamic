using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayer : MonoBehaviour
{
    public float hoverFrequency;
    public float hoverAmplitude;
    public float maxDistance;
    
    private Transform _goal;
    private NavMeshAgent _agent;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _goal = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Distance between enemy and player
        float dist = Vector3.Distance(transform.position, _goal.position);
        
        _agent.SetDestination(dist < maxDistance ? _goal.position : transform.position);

        // Apply hover effect
        var tempPos = transform.position;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * hoverFrequency) * hoverAmplitude;
        transform.position = tempPos;
        
        
    }
}
