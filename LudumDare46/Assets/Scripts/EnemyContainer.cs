using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    public static EnemyContainer instance = null;
    public int count = -1;

    private void Awake()
    {
        instance = this;
    }
}
