using System;
using UnityEngine;
using UnityEngine.UI;


public class TargetPanel : MonoBehaviour
{
   [SerializeField] private Text _text = null;
    private CharacterStats _stats;
    
   void Awake()
    {
        Messenger<Transform>.AddListener(GameEvent.TARGET_SELECTED, OnTargetSelected);
        Messenger.AddListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger<HealthPoints>.AddListener(GameEvent.DIED, OnDied);
       
   }
    void OnDestroy()
    {
        Messenger<Transform>.RemoveListener(GameEvent.TARGET_SELECTED, OnTargetSelected);
        Messenger.RemoveListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger<HealthPoints>.RemoveListener(GameEvent.DIED, OnDied);
        
    }

  


    void Start()
    {
       Close();
    }

   

    private void OnTargetSelected(Transform target)
    {
        if(target.TryGetComponent(out CharacterStats stats))
        {
            _stats = stats;
            _text.text = _stats.name;
            Open();
        }
        
    }

    private void Unselect()
    {
        
        gameObject.SetActive(false);
    }

    private void OnDied(HealthPoints healthPoints)
    {
        if(healthPoints==_stats.currentHP)
        {
            Close();
            _stats = null;
        }
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        Messenger.Broadcast(GameEvent.TARGET_UNSELECTED);
        gameObject.SetActive(false); 
    }

  
}
