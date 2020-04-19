using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    public ShootProfile shootProfile = null;
    public Transform nozzle = null;
    public bool isFiring = false;
    public Vector3 target = Vector3.zero;

    public IEnumerator Shoot()
    {
        for (int indx = 0; indx < shootProfile.noOfBullets; indx++)
        {
            GameObject bullet = Instantiate(shootProfile.bulletPrefab, nozzle.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(nozzle.forward * shootProfile.bulletSpeed, ForceMode.Impulse);
            bullet.GetComponent<BulletController>().aliveTime = shootProfile.bulletAliveTime;
            yield return new WaitForSeconds(shootProfile.fireRate);
        }

        yield return new WaitForSeconds(shootProfile.cooldownTime);
        isFiring = false;
    }

    private void Update()
    {
        if (target== Vector3.zero)
            return;

        transform.LookAt(target);
    }
}
