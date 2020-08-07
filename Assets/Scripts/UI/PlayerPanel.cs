using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private CharacterStats _playerStats = null;
    [SerializeField] private Text _name = null;
    [SerializeField] private Text _lvl = null;
    [SerializeField] private Text _HPstring = null;
    [SerializeField] private Text _MPstring = null;
    [SerializeField] private Slider _healthBarFiller = null;
    [SerializeField] private Slider _manaBarFiller = null;


    private void Awake()
    {
        Messenger.AddListener(GameEvent.STAT_CHANGED, UpdatePlayerPanel);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.STAT_CHANGED, UpdatePlayerPanel);
    }
    private void Start()
    {
        UpdatePlayerPanel();
    }
    private void UpdatePlayerPanel()
    {
        _name.text = _playerStats.name;
        _lvl.text = _playerStats.level.GetValue().ToString();
        _HPstring.text = _playerStats.currentHP.GetValue() + " / " + _playerStats.maxHP.GetValue();
        _MPstring.text = _playerStats.currentMP.GetValue() + " / " + _playerStats.maxMP.GetValue();

        _healthBarFiller.maxValue = _playerStats.maxHP.GetValue();
        _manaBarFiller.maxValue = _playerStats.maxMP.GetValue();

        _healthBarFiller.value = _playerStats.currentHP.GetValue();
        _manaBarFiller.value = _playerStats.currentMP.GetValue();
    }
    public void OnSelfSelected()
    {
        Transform target = _playerStats.gameObject.transform;
        Messenger<Transform>.Broadcast(GameEvent.TARGET_SELECTED, target);
    }

}







