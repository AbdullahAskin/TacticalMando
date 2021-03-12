using System;
using UnityEngine;

public static class CommonCalculateFunctions
{
    // Start is called before the first frame update


    // Update is called once per frame


    public static float FromDirectionToAngle(Vector2 direction)
    {
        float radian = (float)Math.Atan2(direction.y, direction.x);
        float angle = radian * Mathf.Rad2Deg;
        return angle;
    }

    public static Vector2 FromVectorsToDirection(Vector2 objectPosition, Vector2 currentPosition)
    {
        Vector2 characterWorldPosition = new Vector2(currentPosition.x, currentPosition.y);
        double dy = objectPosition.y - characterWorldPosition.y;
        double dx = objectPosition.x - characterWorldPosition.x;
        double angle = Math.Atan2(dy, dx);
        Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        return direction;
    }

    public static Vector2 FromAngleToDirection(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
        return direction;
    }


    public static float LeftPathCalculator(float currentAngle, float targetAngle) // Sadece soldan olan yolu hesaplar.
    {
        float biggerValue, smallerValue, leftPathValue;
        if (targetAngle > currentAngle)
        {
            biggerValue = targetAngle;
            smallerValue = currentAngle;
            leftPathValue = biggerValue - smallerValue;
        }
        else
        {
            biggerValue = currentAngle;
            smallerValue = targetAngle;
            leftPathValue = (360 - biggerValue) + smallerValue;
        }


        return leftPathValue;

    }

    public static float PositiveAngleConverter(float angle)
    {
        angle = angle >= 0 ? angle : angle + 360;
        return angle;
    }

    public static float AnimationLengthFinder(Animator _animator, String animationName) // suanlik kullanmiyorum.
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(animationName))
            {
                return clip.length;
            }
        }

        return 0;
    }


}
