using UnityEngine;


public interface IAlive
{
    void Dead();
    void DamageTakingCalculations(float damage);

    void Heal(float heal);
}

