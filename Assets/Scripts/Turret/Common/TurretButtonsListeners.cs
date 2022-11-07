using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretButtonsListeners : TouchControl
{
    public Joystick joystick_turretControl;
    private GameObject _cineMachineGameObject;
    private GameObject controlledTurret;
    private GameObject goBackToHumanButton;
    private CinemachineVirtualCamera cam_cineMach;

    public static bool turretControlModeOn;

    private void OnEnable()
    {
        turretControlModeOn = false;
    }

    void Start()
    {
        GameObject _camera = GameObject.Find("Camera");
        _cineMachineGameObject = _camera.transform.GetChild(1).gameObject;

        cam_cineMach = _cineMachineGameObject.GetComponent<CinemachineVirtualCamera>();

        goBackToHumanButton = GameObject.FindGameObjectWithTag("Buttons").transform.GetChild(1).gameObject;
        goBackToHumanButton.GetComponent<Button>().onClick.AddListener(GoBackToHumanButtonClicked);
    }


    public void ControlButtonClicked()
    {
        GameObject GO_controlButton = EventSystem.current.currentSelectedGameObject;
        Turret scr_turret = GO_controlButton.transform.parent.parent.parent.parent.GetComponent<Turret>();
        controlledTurret = scr_turret.gameObject;

        GO_controlButton.SetActive(false);
        goBackToHumanButton.SetActive(true);

        Player.joystick_movement.gameObject.SetActive(false);
        Player.joystick_fire.gameObject.SetActive(false);

        scr_turret.joystick_fire = joystick_turretControl;
        joystick_turretControl.gameObject.SetActive(true);
        cam_cineMach.Follow = GO_controlButton.gameObject.transform;

        scr_turret.isTurretUnderControl = true;
        turretControlModeOn = true;

        CloseAllTurretButtons();
    }

    public void GoBackToHumanButtonClicked()
    {
        GameObject GO_controlButton = controlledTurret.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        Turret turret = controlledTurret.GetComponent<Turret>();

        GO_controlButton.SetActive(true);
        goBackToHumanButton.SetActive(false);

        Player.joystick_movement.gameObject.SetActive(true);
        Player.joystick_fire.gameObject.SetActive(true);
        joystick_turretControl.gameObject.SetActive(false);

        Transform trans_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cam_cineMach.Follow = trans_player;

        turret.isTurretUnderControl = false;
        turretControlModeOn = false; // daha sonra kaldirirmak icin duzenle.

        ControlOffAllTurrets();
    }

    public void UpgradeButtonClicked()
    {
        GameObject GO_upgradeButton = EventSystem.current.currentSelectedGameObject;
        GO_upgradeButton.SetActive(false);

        Turret turret = GO_upgradeButton.transform.parent.parent.parent.parent.GetComponent<Turret>();
        TurretUpgrader turretUpgrader = turret.gameObject.GetComponent<TurretUpgrader>();
        turretUpgrader.TurretUpgrade(turret);

        CloseAllTurretButtons();
    }

    public void DeleteTurretButtonClicked()
    {
        GameObject GO_deleteTurretButton = EventSystem.current.currentSelectedGameObject;
        GameObject GO_turretGameObj = GO_deleteTurretButton.transform.parent.parent.parent.gameObject;
        Destroy(GO_turretGameObj);
    }

    public void SwitchButtonClicked()
    {
        SwitchGunManagment scr_switchGun = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchGunManagment>();
        GameObject GO_turret = EventSystem.current.currentSelectedGameObject.transform.parent.parent.parent.parent.gameObject;
        scr_switchGun.scr_selectedGun = GO_turret.GetComponent<Turret>().scr_guns[0];
        scr_switchGun.SwitchButtonClicked();
        GO_turret.GetComponent<ClickOnTurret>().turretButtons[1].buttonParent.SetActive(false);
    }


    public static void CloseAllTurretButtons()
    {
        List<GameObject> turrets = CommonData.GetTurrets();

        foreach (GameObject turret in turrets)
        {
            ClickOnTurret scr_clickOnTurret = turret.GetComponent<ClickOnTurret>();
            scr_clickOnTurret.CloseButtons();
        }
    }

    public static void CloseAllRanges()
    {
        List<GameObject> GO_turrets = CommonData.GetTurrets();

        foreach (GameObject turret in GO_turrets)
        {
            ClickOnTurret scr_clickOnTurret = turret.GetComponent<ClickOnTurret>();
            scr_clickOnTurret.CloseRange();
        }
    }

    public void ControlOffAllTurrets()
    {
        List<GameObject> GO_turrets = CommonData.GetTurrets();

        foreach (GameObject GO_turret in GO_turrets)
        {
            Turret scr_turret = GO_turret.GetComponent<Turret>();
            scr_turret.isTurretUnderControl = false;
        }
    }

}
