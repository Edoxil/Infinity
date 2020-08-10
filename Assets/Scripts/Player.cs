using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Player : MonoBehaviour
{
    private CharacterStats _stats;
    private CharacterStats _targetStats;
    private Transform _thisTransform;
    private Transform _target;




    private float _attackCooldown = 0f;
    private bool flag = false;

    void Start()
    {
        _stats = GetComponent<CharacterStats>();
        _thisTransform = GetComponent<Transform>();

    }

    private void Awake()
    {
        Messenger<Transform>.AddListener(GameEvent.TARGET_SELECTED, Select);
        Messenger.AddListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger<HealthPoints>.AddListener(GameEvent.DIED, Die);
    }



    private void OnDestroy()
    {
        Messenger<Transform>.RemoveListener(GameEvent.TARGET_SELECTED, Select);
        Messenger.RemoveListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger<HealthPoints>.RemoveListener(GameEvent.DIED, Die);
    }


  


    private void Select(Transform target)
    {
        _target = target;
        _targetStats = _target.GetComponent<CharacterStats>();
    }
    private void Unselect()
    {
        _target = null;
        _targetStats = null;
    }
    private void Die(HealthPoints healthPoints)
    {
        if (healthPoints == _stats.currentHP)
        {
            Destroy(gameObject);
        }
    }
}
