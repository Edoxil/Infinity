using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractByMouseClick : MonoBehaviour
{
    private Camera _camera;
    

    void Start()
    {
        _camera = Camera.main;
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                Transform newTarget;
                newTarget = hit.transform;
               
                if (newTarget.TryGetComponent(out Stats stats))
                {
                    Messenger<Transform>.Broadcast(GameEvent.TARGET_SELECTED, newTarget);
                    
                }
            }
        }
    }
}

               

               

