using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(NavMeshAgent))]
public class MoveByMouseClick : MonoBehaviour
{
    private Camera _camera;
    private NavMeshAgent _agent;
    private Vector3 _positionToMove;

    private Transform _currentTarget;


    [SerializeField] private GameObject _destinationMark = null;
    private float _markOffsetY = 0.1f;

    private float _distanceToTarget=0f;
    private float _rotationSpeed = 5f;
    private float _stopDistance=2.5f;



    private void Awake()
    {
        Messenger.AddListener(GameEvent.TARGET_UNSELECTED, Unselect);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TARGET_UNSELECTED, Unselect);
    }
       


    void Start()
    {
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
           

            if (Physics.Raycast(ray, out hit))
            {
                Transform newTarget = hit.transform;

                _positionToMove = hit.point;
                

                if (newTarget == _currentTarget)
                {
                    // move to interactable object based on his radius
                    MoveToPosition(_stopDistance);

                    
                }

                if (newTarget.TryGetComponent(out Interactable interact))
                {
                    _currentTarget = newTarget;
                    CalculateDistanceToTarget();
                }
                else
                {
                    MoveToPosition();
                    _distanceToTarget = 0f;
                }

            }
        }



                    


        if(_distanceToTarget !=0 && _distanceToTarget <2.5f )
        {
            
            RotateTowards(_currentTarget);
            _destinationMark.gameObject.SetActive(false);
            
        }
        if (_agent.transform.position.x == _destinationMark.transform.position.x)
        {
            _destinationMark.gameObject.SetActive(false);
        }
                    
    }

    private void CalculateDistanceToTarget()
    {
        _distanceToTarget = Vector3.Distance(transform.position,_currentTarget.position);
    }
        
    private void RotateTowards(Transform target)
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);

    }

    private void Unselect()
    {
        _currentTarget = null;
    }

   







  
    private void MoveToPosition(float stopingDistance = 0f)
    {
        _agent.stoppingDistance = stopingDistance;
        _agent.SetDestination(_positionToMove);
        SetEndpointMark();
    }

    private void SetEndpointMark()
    {

        Vector3 destinationPosition = new Vector3(_positionToMove.x, _markOffsetY, _positionToMove.z);

        _destinationMark.transform.position = destinationPosition;
        _destinationMark.gameObject.SetActive(true);
    }
}



  





























































