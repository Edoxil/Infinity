using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Player : MonoBehaviour
{
    private CharacterStats _stats;
    private CharacterStats _targetStats;
    private Transform _transform;
    private Transform _target;
    private State _state = State.Peace;

    
    private float _attackSpeed= 1f;
    private float _attackCooldown = 0f;

    private enum State
    {
        Battle,
        Peace
    }
    void Start()
    {
        _stats = GetComponent<CharacterStats>();
        _transform = GetComponent<Transform>();
       
    }

    private void Awake()
    {
        Messenger<Transform>.AddListener(GameEvent.TARGET_SELECTED, Select);
        Messenger.AddListener(GameEvent.TARGET_UNSELECTED, Unselect);
    }
    private void OnDestroy()
    {
        Messenger<Transform>.RemoveListener(GameEvent.TARGET_SELECTED, Select);
        Messenger.RemoveListener(GameEvent.TARGET_UNSELECTED, Unselect);
    }


    void Update()
    {

        
        if(_state==State.Battle)
        {
            _attackCooldown -= Time.deltaTime;
            //Attack(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
           
           
        }
        
    }

    private void Attack(int dmgAmaunt)
    {
        if(_attackCooldown<=0f)
        {
            //_targetStats.currentHP -= dmgAmaunt;
            //_attackCooldown = 1 / _attackSpeed;
            //Debug.Log(dmgAmaunt);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _state = State.Battle;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _state = State.Peace;
        }
    }
    private void Select(Transform target)
    {
        _target = target;
        _targetStats = _target.GetComponent<CharacterStats>();
    }
    private void Unselect()
    {
        _target = null;
        _targetStats = null;
    }
}
