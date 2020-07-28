using UnityEngine;
using Cinemachine;


[RequireComponent(typeof(CinemachineFreeLook))]
public class ThirdPersonCamera : MonoBehaviour
{
    private CinemachineFreeLook _freeLook;
    void Start()
    {
        _freeLook = GetComponent<CinemachineFreeLook>();
    }
        

    
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _freeLook.m_XAxis.m_MaxSpeed = 450;
        }
        else
        {
            _freeLook.m_XAxis.m_MaxSpeed = 0;
        }
    }
}
            
