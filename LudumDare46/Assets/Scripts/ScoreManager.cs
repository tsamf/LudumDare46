using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    private int inititalScore = 0;
    public static ScoreManager instance = null;
    public Action<int> onScoreChange;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        currentScore = inititalScore;
    }

    public void UpdateScore(int val)
    {
        currentScore += val;
        onScoreChange(currentScore);
    }
}
