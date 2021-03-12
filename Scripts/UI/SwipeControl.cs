using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool swipeLeft, swipeRight;
    private bool isDraging = false;
    private float startTouch, swipeDelta;
    private bool inSwipeControl = false;

    void Start()
    {
        Reset();
    }
    public float SwipeDelta
    {
        get { return swipeDelta; }
    }

    public bool SwipeLeft
    {
        get { return swipeLeft; }
    }

    public bool SwipeRight
    {
        get { return swipeRight; }
    }


    void Update()
    {
        swipeLeft = swipeRight = false;
        if (inSwipeControl)
        {
            //Debug.Log("Button is working");
            #region forMobile

            if (Input.touchCount > 0) // Sadece ilk parmak dokunuşunu sayıyor daha sonra değiştir.
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    startTouch = Input.touches[0].position.x;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    Reset();
                }
            }

            #endregion

            #region forPc

            if (Input.GetMouseButtonDown(0))
            {
                isDraging = true;
                startTouch = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Reset();
            }


            #endregion
        }

        if (isDraging)
        {
            if (Input.touchCount > 0)
            {
                swipeDelta = Input.touches[0].position.x - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = Input.mousePosition.x - startTouch;
            }
        }

        //Did we cross the dead zone ?
        if (Mathf.Abs((int)swipeDelta) > 150)
        {
            if (swipeDelta < 0)
            {
                swipeLeft = true;
            }
            else
            {
                swipeRight = true;
            }
            Reset();
        }
    }




    void Reset()
    {
        startTouch = swipeDelta = 0;
        isDraging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        inSwipeControl = true;
        UIControl.inUIControl = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        inSwipeControl = false;
        UIControl.inUIControl = false;
    }
}
