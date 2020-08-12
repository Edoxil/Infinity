using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private float _destroyTime = 2f;
    private Vector3 _offset = new Vector3(0f, 2.5f, 0);
    private Vector3 _randomaizeIntensity = new Vector3(0.5f, 0, 0);
    void Start()
    {
        Destroy(this.gameObject, _destroyTime);
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
        transform.localPosition += _offset;
        transform.localPosition += new Vector3(Random.Range(-_randomaizeIntensity.x, _randomaizeIntensity.x),
            Random.Range(-_randomaizeIntensity.y, _randomaizeIntensity.y),
            Random.Range(-_randomaizeIntensity.z, _randomaizeIntensity.z));


    }


}
