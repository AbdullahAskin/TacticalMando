using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwitchGun : MonoBehaviour
{
    GameObject _switchButton;
    GameObject _switchGuns;
    GameObject _defaultGun;
    GameObject _timer;
    GameObject _currentSwitchGun;

    Slider _timerSlider;

    Player _player;
    PlayerAnimation _playerAnimation;

    public Gun _selectedGun;

    private float switchGunUsingTime = 10f;

    public static bool switchGunMode = false;

    private void OnEnable()
    {
        switchGunMode = false;
    }
    void Start()
    {
        _switchButton = GameObject.FindGameObjectWithTag("Buttons").transform.GetChild(1).gameObject;
        _switchButton.GetComponent<Button>().onClick.AddListener(SwitchButtonClicked);

        _switchGuns = GameObject.FindGameObjectWithTag("Guns").transform.GetChild(1).gameObject;
        _defaultGun = GameObject.FindGameObjectWithTag("Guns").transform.GetChild(0).gameObject;

        _timer = gameObject.transform.GetChild(2).GetChild(0).gameObject;
        _timerSlider = _timer.transform.GetComponent<Slider>();

        _player = GetComponent<Player>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void SwitchButtonClicked()
    {
        _currentSwitchGun = Instantiate(_selectedGun.gameObject);
        StartCoroutine("SwitchGunTimer");
    }

    public void SwitchGunEnable()
    {
        _defaultGun.SetActive(false);

        _currentSwitchGun.transform.SetParent(_switchGuns.transform, false);
        _player._gun = _currentSwitchGun.GetComponent<Gun>();
    }

    public void SwitchGunDisable()
    {
        Destroy(_currentSwitchGun);

        _defaultGun.SetActive(true);
        _player._gun = _defaultGun.GetComponent<Gun>();

    }

    IEnumerator SwitchGunTimer()
    {
        switchGunMode = true;
        _playerAnimation.SwitchGun(true);
        _timer.SetActive(true);
        SwitchGunEnable();

        float decreaseAmount = (100 / switchGunUsingTime) * 0.2f;
        while (_timerSlider.value > 0)
        {
            _timerSlider.value -= decreaseAmount;
            yield return new WaitForSeconds(0.2f);
        }

        _timer.SetActive(false);
        _playerAnimation.SwitchGun(false);
        SwitchGunDisable();


        _timerSlider.value = _timerSlider.maxValue;
        switchGunMode = false;
    }
}
