using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HealthBarImage;
    
    private void Start()
    {
        HealthBarImage = GetComponent<Image>();
    }

    public void SetHealthBarValue(float value)
    {
        Debug.Log(value);
        HealthBarImage.fillAmount = value;
        if (HealthBarImage.fillAmount < 0.3f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (HealthBarImage.fillAmount < .6f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }

    public float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    private void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }

}