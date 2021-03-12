using System;
using System.Collections;
using UnityEngine;

public class TurretRotate : MonoBehaviour
{
    private float rotateSpeed = 20.0f;
    private float targetPathValue;
    protected bool turretRotateIsComplete = true;
    public Transform _transform;

    public void RotateTurret(float angle)
    {
        if (turretRotateIsComplete)
        {
            StartCoroutine("TurretRotatingEffect", angle);
        }
    }
    public IEnumerator TurretRotatingEffect(float targetAngle)
    {
        turretRotateIsComplete = false;
        float currentAngle, addedAngleCounter = 0;
        Vector3 tempRotateSpeed = Vector3.forward * rotateSpeed;

        currentAngle = CommonCalculateFunctions.PositiveAngleConverter(_transform.eulerAngles.z);
        targetAngle = CommonCalculateFunctions.PositiveAngleConverter(targetAngle);

        if (currentAngle == targetAngle)
        {
            turretRotateIsComplete = true;
            yield break;
        }

        bool isItTurningLeftControl = IsItTurningLeft(currentAngle, targetAngle);
        if (isItTurningLeftControl)
        {
            while (true)
            {
                if ((addedAngleCounter + tempRotateSpeed.z) >= targetPathValue)
                {
                    _transform.eulerAngles = _transform.eulerAngles + Vector3.forward * (targetPathValue - addedAngleCounter);
                    turretRotateIsComplete = true;
                    yield break;
                }
                _transform.eulerAngles = _transform.eulerAngles + tempRotateSpeed;
                addedAngleCounter += tempRotateSpeed.z;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            tempRotateSpeed = -tempRotateSpeed;
            while (true)
            {
                if ((addedAngleCounter + Math.Abs(tempRotateSpeed.z)) >= targetPathValue)
                {
                    _transform.eulerAngles = _transform.eulerAngles + Vector3.forward * (addedAngleCounter - targetPathValue);
                    turretRotateIsComplete = true;
                    yield break;
                }
                _transform.eulerAngles = _transform.eulerAngles + tempRotateSpeed;
                addedAngleCounter += Math.Abs(tempRotateSpeed.z); // Value - olduğu için cıkarmak zorundayım.
                yield return new WaitForSeconds(0.05f);
            }
        }
    }



    public bool IsItTurningLeft(float currentAngle, float targetAngle)
    {
        float leftPath = CommonCalculateFunctions.LeftPathCalculator(currentAngle, targetAngle);
        if (leftPath <= 180)
        {
            targetPathValue = leftPath;
            return true;
        }
        else
        {
            float rightPath = 360 - leftPath;
            targetPathValue = rightPath;
            return false;
        }
    }
}
