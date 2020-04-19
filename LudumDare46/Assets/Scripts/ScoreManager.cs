using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    private int inititalScore = 0;
    public static ScoreManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = inititalScore;
    }

    public void UpdateScore(int val)
    {
        Debug.Log(currentScore);
        currentScore += val;
    }
}
