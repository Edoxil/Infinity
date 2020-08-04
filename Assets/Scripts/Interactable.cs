using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Interactable : MonoBehaviour
{
    private Stats _stats;
    private Transform _thisTransform;

    private void OnDestroy()
    {
        Messenger<Transform>.Broadcast(GameEvent.TRANSFORM_DESTROYED, _thisTransform);
    }

    void Start()
    {
        _stats = GetComponent<Stats>();
        _thisTransform = GetComponent<Transform>();
    }

    
  
}
