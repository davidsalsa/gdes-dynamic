//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Ghost : MonoBehaviour
{
    private enum FSMState
    {
        Hover,
        Chase,
        Attack,
        Dead,
        Respawn,
    }

    private FSMState _currentState;
    private GameObject player;
    private Transform playerTransform;

    public float ghostSpeed;
    public int health;
    public int damage;

    public float hoverFrequency;
    public float hoverAmplitude;

    public float chaseDistance;
    public float attackDistance;
    public float rotationSpeed;
    
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        FSMUpdate();
    }
    
    private void Initialize()
    {
        _currentState = FSMState.Hover;
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        
        if(!playerTransform)
            print("Player does not exist. Please add one with Tag 'Player'");
    }

    private void FSMUpdate()
    {
        // State handling
        switch (_currentState)
        {
            case FSMState.Hover: UpdateHoverState();
                break;
            case FSMState.Chase: UpdateChaseState();
                break;
            case FSMState.Attack: UpdateAttackState();
                break;
            case FSMState.Dead: UpdateDeadState();
                break;
            case FSMState.Respawn: UpdateRespawnState();
                break;
        }
        
        if (health <= 0)
        {
            _currentState = FSMState.Dead;
        }
    }

    private void UpdateHoverState()
    {
        // Apply hover effect
        var tempPos = transform.position;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * hoverFrequency) * hoverAmplitude;
        transform.position = tempPos;
        
        // Get distance between ghost and player
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        
        if (dist < chaseDistance)
        {
            // Stop hovering and move to regular height
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.y);
            
            // Start chase state
            _currentState = FSMState.Chase;
        }
    }
    
    private void UpdateChaseState()
    {
        // Rotate to the player
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Get the distance from player
        // If ghost is near player, change state to attack
        var dist = Vector3.Distance(transform.position, playerTransform.position);
        
        if (dist <= attackDistance)
        {
            _currentState = FSMState.Attack;
        }
        // Go back to hover if player is too far
        else if (dist >= chaseDistance)
        {
            _currentState = FSMState.Hover;
        }

        // Go forward
        transform.Translate(ghostSpeed * Time.deltaTime * Vector3.forward);
    }
    
    private void UpdateAttackState()
    {
        //TODO: Hurt the player with var damage
        _currentState = FSMState.Respawn;
    }
    
    private void UpdateDeadState()
    {
        Destroy(this);
    }

    private void UpdateRespawnState()
    {
        gameObject.SetActive(false);
        transform.position = new Vector3(Random.Range(-6, 6), 0.5f, Random.Range(-6, 6));
    }
}
