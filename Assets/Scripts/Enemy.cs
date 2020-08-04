using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    private NavMeshAgent _agent;
    bool flag = true;

    private Transform _target;
    private Stats _targetStats;


    private State _state = State.Peace;

    private enum State
    {
        Battle,
        Peace
    }


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        start = transform.position;
        end = transform.position;
        end.x += 15f;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(flag)
            {
                _agent.SetDestination(end);
                flag = false;
            }
            else
            {
                _agent.SetDestination(start);
                flag = true;
            }

        }


        if(Input.GetKeyDown(KeyCode.A) && _state==State.Battle)
        {
            Attack(5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _state = State.Battle;
            _target = other.transform;
            _targetStats = _target.GetComponent<Stats>();
        }
    }

    private void Attack(int dmgAmaunt)
    {
        _targetStats.currentHP -= dmgAmaunt;
        Messenger.Broadcast(GameEvent.PLAYER_STATS_CHANGED);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _state = State.Peace;
            _target = null;
            _targetStats = null;
        }
    }
}
