using UnityEngine;




[System.Serializable]
public class HealthPoints : Stat
{
    public override void SetValue(int value)
    {
        base.SetValue(value);
     
        if(_value<=0)
        {
            Messenger<HealthPoints>.Broadcast(GameEvent.DIED, this);
        }    
    }
}
