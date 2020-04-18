using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public List<GameObject> buildings = new List<GameObject>();
    public float Xoffset = 0.0f;
    public float yOffset = 2.5f;
    public float zOffset = -3.0f;
    void Start()
    {
        Random.InitState(42);

        //Generate Buildings
        for(int i = 0; i <1000; i++)
        {
            int index = Random.Range(0, 3);

            GameObject building = Instantiate(buildings[index],new Vector3(Xoffset,yOffset,zOffset),Quaternion.identity);

            float size = building.GetComponent<Building>().size;
        }
    }
}
