using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    private static float gold;
    private static Text _characterGoldText;

    private void OnEnable()
    {
        _characterGoldText = GameObject.Find("CurrentGoldAmount").GetComponent<Text>();
        Gold = 100;
    }


    public static bool GoldIsEnoughToBuy(IBuyable buyableScript)
    {
        if (buyableScript.Price() <= gold)
        {
            return true;
        }
        else
        {
            FloatingTextController.CreateWarningText("The current money is not enough to buy");
            return false;
        }
    }

    public static void Buy(IBuyable buyableScript)
    {
        if (buyableScript.Price() <= gold)
        {
            GoldExtraction(buyableScript.Price());
        }

    }


    public static void GoldAddition(float value)
    {
        gold += value;
        _characterGoldText.text = gold.ToString();
        FloatingTextController.AdditionFloatingGoldTextForEnemy(value.ToString(), _characterGoldText.transform);

    }

    public static void GoldExtraction(float value)
    {
        gold -= value;
        _characterGoldText.text = gold.ToString();
        FloatingTextController.ExtractionFloatingGoldTextForEnemy(value.ToString(), _characterGoldText.transform);
    }


    public float Gold
    {
        get { return gold; }
        set { gold = value; _characterGoldText.text = gold.ToString(); }
    }
}
