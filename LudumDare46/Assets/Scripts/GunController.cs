using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public ShootProfile shootProfile = null;
    public Transform nozzle = null;
    public bool isFiring = false;

    public IEnumerator Shoot()
    {
        for (int indx = 0; indx < shootProfile.noOfBullets; indx++)
        {      
            GameObject bullet = Instantiate(shootProfile.bulletPrefab, nozzle.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(nozzle.forward * shootProfile.bulletSpeed, ForceMode.Impulse);
            yield return new WaitForSeconds(shootProfile.fireRate);
        }

        yield return new WaitForSeconds(shootProfile.cooldownTime);
        isFiring = false;
    }
}
