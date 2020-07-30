using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Interactable : MonoBehaviour
{
    private Stats _stats;

    private void OnDestroy()
    {
        Messenger<Transform>.Broadcast(GameEvent.TRANSFORM_DESTROYED, gameObject.transform);
    }

    void Start()
    {
        _stats = GetComponent<Stats>();
    }

    
    void Update()
    {
        
    }
}
