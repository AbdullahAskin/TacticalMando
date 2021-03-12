﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDragging : TouchControl, IBuyable
{

    [NonSerialized]
    public float price;
    private float draggingFingerId;


    protected GameObject _exampleTurret;
    protected GameObject _currentTurret;
    private GameObject _trashCan;

    private Image redScreen;
    private TurretButtonsListeners _turretButtonsListeners;
    private List<GameObject> _tileMapsGameObj;


    private bool areaIsFull = false; // Turretin altinda baska turret varmi 
    private bool turretIsDragging = false;      // Turret suanda suruklenmeye devam ediyormu 
    private bool redScreenIsWorking = false;   // Kirmizi ekran suanda aktifmi 

    public void Start()
    {
        redScreen = GameObject.FindGameObjectWithTag("Screens").transform.GetChild(0).GetComponent<Image>();  //Screenin içindeki sprite renderiri alır.
        _turretButtonsListeners = GameObject.FindGameObjectWithTag("CommonScripts").GetComponent<TurretButtonsListeners>();
        _trashCan = GameObject.FindGameObjectWithTag("Buttons").transform.GetChild(0).gameObject;

        _tileMapsGameObj = new List<GameObject>
        {
            GameObject.FindGameObjectWithTag("TileMaps").transform.GetChild(1).gameObject,
            GameObject.FindGameObjectWithTag("TileMaps").transform.GetChild(2).gameObject
        };

    }

    void Update()
    {
        if (turretIsDragging)
        {
            //Suruklemeye dair olan fonksyon.
            TouchContinue();
            TouchEnded();
        }
        else if (Input.touchCount > 0)
        {
            TouchBegan(TouchBeganOnUIObject(gameObject));
        }
    }



    void TouchBegan(float fingerId)
    {
        if (fingerId == -1) // Touch resim uzerinde baslamamis demektir.
            return;

        if (!Purchase.GoldIsEnoughToBuy(this))
        {
            return;
        }

        _currentTurret = Instantiate(_exampleTurret);
        _currentTurret.transform.position = transform.position;  // Default konumu direk atamassak gorsel bug olusuyor.
        turretIsDragging = true;
        draggingFingerId = fingerId;
        UIControl.inUIControl = true;

        _trashCan.SetActive(true);
    }

    void TouchContinue()
    {
        if (FindTouchWithFingerId(draggingFingerId).phase != TouchPhase.Canceled)
        {
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(FindTouchWithFingerId(draggingFingerId).position);
            touchWorldPosition.z = 0f; // kameradan -10 aldığı için düzeltmek zorundayız.
            _currentTurret.transform.position = touchWorldPosition;
            areaIsFull = TouchOnColliderAndTileMaps(draggingFingerId, CommonData.GetTurrets(), _tileMapsGameObj);

            RedScreenControl();
        }
    }

    void TouchEnded()
    {
        if (FindTouchWithFingerId(draggingFingerId).phase == TouchPhase.Ended)
        {
            if (!areaIsFull && !TouchOnUIObjectWithPhaseControl(_trashCan, TouchPhase.Ended))
            {
                InitializeTurret(); // iconlar en son eklenicek.
                UIControl.inUIControl = false;  // Daha sonra duzeltilicek.
                CommonData.AddTurret(_currentTurret);
                Purchase.Buy(this);
            }
            else
            {
                DestroyTurret();
            }
            turretIsDragging = false;
            _trashCan.SetActive(false);
        }
    }


    void InitializeTurret()
    {
        ClickOnTurret _turretButtons = _currentTurret.GetComponent<ClickOnTurret>();
        Turret _turret = _currentTurret.GetComponent<Turret>();
        TurretColliderEnable();       // _currentTurret.transform.GetChild(0).GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;    
        _turret.IsActive = true;    // Taretin yerleştiğini onaylıyor ve ateş izni veriyor.  
        _turretButtons.IsActive = true;
        _turretButtons._controlButton.onClick.AddListener(_turretButtonsListeners.ControlButtonClicked);
        _turretButtons._upgradeButton.onClick.AddListener(_turretButtonsListeners.UpgradeButtonClicked);
        _turretButtons._deleteTurretButton.onClick.AddListener(_turretButtonsListeners.DeleteTurretButtonClicked);
        _turretButtons._switchGunsButton.onClick.AddListener(_turretButtonsListeners.SwitchButtonClicked);
    }

    void TurretColliderEnable()
    {
        Transform _turretGFX = _currentTurret.transform.GetChild(0).GetChild(0).transform;
        foreach (Transform child in _turretGFX)
        {
            child.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }

    void DestroyTurret()
    {
        Destroy(_currentTurret);
        OpacityReset();        //Ekranı düzelt
    }

    void RedScreenControl()
    {
        if (!redScreenIsWorking)
        {
            StartCoroutine("RedScreenProtocol");
        }
    }

    IEnumerator RedScreenProtocol()
    {
        redScreenIsWorking = true;
        float currentOpacity = redScreen.color.a;
        if (areaIsFull)
        {
            while (currentOpacity < .3f)
            {
                currentOpacity += 0.1f;  // opacity degisme hizi
                redScreen.color = new Color(1.0f, 1.0f, 1.0f, currentOpacity); ;
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            OpacityReset();
        }

        redScreenIsWorking = false;
    }

    void OpacityReset()
    {
        redScreen.color = new Color(1.0f, 1.0f, 1.0f, 0f);
    }

    public float Price()
    {
        return price;
    }
}
