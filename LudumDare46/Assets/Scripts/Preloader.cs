using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloader : MonoBehaviour
{
    public static Preloader instance = null;
    public List<GameObject> persistantObjects = new List<GameObject>();

    private void Awake()
    {
        if (instance)
            return;

        instance = this;
        DontDestroyOnLoad(this);
        Initialize();
    }

    private void Initialize()
    {
        Debug.Log("init");
        for (int indx = 0; indx < persistantObjects.Count; indx++)
        {
            GameObject _go = Instantiate(persistantObjects[indx], Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(_go);
        }
    }
}
