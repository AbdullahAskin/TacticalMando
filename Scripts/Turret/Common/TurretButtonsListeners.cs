using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretButtonsListeners : TouchControl
{
    private Joystick _movementJoystick;
    private Joystick _fireJoystick;
    private Joystick _turretControlJoystick;
    private GameObject _cineMachineGameObject;
    private GameObject _controlledTurret;
    private GameObject _goBackToHumanButton;
    private CinemachineVirtualCamera _cineMach;

    public static bool turretControlModeOn;

    private void OnEnable()
    {
        turretControlModeOn = false;
    }

    void Start()
    {
        GameObject joysticks = GameObject.FindGameObjectWithTag("Joysticks");
        _movementJoystick = joysticks.transform.GetChild(0).GetComponent<Joystick>();
        _fireJoystick = joysticks.transform.GetChild(1).GetComponent<Joystick>();
        _turretControlJoystick = joysticks.transform.GetChild(2).GetComponent<Joystick>();

        GameObject _camera = GameObject.Find("Camera");
        _cineMachineGameObject = _camera.transform.GetChild(1).gameObject;

        _cineMach = _cineMachineGameObject.GetComponent<CinemachineVirtualCamera>();

        _goBackToHumanButton = GameObject.FindGameObjectWithTag("Buttons").transform.GetChild(1).gameObject;
        _goBackToHumanButton.GetComponent<Button>().onClick.AddListener(GoBackToHumanButtonClicked);
    }


    public void ControlButtonClicked()
    {
        GameObject _controlButton = EventSystem.current.currentSelectedGameObject;
        Turret _turret = _controlButton.transform.parent.parent.parent.GetComponent<Turret>();
        _controlledTurret = _turret.gameObject;

        _controlButton.SetActive(false);
        _goBackToHumanButton.SetActive(true);

        //Joystickleri görünmez yap ve control butonun görünürlügünü kapat
        _movementJoystick.gameObject.SetActive(false);
        _fireJoystick.gameObject.SetActive(false);

        //Turretin joysticiğini ayarlama ve kamerasyı ona çevirme.
        _turret._fireJoystick = _turretControlJoystick;
        _turretControlJoystick.gameObject.SetActive(true);
        _cineMach.Follow = _controlButton.gameObject.transform;

        _turret.turretControl = true;
        turretControlModeOn = true;

        CloseAllTurretButtons();
    }

    public void GoBackToHumanButtonClicked()
    {
        GameObject _controlButton = _controlledTurret.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        Turret _turret = _controlledTurret.GetComponent<Turret>();

        _controlButton.SetActive(true);
        _goBackToHumanButton.SetActive(false);

        _movementJoystick.gameObject.SetActive(true);
        _fireJoystick.gameObject.SetActive(true);
        _turretControlJoystick.gameObject.SetActive(false);

        Transform _playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _cineMach.Follow = _playerTrans;

        _turret.turretControl = false;
        turretControlModeOn = false; // daha sonra kaldirirmak icin duzenle.

        ControlOffAllTurrets();
    }

    public void UpgradeButtonClicked()
    {
        GameObject _upgradeButton = EventSystem.current.currentSelectedGameObject;
        _upgradeButton.SetActive(false);

        Turret _turret = _upgradeButton.transform.parent.parent.parent.GetComponent<Turret>();
        TurretUpgrader _turretUpgrader = _turret.gameObject.GetComponent<TurretUpgrader>();
        _turretUpgrader.TurretUpgrade(_turret);

        CloseAllTurretButtons();
    }

    public void DeleteTurretButtonClicked()
    {
        GameObject _deleteTurretButton = EventSystem.current.currentSelectedGameObject;
        GameObject _turretGameObj = _deleteTurretButton.transform.parent.parent.parent.gameObject;
        Destroy(_turretGameObj);
    }

    public void SwitchButtonClicked()
    {
        GameObject _turretGameObj = EventSystem.current.currentSelectedGameObject.transform.parent.parent.parent.gameObject;
        SwitchGun _switchGunScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchGun>();
        _switchGunScript._selectedGun = _turretGameObj.GetComponent<Turret>()._guns[0];
        _switchGunScript.SwitchButtonClicked();
        _turretGameObj.GetComponent<ClickOnTurret>()._switchGunsButton.gameObject.SetActive(false);
    }


    public static void CloseAllTurretButtons()
    {
        List<GameObject> turrets = CommonData.GetTurrets();

        foreach (GameObject turret in turrets)
        {
            ClickOnTurret _clickOnTurret = turret.GetComponent<ClickOnTurret>();
            _clickOnTurret.CloseButtons();
        }
    }

    public static void CloseAllRanges()
    {
        List<GameObject> turrets = CommonData.GetTurrets();

        foreach (GameObject turret in turrets)
        {
            ClickOnTurret _clickOnTurret = turret.GetComponent<ClickOnTurret>();
            _clickOnTurret.CloseRange();
        }
    }

    public void ControlOffAllTurrets()
    {
        List<GameObject> turrets = CommonData.GetTurrets();

        foreach (GameObject turret in turrets)
        {
            Turret _turret = turret.GetComponent<Turret>();
            _turret.turretControl = false;
        }
    }

}
