using UnityEngine;
using UnityEngine.UI;


public class TargetHealthBar : MonoBehaviour
{

    private Transform _target;
    private CharacterStats _stats;
    private Slider _healthBarFiller;
    private Camera _camera;
    private float _offsetY = 3f;

    private void Awake()
    {
        Messenger<Transform>.AddListener(GameEvent.TARGET_SELECTED, Select);
        Messenger.AddListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger.AddListener(GameEvent.STAT_CHANGED, UpdateHealthBar);
        Messenger<HealthPoints>.AddListener(GameEvent.DIED, HideIfActive); 

    }



    private void OnDestroy()
    {
        Messenger<Transform>.RemoveListener(GameEvent.TARGET_SELECTED, Select);
        Messenger.RemoveListener(GameEvent.TARGET_UNSELECTED, Unselect);
        Messenger.RemoveListener(GameEvent.STAT_CHANGED, UpdateHealthBar);
        Messenger<HealthPoints>.RemoveListener(GameEvent.DIED, HideIfActive);

    }





    void Start()
    {

        _healthBarFiller = GetComponent<Slider>();
        _camera = Camera.main;
        gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        gameObject.transform.LookAt(_camera.transform);
        Vector3 newPos = new Vector3(_target.position.x, _target.position.y + _offsetY, _target.position.z);
        transform.position = newPos;
    }




    private void UpdateHealthBar()
    {
        _stats = _target.GetComponent<CharacterStats>();
        _healthBarFiller.maxValue = _stats.maxHP.GetValue();
        _healthBarFiller.value = _stats.currentHP.GetValue();
    }


    private void Open()
    {
        gameObject.SetActive(true);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
    private void Select(Transform target)
    {

        if (target.TryGetComponent(out CharacterStats stats))
        {
            _target = target;


            UpdateHealthBar();
            Open();
        }
        else
        {
            Unselect();
        }
    }

    private void Unselect()
    {
        Close();
    }

    private void HideIfActive(HealthPoints healthPoints)
    {
        if (!isActiveAndEnabled) return;
        if (_stats.currentHP == healthPoints)
        {

            Unselect();
        }
    }
}






