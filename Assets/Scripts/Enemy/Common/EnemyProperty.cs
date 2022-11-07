using UnityEngine;
using UnityEngine.UI;

public class EnemyProperty : TouchControl, IAlive, IDamageableParticle
{
    private float hp = 100;
    private float maxHp; // Ust classdan ayarla.
    private bool healthAppear = false;
    private float healthDisappearTimeout = 2.0f;
    private bool healable = true;
    private float goldValue = 10f; // default

    private Image _healthBar;
    private Slider _healthSlider;
    private GameObject _canvas;



    public void SetHp(int value)
    {
        hp = value;
        maxHp = value;
    }

    public float GetHp()
    {
        return hp;
    }


    public void Start()
    {
        _canvas = transform.GetChild(0).gameObject;  // Daha sonra dinamik yap.
        _healthBar = transform.GetChild(0).GetComponent<Transform>().GetChild(0).GetComponent<Image>();
        _healthSlider = transform.GetChild(0).GetComponent<Transform>().GetChild(0).GetComponent<Transform>().GetComponent<Slider>();
        SliderHealthSetter(hp);
    }

    public void Update()
    {
        TouchOnEnemy();
    }

    public void TouchOnEnemy()
    {
        if (!healthAppear && TouchOnWorldObjectWithPhaseControl(gameObject, TouchPhase.Began))
        {
            HealthSliderAppear(healthDisappearTimeout);
        }
    }

    public void HealthSliderAppear(float timeout) // Implementasyona dahil et.
    {
        _healthBar.gameObject.SetActive(true);
        healthAppear = true;
        CancelInvoke("HealthSliderDisapear"); // Hasar alirsa timer'i 0'lamak icin bunu yaptik.
        Invoke("HealthSliderDisapear", timeout);
    }

    public void HealthSliderDisapear()
    {
        healthAppear = false;
        _healthBar.gameObject.SetActive(false);
    }

    public void SliderHealthSetter(float value)
    {
        _healthSlider.maxValue = value;
        _healthSlider.value = value;
        float sliderScaleX = value / 100; // Her 100 can icin bi normal slider boyutundadir.
        _healthBar.transform.localScale = new Vector3(sliderScaleX, 1, 1);
    }

    public void Dead()
    {
        throw new System.NotImplementedException();
    }

    public void CalculateDamage(float damage)
    {
        FloatingTextController.CreateFloatingDamageTextForEnemy(_canvas, damage.ToString(), transform);
        _healthSlider.value -= damage;
        HealthSliderAppear(healthDisappearTimeout);
        if (_healthSlider.value <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Heal(float heal)
    {
        if (!healable)
            return;

        FloatingTextController.CreateFloatingHealTextForEnemy(_canvas, heal.ToString(), transform);
        healable = false;
        Invoke(nameof(ActivateHealing), .4f);

        if (_healthSlider.value + heal < maxHp) // Max canindan daha fazla iyilesemez.
            _healthSlider.value += heal;

    }
    void ActivateHealing() // arka arkaya iyilesmenin onune gecer 
    {
        healable = true;
    }
    public void InitializeParticle(GameObject _particleOrigin)
    {
        GameObject _particle = Instantiate(_particleOrigin);  // Burda null exception aliniyor.
        _particle.transform.position = gameObject.transform.position;

        ParticleSystem _partSys = _particle.GetComponent<ParticleSystem>();
        _partSys.Play();
        Destroy(_particle, _partSys.main.duration + _partSys.main.startLifetime.constantMax); // Arti bir sure vermezsek direk kayboluyor.
    }

    void OnDestroy()
    {
        CommonData.RemoveEnemy(transform);
        Purchase.GoldAddition(goldValue);
    }




}

