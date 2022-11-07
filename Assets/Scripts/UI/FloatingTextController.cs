
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    public static FloatingText scr_damageEnemy, scr_damageCharacter, scr_heal, scr_additionGold, scr_extractionGold, scr_round, scr_warning;
    public static GameObject GO_canvas, GO_messagesLocation;
    public void Start()
    {
        Initialize();
    }

    public static void Initialize()
    {
        scr_damageEnemy = Resources.Load<FloatingText>("UI/Common/PopUpTexts/EnemyDamageGO");
        scr_heal = Resources.Load<FloatingText>("UI/Common/PopUpTexts/HealGO");
        scr_damageCharacter = Resources.Load<FloatingText>("UI/Common/PopUpTexts/CharacterDamageGO");
        scr_additionGold = Resources.Load<FloatingText>("UI/Common/PopUpTexts/AddGoldGO");
        scr_extractionGold = Resources.Load<FloatingText>("UI/Common/PopUpTexts/ExtractionGoldGO");
        scr_warning = Resources.Load<FloatingText>("UI/Common/Texts/WarningGO");
        scr_round = Resources.Load<FloatingText>("UI/Common/Texts/RoundGO");
        GO_canvas = GameObject.Find(nameof(Canvas));
        GO_messagesLocation = GameObject.Find("Messages");
    }

    // Size dinamikligi ekle.
    public static void CreateFloatingDamageTextForEnemy(GameObject _canvas, string text, Transform location)
    {
        text = "-" + text;
        CreatingRandomFloatingText(_canvas, text, location, scr_damageEnemy);
    }

    public static void CreateFloatingDamageTextForCharacter(GameObject _canvas, string text, Transform location)
    {
        text = "-" + text;
        CreatingRandomFloatingText(_canvas, text, location, scr_damageCharacter);
    }

    public static void CreateFloatingHealTextForEnemy(GameObject _canvas, string text, Transform location)
    {
        text = "+" + text;
        CreatingRandomFloatingText(_canvas, text, location, scr_heal);

    }

    public static void AdditionFloatingGoldTextForEnemy(string text, Transform location)
    {
        text = "+" + text;
        CreateFloatingText(GO_canvas, text, location, scr_additionGold);

    }

    public static void ExtractionFloatingGoldTextForEnemy(string text, Transform location)
    {
        text = "-" + text;
        CreateFloatingText(GO_canvas, text, location, scr_extractionGold);
    }


    public static void CreateWarningText(string text)
    {
        CreateText(text, scr_warning);
    }

    public static void CreateRoundText(string text)
    {
        CreateText(text, scr_round);
    }

    static void CreateText(string text, FloatingText _floatingText)
    {
        FloatingText _instance = Instantiate(_floatingText);
        Vector2 textPosition = new Vector2(GO_messagesLocation.transform.position.x + 0.5f, GO_messagesLocation.transform.position.y);
        _instance.transform.SetParent(GO_canvas.transform, false);
        _instance.transform.position = textPosition;
        _instance.SetText(text);
    }

    static void CreatingRandomFloatingText(GameObject _canvas, string text, Transform location, FloatingText _floatingText)
    {
        if (!GO_canvas.activeSelf)
            return;

        FloatingText _instance = Instantiate(_floatingText);
        Vector2 textPosition = new Vector2(location.position.x + Random.Range(-0.5f, .5f), location.position.y + Random.Range(-.5f, .5f));
        _instance.transform.SetParent(_canvas.transform, false);
        _instance.transform.position = textPosition;
        _instance.SetText(text);
    }

    static void CreateFloatingText(GameObject GO_canvas, string text, Transform location, FloatingText _floatingText)
    {
        if (!GO_canvas.activeSelf)
            return;

        FloatingText scr_instance = Instantiate(_floatingText);
        Vector2 textPosition = new Vector2(location.position.x + 0.5f, location.position.y);
        scr_instance.transform.SetParent(GO_canvas.transform, false);
        scr_instance.transform.position = textPosition;
        scr_instance.SetText(text);
    }



}
