
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    public static FloatingText _damageEnemy, _damageCharacter, _heal, _additionGold, _extractionGold;
    public static FloatingText _warning, _round;
    public static GameObject _canvas, _messagesLocation;
    public void Start()
    {
        Initialize();
    }

    public static void Initialize()
    {
        _damageEnemy = Resources.Load<FloatingText>("UI/Common/PopUpTexts/EnemyDamageGO");
        _heal = Resources.Load<FloatingText>("UI/Common/PopUpTexts/HealGO");
        _damageCharacter = Resources.Load<FloatingText>("UI/Common/PopUpTexts/CharacterDamageGO");
        _additionGold = Resources.Load<FloatingText>("UI/Common/PopUpTexts/AddGoldGO");
        _extractionGold = Resources.Load<FloatingText>("UI/Common/PopUpTexts/ExtractionGoldGO");
        _warning = Resources.Load<FloatingText>("UI/Common/Texts/WarningGO");
        _round = Resources.Load<FloatingText>("UI/Common/Texts/RoundGO");
        _canvas = GameObject.Find(nameof(Canvas));
        _messagesLocation = _canvas.transform.GetChild(7).gameObject;
    }

    // Size dinamikligi ekle.
    public static void CreateFloatingDamageTextForEnemy(GameObject _canvas, string text, Transform location)
    {
        text = "-" + text;
        CreatingRandomFloatingText(_canvas, text, location, _damageEnemy);
    }

    public static void CreateFloatingDamageTextForCharacter(GameObject _canvas, string text, Transform location)
    {
        text = "-" + text;
        CreatingRandomFloatingText(_canvas, text, location, _damageCharacter);
    }

    public static void CreateFloatingHealTextForEnemy(GameObject _canvas, string text, Transform location)
    {
        text = "+" + text;
        CreatingRandomFloatingText(_canvas, text, location, _heal);

    }

    public static void AdditionFloatingGoldTextForEnemy(string text, Transform location)
    {
        text = "+" + text;
        CreateFloatingText(_canvas, text, location, _additionGold);

    }

    public static void ExtractionFloatingGoldTextForEnemy(string text, Transform location)
    {
        text = "-" + text;
        CreateFloatingText(_canvas, text, location, _extractionGold);
    }



    public static void CreateWarningText(string text)
    {
        CreateText(text, _warning);
    }

    public static void CreateRoundText(string text)
    {
        CreateText(text, _round);
    }

    static void CreateText(string text, FloatingText _floatingText)
    {
        FloatingText _instance = Instantiate(_floatingText);
        Vector2 textPosition = new Vector2(_messagesLocation.transform.position.x + 0.5f, _messagesLocation.transform.position.y);
        _instance.transform.SetParent(_canvas.transform, false);
        _instance.transform.position = textPosition;
        _instance.SetText(text);
    }

    static void CreatingRandomFloatingText(GameObject _canvas, string text, Transform location, FloatingText _floatingText)
    {
        FloatingText _instance = Instantiate(_floatingText);
        Vector2 textPosition = new Vector2(location.position.x + Random.Range(-0.5f, .5f), location.position.y + Random.Range(-.5f, .5f));
        _instance.transform.SetParent(_canvas.transform, false);
        _instance.transform.position = textPosition;
        _instance.SetText(text);
    }

    static void CreateFloatingText(GameObject _canvas, string text, Transform location, FloatingText _floatingText)
    {
        FloatingText _instance = Instantiate(_floatingText);
        Vector2 textPosition = new Vector2(location.position.x + 0.5f, location.position.y);
        _instance.transform.SetParent(_canvas.transform, false);
        _instance.transform.position = textPosition;
        _instance.SetText(text);
    }



}
