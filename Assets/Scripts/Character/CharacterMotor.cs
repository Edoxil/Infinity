using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMotor : MonoBehaviour
{
    private Transform _thisTransform;
    private Transform _target;
    private NavMeshAgent _agent;
    private Vector3 _destination;
    private float _rotationSpeed = 5f;
    private float _rotationRadius = 3f;
    [SerializeField] private GameObject _destinationMark = null;
    private float _markOffsetY = 0.1f;

    

    public State _state = State.Moving;
    public enum State
    {
        Moving,
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
    }
        
        




    private void LateUpdate()
    {
        if(_thisTransform.position.x == _destination.x)
        {
            HideDestination();
        }
            

        if(_target!=null)
        {
            RotateTowards(_target);
        }

        if(_target!=null && _state==State.Chase)
        {
            Move(_target.position);
        }
    }


       
            
            
    private void Select(Transform target)
    {
        _target = target;
    }

    private void Unselect()
    {
        _target = null;
    }

    public void Move(Vector3 destination)
    {
        
        _destination = destination;
        _agent.SetDestination(destination);
        _state = State.Moving;
        ShowDestination();
    }

    private void RotateTowards(Transform target)
    {
        float distance = Vector3.Distance(_thisTransform.position, _destination);
        if (distance <= _rotationRadius)
        {
            Debug.Log(distance);
            Vector3 direction = (target.position - _thisTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _thisTransform.rotation = Quaternion.Slerp(_thisTransform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
    }
          
        
            
    private void HideDestination()
    {
        _destinationMark.gameObject.SetActive(false);
    }
    private void ShowDestination()
    {
        Vector3 markPosition = new Vector3(_destination.x, _markOffsetY, _destination.z);
        _destinationMark.transform.position = markPosition;
        _destinationMark.gameObject.SetActive(true);
    }

}

        
        
        

        
        

