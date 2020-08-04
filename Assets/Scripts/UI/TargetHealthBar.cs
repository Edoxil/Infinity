using System;
using UnityEngine;
using UnityEngine.UI;


public class TargetHealthBar : MonoBehaviour
{
   
    private Transform _target;
    private Stats _stats;
    private Slider _healthBarFiller;
    private Camera _camera;
    private float _offsetY = 1.5f;

    private void Awake()
    {
        Messenger<Transform>.AddListener(GameEvent.TARGET_SELECTED, Select);
        Messenger.AddListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger<Transform>.AddListener(GameEvent.TRANSFORM_DESTROYED, HideIfActive);
        
    }

   

    private void OnDestroy()
    {
        Messenger<Transform>.RemoveListener(GameEvent.TARGET_SELECTED, Select);
        Messenger.RemoveListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger<Transform>.RemoveListener(GameEvent.TRANSFORM_DESTROYED, HideIfActive);

    }


    private void Update()
    {
        UpdateHealthBar();
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
        Vector3 newPos = new Vector3(_target.position.x, _target.position.y + _offsetY, _target.position.z);
        transform.position = newPos;
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
       
        if(target.TryGetComponent(out Stats stats))
        {
            _target = target;
            
         
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

    private void HideIfActive(Transform transform)
    {
        if (!isActiveAndEnabled) return;
        if(transform == _target)
        {
            Unselect();
        }
    }
}
       
        

        
        
        
