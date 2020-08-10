using UnityEngine;

public class MeleCombat : MonoBehaviour
{
    private CharacterStats _playerStats;
    private CharacterStats _targetStats;
    [SerializeField] private Animator _animator;
    private float _attackCooldown = 0f;
    
    private void Start()
    {
        _playerStats = GetComponentInParent<CharacterStats>();
        
        
    }



    void Update()
    {

        _attackCooldown -= Time.deltaTime;

        if (_targetStats != null)
        {
            _animator.Play("Attack");
            Attack(_playerStats.attack.GetValue());
            
        }

    }

    private void Attack(int dmgAmaunt)
    {
        if (_attackCooldown <= 0f)
        {
            dmgAmaunt -= _targetStats.defense.GetValue();
            int hp = _targetStats.currentHP.GetValue() - dmgAmaunt;

            _targetStats.currentHP.SetValue(hp);
            _attackCooldown = 1 / _playerStats.attackspeed.GetValue();
            Debug.Log(_targetStats.name + " take dmg " + dmgAmaunt);
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _targetStats = other.GetComponent<CharacterStats>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _targetStats = null;
        }
    }
}
