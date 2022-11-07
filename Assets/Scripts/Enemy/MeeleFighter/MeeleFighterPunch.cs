using UnityEngine;

public class MeeleFighterPunch : MonoBehaviour
{
    private float damage = 15f; // bu degerler daha sonra belirlenicek.

    private bool hasEntered = false;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player") && !hasEntered)
        {
            //Burda alive scriptini cekip hasar verdir
            //Playerin damageTaking animasyonunu yap 
            //Dusmanlarinkinden farkli renkte popup hasar cikar  

            IAlive _aliveScript = _collision.gameObject.GetComponent<IAlive>();


            _aliveScript.CalculateDamage(damage);
            hasEntered = true;
        }
    }

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player") && hasEntered)
        {
            hasEntered = false;
        }
    }




}
