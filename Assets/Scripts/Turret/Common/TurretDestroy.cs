using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurretDestroy
{
    public float elapsedTime = 0f;

    private Slider slider_target;
    private Turret scr_turret;
    public TurretDestroy(Slider targetSlider, Turret targetTurretScr)
    {
        this.slider_target = targetSlider;
        this.scr_turret = targetTurretScr;
    }

    public IEnumerator SwitchGunTimer(float duration)
    {
        while (duration > elapsedTime)
        {
            float currentSliderValue = Mathf.Lerp(100f, 0f, elapsedTime / duration);
            slider_target.value = currentSliderValue;
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        scr_turret.DestroyTheTurret();
    }
}
