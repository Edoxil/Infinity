using System;
using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(NavMeshAgent))]
public class MoveByMouseClick : MonoBehaviour
{
    private Camera _camera;
    private NavMeshAgent _agent;
    private Vector3 _positionToMove;

    private Transform _currentTarget;

    [SerializeField] private GameObject _destinationMark = null;
    private float _markOffsetY = 0.1f;


    private void Awake()
    {
        Messenger.AddListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger<Transform>.AddListener(GameEvent.TARGET_SELECTED, Select);
    }

  

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger<Transform>.RemoveListener(GameEvent.TARGET_SELECTED, Select);
    }
  

    void Start()
    {
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                _positionToMove = hit.point;
                MoveToPosition();
            }
        }
              
        if(_agent.transform.position.x == _destinationMark.transform.position.x)
        {
            _destinationMark.gameObject.SetActive(false);
        }
    }

              



    private void MoveToPosition()
    {
        _agent.SetDestination(_positionToMove);
        SetEndpointMark();
    }
    private void SetEndpointMark()
    {
       
        Vector3 destinationPosition = new Vector3(_positionToMove.x, _markOffsetY, _positionToMove.z);
        _destinationMark.transform.position = destinationPosition;
        _destinationMark.gameObject.SetActive(true);
    }

    private void Select(Transform target)
    {
        _currentTarget = target;
    }

    private void Unselect()
    {
        _currentTarget = null;
    }

}




        

   









