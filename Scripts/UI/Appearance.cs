
using UnityEngine;
using UnityEngine.UI;

public class Appearance : MonoBehaviour
{
    private SwipeControl swipeControlScript;
    private Slots slotsScript;
    private Image[] slots;
    private int currentSlot = 0;
    private int numberOfSlots;


    void Start()
    {
        //Diziyi oluştur scripti al ve slot sayısını al.
        swipeControlScript = transform.GetComponent<SwipeControl>();
        slotsScript = transform.GetChild(0).GetComponent<Slots>();
        numberOfSlots = slotsScript.NumberOfSlots;
        slots = new Image[numberOfSlots];

        //Slotları al.
        for (int i = 0; i < numberOfSlots; i++)
        {
            slots[i] = transform.GetChild(0).transform.GetChild(i).GetComponent<Image>(); 
        }
        SortSlots();
    }

    public int CurrentSlot
    {
        get { return currentSlot; }
        set { currentSlot = value; }
    }

    void Update()
    {
        if (swipeControlScript.SwipeLeft)
        {
            swipeLeft();
        }
        else if (swipeControlScript.SwipeRight)
        {
            swipeRight();
        }
    }

    void SortSlots()
    {
        ResetSlots();
        if (currentSlot == 0)
        {
            slots[currentSlot].rectTransform.anchoredPosition = new Vector2(200, -50);
            slots[currentSlot].rectTransform.sizeDelta = new Vector2(100, 100);

            slots[currentSlot + 1].rectTransform.anchoredPosition = new Vector2(300, -50);
        }
        else if (currentSlot == (numberOfSlots - 1)) // currentSlot index hesabıyla numberOfslot sayı hesabıyla gidiyor.
        {
            slots[currentSlot - 1].rectTransform.anchoredPosition = new Vector2(85, -50);

            slots[currentSlot].rectTransform.anchoredPosition = new Vector2(200, -50);
            slots[currentSlot].rectTransform.sizeDelta = new Vector2(100, 100);
        }
        else if (currentSlot > 0 && currentSlot < numberOfSlots)
        {
            slots[currentSlot - 1].rectTransform.anchoredPosition = new Vector2(85, -50);

            slots[currentSlot].rectTransform.anchoredPosition = new Vector2(200, -50);
            slots[currentSlot].rectTransform.sizeDelta = new Vector2(100, 100);

            slots[currentSlot + 1].rectTransform.anchoredPosition = new Vector2(300, -50);
        }
    }

    void ResetSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].rectTransform.anchoredPosition = new Vector2(500, -50);
            slots[i].rectTransform.sizeDelta = new Vector2(50, 50);
        }
    }

    void swipeRight()
    {
        if (currentSlot != 0)
        {
            currentSlot--;
            SortSlots();
        }
    }
    void swipeLeft()
    {
        if (currentSlot != (numberOfSlots - 1))
        {
            currentSlot++;
            SortSlots();
        }
    }
}
