using UnityEngine;
using UnityEngine.UI;

public class TurretTwoUI : ImageDragging
{
    Text _priceText;

    private new void Start()
    {
        base.Start();

        _exampleTurret = Resources.Load<GameObject>("Turrets/Turret_2");
        price = 20f; // daha sonra belirle.

        _priceText = transform.parent.GetComponentInChildren<Text>();
        _priceText.text = price.ToString() + "G";
    }
}
