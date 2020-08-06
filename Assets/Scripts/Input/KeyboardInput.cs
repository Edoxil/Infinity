using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KeyboardInput : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           
            Messenger.Broadcast(GameEvent.TARGET_UNSELECTED);
        }

      
    }
}
