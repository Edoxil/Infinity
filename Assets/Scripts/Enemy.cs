using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Stats))]
public class Enemy : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    private NavMeshAgent _agent;
    private Stats _stats;
    private State _state = State.Peace;


    private float _attackDelay = 1.5f;


    bool flag = true;

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
        _stats = GetComponent<Stats>();
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


        if(_state == State.Battle)
        {
            TakeDamage(1);
        }
    }



    private void TakeDamage(int damage)
    {
        _stats.currentHP -= damage;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _state = State.Battle;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _state = State.Peace;
        }
    }
}
