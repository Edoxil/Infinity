using UnityEngine;
using UnityEngine.EventSystems;

public class InteractByMouseClick : MonoBehaviour
{
    private Camera _camera;


    void Start()
    {
        _camera = Camera.main;
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
                if (newTarget.TryGetComponent(out Interactable _))
                {
                    Messenger<Transform>.Broadcast(GameEvent.TARGET_SELECTED, newTarget);
                }

            }
        }
    }
}





