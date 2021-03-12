using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour
{
    public bool TouchOnColliderAndTileMaps(float fingerId, List<GameObject> gameObjects, List<GameObject> _tileMapsGameObj)
    {
        Touch touch = FindTouchWithFingerId(fingerId);
        if (touch.phase == TouchPhase.Canceled)
            return false;


        if (TouchOnColliders(touch, gameObjects))
            return true;
        else if (TouchOnColliders(touch, _tileMapsGameObj))
            return true;

        return false;
    }

    public bool TouchOnColliders(Touch touch, List<GameObject> gameObjects)
    {

        foreach (GameObject _gameObj in gameObjects)
        {
            if (TouchOnCollider(touch, _gameObj))
                return true;
        }

        return false;
    }



    public bool TouchOnWorldObjectWithPhaseControl(GameObject _gameObj, TouchPhase touchPhase) // Daha sonra silinebilir.
    {
        Touch touch = FindTouchWithTouchPhase(touchPhase);
        if (touch.phase == TouchPhase.Canceled)
            return false;

        return TouchOnCollider(touch, _gameObj);
    }

    public float TouchBeganOnUIObject(GameObject _gameObj) // If it doesnt than that function returns cancelled touch. 
    {

        Touch touch = FindTouchWithTouchPhase(TouchPhase.Began);
        if (touch.phase == TouchPhase.Canceled)
            return -1;


        if (TouchOnUIObject(touch, _gameObj))
            return touch.fingerId;
        else
            return -1;
    }

    public bool TouchOnUIObjectWithPhaseControl(GameObject _gameObj, TouchPhase touchPhase)
    {
        Touch touch = FindTouchWithTouchPhase(touchPhase);
        if (touch.phase == TouchPhase.Canceled)
            return false;

        return TouchOnUIObject(touch, _gameObj);
    }

    public Touch FindTouchWithFingerId(float fingerId)
    {
        Touch returnTouch = new Touch();
        returnTouch.phase = TouchPhase.Canceled;
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == fingerId)
            {
                return touch;
            }
        }

        return returnTouch;
    }

    public Touch FindTouchWithTouchPhase(TouchPhase touchPhase)
    {
        Touch returnTouch = new Touch();
        returnTouch.phase = TouchPhase.Canceled;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == touchPhase)
            {
                return touch;
            }
        }

        return returnTouch;
    }

    public bool TouchOnCollider(Touch touch, GameObject _gameObj)
    {
        Collider2D _collider2D = _gameObj.GetComponent<Collider2D>();

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        if (_collider2D == Physics2D.OverlapPoint(touchPos))
            return true;

        return false;
    }

    public void CollidersCollision(GameObject _gameObj)
    {
        Collider2D _collider2D = _gameObj.GetComponent<Collider2D>();

        List<Collider2D> _colliders = new List<Collider2D>();
        int v = Physics2D.OverlapCollider(_collider2D, new ContactFilter2D().NoFilter(), _colliders);
        print(" colliders count :" + v + " -----------------------");
        foreach (Collider2D _currentCollider in _colliders)
        {
            print(_currentCollider.name);
        }
        print("-------------------------------");

        return;
    }

    public bool TouchOnUIObject(Touch touch, GameObject _gameObj)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0)
        {
            if (_gameObj.GetInstanceID() == results[0].gameObject.GetInstanceID())
                return true;
        }

        return false;
    }

}
