using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealthBar : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Stats _stats;
    private Slider _healthBarFiller;
    private Camera _camera;

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

  

   

    void Start()
    {
        
        _healthBarFiller = GetComponent<Slider>();
        _camera = Camera.main;
        gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        gameObject.transform.LookAt(_camera.transform);
    }

    


    private void UpdateHealthBar()
    {
        _stats = _target.GetComponent<Stats>();
        _healthBarFiller.maxValue = _stats.maxHP;
        _healthBarFiller.value = _stats.currentHP; 
    }


    private void Open()
    {
        gameObject.SetActive(true);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
    private void Select(Transform target)
    {
        if(_target == target)
        {
            UpdateHealthBar();
            Open();
        }
        else
        {
            Unselect();
        }
    }
    private void Unselect()
    {
        Close();
    }
}
       
        

        
        
        
