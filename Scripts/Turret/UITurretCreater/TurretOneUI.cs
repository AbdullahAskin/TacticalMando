using UnityEngine;
using UnityEngine.UI;

public class TurretOneUI : ImageDragging
{
    Text _priceText;
    private new void Start()
    {
        base.Start();

        _exampleTurret = Resources.Load<GameObject>("Turrets/Turret_1");

        price = 10f; // daha sonra belirle.
        _priceText = GetComponentInChildren<Text>();
        _priceText.text = price.ToString() + "G";
    }


}
