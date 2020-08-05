using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Interactable : MonoBehaviour
{
    private CharacterStats _stats;
    private Transform _thisTransform;

    private void OnDestroy()
    {
        Messenger<Transform>.Broadcast(GameEvent.TRANSFORM_DESTROYED, _thisTransform);
    }

    void Start()
    {
        _stats = GetComponent<CharacterStats>();
        _thisTransform = GetComponent<Transform>();
    }

    
  
}
