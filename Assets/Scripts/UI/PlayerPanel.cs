using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private Stats _playerStats = null;
    [SerializeField] private Text _name = null; 
    [SerializeField] private Text _lvl = null;
    [SerializeField] private Text _HPstring = null;
    [SerializeField] private Text _MPstring = null;
    [SerializeField] private Slider _healthBarFiller = null;
    [SerializeField] private Slider _manaBarFiller = null;

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
