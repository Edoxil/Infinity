using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickInput : MonoBehaviour
{
    private Camera _camera;
    private CharacterMotor _motor;

    void Start()
    {
        _camera = Camera.main;
        _motor = GetComponent<CharacterMotor>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                Transform newTarget;
                newTarget = hit.transform;

                if(newTarget == _motor._currentTarget)
                {
                    _motor.state = CharacterMotor.State.Chase;
                }
                else
                {
                    _motor.state = CharacterMotor.State.Default;
                    _motor.Move(hit.point);
                }

                if (newTarget.TryGetComponent(out CharacterStats _))
                {
                    Messenger<Transform>.Broadcast(GameEvent.TARGET_SELECTED, newTarget);
                }

            }
        }
    }
}
                
                
                
               
                    
                





