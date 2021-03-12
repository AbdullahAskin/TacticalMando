using UnityEngine;
using UnityEngine.UI;


public class ClickOnTurret : TouchControl
{
    private bool isActive = false;

    public GameObject _buttonsGameObj;
    public Button _controlButton;
    public Button _upgradeButton;
    public Button _deleteTurretButton;
    public Button _switchGunsButton;

    private GameObject _turretRange;

    private Turret _turret;

    public float rangeDisappearTimeout = 3f;


    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    void Start()
    {
        _buttonsGameObj = transform.GetChild(1).GetChild(0).gameObject;
        _controlButton = _buttonsGameObj.transform.GetChild(0).GetComponent<Button>();
        _upgradeButton = _buttonsGameObj.transform.GetChild(1).GetComponent<Button>();
        _deleteTurretButton = _buttonsGameObj.transform.GetChild(2).GetComponent<Button>();
        _switchGunsButton = _buttonsGameObj.transform.GetChild(3).GetComponent<Button>();

        _turretRange = transform.GetChild(2).gameObject;
        _turret = GetComponent<Turret>();
    }

    void Update()
    {
        if (isActive)
        {
            if (Input.touchCount > 0)
            {
                if (TouchOnWorldObjectWithPhaseControl(gameObject, TouchPhase.Began))
                {
                    TurretClicked();
                }
            }
        }
    }

    private void TurretClicked()  // Dinamik bi sekilde tekrar yaz.
    {
        if (!TurretButtonsListeners.turretControlModeOn)
        {
            TurretButtonsListeners.CloseAllTurretButtons();
            _switchGunsButton.gameObject.SetActive(!SwitchGun.switchGunMode);
            _buttonsGameObj.SetActive(!_buttonsGameObj.activeSelf);
        }

        ButtonAppear(rangeDisappearTimeout);
    }


    public void ButtonAppear(float timeout) // Implementasyona dahil et.
    {
        TurretButtonsListeners.CloseAllRanges();
        _turretRange.SetActive(true);
        CancelInvoke(nameof(ButtonDisappear)); // Hasar alirsa timer'i 0'lamak icin bunu yaptik.
        Invoke(nameof(ButtonDisappear), timeout);
    }

    public void ButtonDisappear()
    {
        _turretRange.SetActive(false);
    }

    public void CloseButtons()
    {
        _buttonsGameObj.SetActive(false);
    }

    public void CloseRange()
    {
        _turretRange.SetActive(false);
    }



}





