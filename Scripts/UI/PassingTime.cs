using UnityEngine;
using UnityEngine.UI;

public class PassingTime : MonoBehaviour
{
    private float timeCount = 0;
    private Text timeText;

    void Start()
    {
        timeText = transform.GetChild(0).GetComponent<Text>();
        InvokeRepeating("TimePassing", 1f, 1f);
    }


    void TimePassing()
    {
        timeText.text = timeCount.ToString();
        timeCount++;
    }
}
