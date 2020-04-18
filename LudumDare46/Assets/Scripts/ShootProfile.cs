using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootProfile", menuName = "ScriptableObject/ShootProfile", order =1)]
public class ShootProfile : ScriptableObject
{
    public float fireRate = 1f;
    public int noOfBullets = 1;
    public float bulletSpeed = 10f;
    public float bulletAliveTime = 4f;
    public float cooldownTime = 2f;
    public GameObject bulletPrefab; 
}
