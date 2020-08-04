using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(NavMeshAgent))]
public class MoveByMouseClick : MonoBehaviour
{
    
    private Camera _camera;
    private NavMeshAgent _agent;
    private Vector3 _positionToMove;

    private Transform _thisTransform;
    private Transform _currentTarget;
    private float _radius = 3.5f;

    [SerializeField] private GameObject _destinationMark = null;
    private float _markOffsetY = 0.1f;

    private float _distanceToTarget = -1f;
    private float _rotationSpeed = 5f;

    private AgentState State = AgentState.Stop;
    private enum AgentState
    {
        Move,
        Chase,
        Stop
    }




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
        _thisTransform = GetComponent<Transform>();
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

                if (_currentTarget == newTarget)
                {
                    CalculateDistanceToTarget();
                    State = AgentState.Chase;
                }
                else
                {
                    _distanceToTarget = -1f;
                    State = AgentState.Move;
                }

                MoveToPosition(_positionToMove);
            }
        }

        if (_currentTarget != null && State == AgentState.Chase)
        {
            _positionToMove = _currentTarget.position;
            MoveToPosition(_positionToMove);
        }
    }



    private void LateUpdate()
    {
        if (_distanceToTarget > 0 && _distanceToTarget < _radius)
        {

            RotateTowards(_currentTarget);
            _destinationMark.gameObject.SetActive(false);

        }



        if (_agent.transform.position.x == _destinationMark.transform.position.x)
        {
            _destinationMark.gameObject.SetActive(false);
            State = AgentState.Stop;
        }


    }
    private void CalculateDistanceToTarget()
    {
        _distanceToTarget = Vector3.Distance(_thisTransform.position, _currentTarget.position);
    }

    private void RotateTowards(Transform target)
    {
        if (target == null) return;

        Vector3 direction = (target.position - _thisTransform.position).normalized; 
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _thisTransform.rotation = Quaternion.Slerp(_thisTransform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);

    }

    private void Unselect()
    {
        _currentTarget = null;
    }

    
    private void Select(Transform target)
    {

        _currentTarget = target;

    }
    public void MoveToPosition(Vector3 position)
    {

        _agent.SetDestination(position);
        SetEndpointMark();
    }
    private void SetEndpointMark()
    {
        Vector3 destinationPosition = new Vector3(_positionToMove.x, _markOffsetY, _positionToMove.z);
        _destinationMark.transform.position = destinationPosition;
        _destinationMark.gameObject.SetActive(true);
    }

}














