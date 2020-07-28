using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Interactable : MonoBehaviour
{
    private Stats _stats;
    void Start()
    {
        _stats = GetComponent<Stats>();
    }

    
    void Update()
    {
        
    }
}
