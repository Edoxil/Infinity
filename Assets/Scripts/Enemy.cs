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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Can attack");
        }
    }
}
