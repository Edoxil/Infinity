using UnityEngine;
using UnityEngine.UI;


public class TargetPanel : MonoBehaviour
{
    [SerializeField] private Text _text = null;
   
    
   void Awake()
    {
        Messenger<Transform>.AddListener(GameEvent.TARGET_SELECTED, OnTargetSelected); 
    }
    void OnDestroy()
    {
        Messenger<Transform>.RemoveListener(GameEvent.TARGET_SELECTED, OnTargetSelected); 
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
