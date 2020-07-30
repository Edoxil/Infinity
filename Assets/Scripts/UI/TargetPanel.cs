using System;
using UnityEngine;
using UnityEngine.UI;


public class TargetPanel : MonoBehaviour
{
    [SerializeField] private Text _text = null;
   
    
   void Awake()
    {
        Messenger<Transform>.AddListener(GameEvent.TARGET_SELECTED, OnTargetSelected); 
        Messenger.AddListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger<Transform>.AddListener(GameEvent.TRANSFORM_DESTROYED, HideIfActive);
    }
    void OnDestroy()
    {
        Messenger<Transform>.RemoveListener(GameEvent.TARGET_SELECTED, OnTargetSelected);
        Messenger.RemoveListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger<Transform>.RemoveListener(GameEvent.TRANSFORM_DESTROYED, HideIfActive);
    }

  


    void Start()
    {
       Close();
    }

   

    private void OnTargetSelected(Transform target)
    {
        if(target.TryGetComponent(out Stats stats))
        {
            _text.text = stats.name;
        }
        Open();
    }

    private void Unselect()
    {
        gameObject.SetActive(false);
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

    private void HideIfActive(Transform transform)
    {
        if (!isActiveAndEnabled)
            return;
        else
            gameObject.SetActive(false);
    }
}
