using UnityEngine;
using UnityEngine.UI;

public class TurretThreeUI : ImageDragging
{
    Text _priceText;

    private new void Start()
    {
        base.Start();

        _exampleTurret = Resources.Load<GameObject>("Turrets/Turret_3");
        price = 30f; // daha sonra belirle.

        _priceText = GetComponentInChildren<Text>();
        _priceText.text = price.ToString() + "G";
    }
}
