using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private Stats _playerStats;
    [SerializeField] private Text _name; 
    [SerializeField] private Text _lvl;
    [SerializeField] private Text _HPstring;
    [SerializeField] private Text _MPstring;
    [SerializeField] private Slider _healthBarFiller;
    [SerializeField] private Slider _manaBarFiller;

    private void Start()
    {
        UpdatePlayerPanel();
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_STATS_CHANGED, UpdatePlayerPanel);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_STATS_CHANGED, UpdatePlayerPanel);
    }

    private void Update()
    {
        // убрать из апдейта когда будет рассылка по ивенту PLATER_STATS_CHANGED
        UpdatePlayerPanel();
       
    }
    private void UpdatePlayerPanel()
    {
        _name.text = _playerStats.name;
        _lvl.text = _playerStats.level.ToString();
        _HPstring.text = _playerStats.currentHP + " / " + _playerStats.maxHP;
        _MPstring.text = _playerStats.currentMP + " / " + _playerStats.maxMP;

        _healthBarFiller.maxValue = _playerStats.maxHP;
        _manaBarFiller.maxValue = _playerStats.maxMP;

        _healthBarFiller.value = _playerStats.currentHP;
        _manaBarFiller.value = _playerStats.currentMP;
    }
        
   

  
    public void OnSelfSelected()
    {
        Transform target = _playerStats.gameObject.transform;
        Messenger<Transform>.Broadcast(GameEvent.TARGET_SELECTED, target);
    }

}
