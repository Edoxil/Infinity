using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMotor : MonoBehaviour
{
    private Transform _thisTransform;
    private NavMeshAgent _agent;
    private Vector3 _destination;
    private float _rotationSpeed = 5f;
    private float _rotationRadius = 2.3f;
    [SerializeField] private GameObject _destinationMark = null;
    private float _markOffsetY = 0.1f;
    [SerializeField]private Animator _animator;
    private Vector3 _previousPos;


    public Transform _currentTarget;

    

    public State state = State.Default;
    public enum State
    {
        Default,
        Chase
    }
       
       

    private void Awake()
    {
        Messenger<Transform>.AddListener(GameEvent.TARGET_SELECTED, Select);
        Messenger.AddListener(GameEvent.TARGET_UNSELECTED, Unselect);
    }
    private void OnDestroy()
    {
        Messenger<Transform>.RemoveListener(GameEvent.TARGET_SELECTED, Select);
        Messenger.RemoveListener(GameEvent.TARGET_UNSELECTED, Unselect);
    }
    private void Start()
    {
        _thisTransform = GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _previousPos = _thisTransform.position;
    }
    private void Update()
    {
        if (_currentTarget != null)
        {
            RotateTowards(_currentTarget);
        }

        if (_thisTransform.position != _previousPos)
        {
            _animator.SetBool("Walk", true);
            ShowDestination();
        }
        else
        {
            _animator.SetBool("Walk", false);
            HideDestination();
        }

        
    }
    private void LateUpdate()
    {
        SetStoppingDistance();

        
        if (_currentTarget!=null && state==State.Chase)
        {
            Move(_currentTarget.position);
        }
        _previousPos = _thisTransform.position;
    }
        
        
       
       



            


    private void Select(Transform target)
    {
        _currentTarget = target;
    }
    private void Unselect()
    {
        _currentTarget = null;
    }
    public void Move(Vector3 destination)
    {
        
        _destination = destination;
        _agent.SetDestination(_destination);
        
        
    }
    private void RotateTowards(Transform target)
    {
        if (target == _thisTransform) return;

        float distance = Vector3.Distance(_thisTransform.position, target.position);
        if (distance <= _rotationRadius)
        {
            Vector3 direction = (target.position - _thisTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _thisTransform.rotation = Quaternion.Slerp(_thisTransform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
    }
    private void SetStoppingDistance()
    {
        if(state==State.Chase)
        {
            _agent.stoppingDistance = 1.15f;
        }
        if(state == State.Default)
        {
            _agent.stoppingDistance = 0f;
        }
    }
    private void HideDestination()
    {
       
       if(_thisTransform.position.x == _destination.x)
       {
       }
           _destinationMark.gameObject.SetActive(false);
            
    }
        
           
        
 
    private void ShowDestination()
    {
        Vector3 markPosition = new Vector3(_destination.x, _markOffsetY, _destination.z);
        _destinationMark.transform.position = markPosition;
        _destinationMark.gameObject.SetActive(true);
    }
        

}           