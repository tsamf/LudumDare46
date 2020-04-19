using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public List<GameObject> buildings = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public float Xoffset = -3.0f;
    public float yOffset = 2.5f;
    public float zOffset = 1.0f;
    public float spaceBetweenBuildings = 3.0f;

    void Start()
    {
        Random.InitState(45);

        //Generate Buildings
        for(int i = 0; i <1000; i++)
        {
            int index = Random.Range(0, 3);
            float size = buildings[index].GetComponent<Building>().size;
            Xoffset += size / 2.0f;
            GameObject building = Instantiate(buildings[index],new Vector3(Xoffset,yOffset,zOffset),Quaternion.identity);
            Xoffset += size / 2.0f + 3.0f;



        }
    }
}
