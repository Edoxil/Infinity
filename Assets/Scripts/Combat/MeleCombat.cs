using System;
using UnityEngine;

public class MeleCombat : MonoBehaviour
{
    private CharacterStats _playerStats;
    private CharacterStats _targetStats;
    private Transform _thisTransform;
    private Transform _targetTransform;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _floatingTextPrefab;
    private TextMesh _textMesh;
    private float _attackCooldown = 0f;
   
    private void Start()
    {
        _playerStats = GetComponentInParent<CharacterStats>();
        _textMesh = _floatingTextPrefab.GetComponent<TextMesh>();
        _thisTransform = GetComponent<Transform>();
    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            _animator.SetFloat("AttackSpeed", 2f);
            
        }
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

            ShowFloatingText(dmgAmaunt.ToString(),Color.green);

            _targetStats.currentHP.SetValue(hp);
            _attackCooldown = 1 / _playerStats.attackspeed.GetValue();
            Debug.Log(_targetStats.name + " take dmg " + dmgAmaunt);
        }

    }

    private void ShowFloatingText(string text, Color color)
    {
        _textMesh.color = color;
        _textMesh.text = text;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _targetStats = other.GetComponent<CharacterStats>();
            _targetTransform = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _targetStats = null;
            _targetTransform = null;
        }
    }
}
