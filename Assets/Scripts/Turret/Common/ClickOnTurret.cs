using UnityEngine;
using UnityEngine.UI;


public class ClickOnTurret : TouchControl
{
    private bool isActive = false;
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
    [System.Serializable]
    public struct TurretButton
    {
        public Button button;
        public GameObject buttonParent;
    }
    #region Variables 
    public GameObject buttonsParent;
    public TurretButton[] turretButtons;
    private GameObject turretRange;
    private Turret turret;
    private float rangeDisappearTimeout = 3f;
    #endregion


    void Start()
    {
        turretRange = transform.GetChild(2).gameObject;
        turret = GetComponent<Turret>();
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
            turretButtons[1].buttonParent.SetActive(!SwitchGunManagment.switchGunMode);
            buttonsParent.SetActive(!buttonsParent.activeSelf);
        }

        ButtonAppear(rangeDisappearTimeout);
    }


    public void ButtonAppear(float timeout) // Implementasyona dahil et.
    {
        TurretButtonsListeners.CloseAllRanges();
        turretRange.SetActive(true);
        CancelInvoke(nameof(ButtonDisappear)); // Hasar alirsa timer'i 0'lamak icin bunu yaptik.
        Invoke(nameof(ButtonDisappear), timeout);
    }

    public void ButtonDisappear()
    {
        turretRange.SetActive(false);
    }

    public void CloseButtons()
    {
        buttonsParent.SetActive(false);
    }

    public void CloseRange()
    {
        turretRange.SetActive(false);
    }



}





