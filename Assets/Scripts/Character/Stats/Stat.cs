using UnityEngine;


[System.Serializable]
public class Stat 
{
    [SerializeField]
    private int _value;

    public int GetValue()
    {
        return _value;
    }

    public void SetValue(int value)
    {
        _value = value;
    }
}