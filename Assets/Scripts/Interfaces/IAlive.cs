using UnityEngine;


public interface IAlive
{
    void Dead();
    void CalculateDamage(float damage);

    void Heal(float heal);
}

