using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float fireRate = .15f;
    public float nextFire = float.MinValue;

    public GameObject _defaultFire; //Burasi resources.onload seklinde yapilicak.
    public Transform _gunPointer;


    public void Shoot(float angle)
    {
        GameObject currentFire = Instantiate(_defaultFire); // Mermiyi askerden çıkar.
        currentFire.GetComponent<Projectile>().Angle = angle;
        currentFire.transform.position = new Vector3(_gunPointer.position.x, _gunPointer.position.y, _gunPointer.position.z);
        currentFire.transform.eulerAngles = Vector3.forward * angle;
        nextFire = Time.time + fireRate;
    }


}
