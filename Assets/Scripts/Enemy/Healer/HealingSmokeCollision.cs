using UnityEngine;

public class HealingSmokeCollision : MonoBehaviour
{
    float healingAmount = 2f;

    void OnParticleCollision(GameObject other)
    {
        IAlive _aliveScript = other.GetComponent<IAlive>();
        _aliveScript.Heal(healingAmount);
    }

}
