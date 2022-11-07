using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwitchGunManagment : MonoBehaviour
{
    #region Variables
    public GameObject GO_switchGuns, GO_defaultGun, GO_timer;
    private GameObject GO_currentSwitchGun;

    public Slider slider_timer;

    private Player scr_player;
    private PlayerAnimation scr_playerAnimation;

    [HideInInspector] public Gun scr_selectedGun;

    private float switchGunUsingTime = 10f;

    public static bool switchGunMode = false;
    #endregion

    private void OnEnable()
    {
        switchGunMode = false;
    }

    void Start()
    {
        scr_player = GetComponent<Player>();
        scr_playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void SwitchButtonClicked()
    {
        GO_currentSwitchGun = Instantiate(scr_selectedGun.gameObject);
        StartCoroutine(SwitchGunTimer());
    }

    public void SwitchGunEnable()
    {
        GO_defaultGun.SetActive(false);

        GO_currentSwitchGun.transform.SetParent(GO_switchGuns.transform, false);
        scr_player._gun = GO_currentSwitchGun.GetComponent<Gun>();
    }

    public void SwitchGunDisable()
    {
        Destroy(GO_currentSwitchGun);

        GO_defaultGun.SetActive(true);
        scr_player._gun = GO_defaultGun.GetComponent<Gun>();

    }

    IEnumerator SwitchGunTimer()
    {
        switchGunMode = true;
        scr_playerAnimation.SwitchGun(true);
        GO_timer.SetActive(true);
        SwitchGunEnable();

        float decreaseAmount = (100 / switchGunUsingTime) * 0.2f;
        while (slider_timer.value > 0)
        {
            slider_timer.value -= decreaseAmount;
            yield return new WaitForSeconds(0.2f);
        }

        GO_timer.SetActive(false);
        scr_playerAnimation.SwitchGun(false);
        SwitchGunDisable();

        slider_timer.value = slider_timer.maxValue;
        switchGunMode = false;
    }
}
