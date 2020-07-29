using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KeyboardInput : MonoBehaviour
{
    public NavMeshAgent _agent;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           
            Messenger.Broadcast(GameEvent.TARGET_UNSELECTED);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _agent.stoppingDistance = 0f;
            _agent.Stop(true);
            _agent.Resume();
        }
    }
}
