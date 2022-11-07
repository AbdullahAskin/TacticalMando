using System.Collections;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    private Transform cameraTransform;
    private Player playerScript;

    private bool shaking = false;
    private float smoothSpeed = 0.125f;
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        playerScript = Object.FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        shakeControl();
        cameraPositionControl();
    }

    void shakeControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CameraShake(.25f,.1f));
        }
    }

    void cameraPositionControl()
    {
        if (!shaking)
        {
            cameraTransform.position = playerScript.transform.position;
        }
    }

    IEnumerator CameraShake(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        shaking = true;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude + playerScript.transform.position.x;
            float y = Random.Range(-1f, 1f) * magnitude + playerScript.transform.position.y;

            //transform.position = new Vector2(x,y);
            Vector2 desiredPosition = new Vector2(x,y);
            Vector2 smoothedPosition = Vector2.Lerp(transform.position,desiredPosition,smoothSpeed);
            cameraTransform.position = smoothedPosition;
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        shaking = false;
    }
}
