using System;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterStats))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _target;
    private CharacterStats _stats;

    // tmp fields
    public Vector3 start;
    public Vector3 end;
    bool flag = true;



   

    void Awake()
    {
        Messenger<HealthPoints>.AddListener(GameEvent.DIED, Die);
    }

    void OnDestroy()
    {
        Messenger<HealthPoints>.RemoveListener(GameEvent.DIED, Die);
    }
   

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stats = GetComponent<CharacterStats>();

        start = transform.position;
        end = transform.position;
        end.x += 15f;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (flag)
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



    private void Die(HealthPoints healthPoints)
    {
        if(healthPoints==_stats.currentHP)
        {
            Destroy(gameObject);
        }
    }
            
}


