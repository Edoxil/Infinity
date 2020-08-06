using UnityEngine;


[System.Serializable]
public class Stat 
{
    
    [SerializeField] protected int _value;

    public int GetValue()
    {
        return _value;
    }

    public virtual void SetValue(int value)
    {
        _value = value;
        Messenger.Broadcast(GameEvent.STAT_CHANGED);

    }
        
}