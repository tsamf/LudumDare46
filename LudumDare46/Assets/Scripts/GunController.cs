using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public ShootProfile shootProfile = null;
    public Transform nozzle = null;
    public AudioSource shoot;
    public bool isFiring = false;

    public IEnumerator Shoot()
    {
        for (int indx = 0; indx < shootProfile.noOfBullets; indx++)
        {      
            GameObject bullet = Instantiate(shootProfile.bulletPrefab, nozzle.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(nozzle.forward * shootProfile.bulletSpeed, ForceMode.Impulse);
            bullet.GetComponent<BulletController>().aliveTime = shootProfile.bulletAliveTime;
            shoot.Play(0);
            yield return new WaitForSeconds(shootProfile.fireRate);
        }

        yield return new WaitForSeconds(shootProfile.cooldownTime);
        isFiring = false;
    }

    private void Update()
    {
        Vector3 gunLookPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z);
        gunLookPos = Camera.main.ScreenToWorldPoint(gunLookPos);
        transform.LookAt(gunLookPos, Vector3.forward);
    }
}
