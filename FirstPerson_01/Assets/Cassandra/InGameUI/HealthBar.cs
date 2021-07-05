using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image ImgHealthBar;

    public Text TxtHealth;

    public int Min;
    public int Max;
    private int mCurrentValue;
    private float mCurrentPercent;

    public int health;

    public void SetHealth(int health)
    {
        if(health != mCurrentValue)
        {
            if(Max - Min == 0)
            {
                mCurrentValue = 0;
                mCurrentPercent = 0;
            }
            else
            {
                mCurrentValue = health;

                mCurrentPercent = (float)mCurrentValue / (float)(Max - Min);
            }

            TxtHealth.text = string.Format("{0}", Mathf.RoundToInt(mCurrentPercent * 100));

            ImgHealthBar.fillAmount = mCurrentPercent;
        }
    }

    public float CurrentPercent
    {
        get { return mCurrentPercent; }
    }

    public int CurrentValue
    {
        get { return mCurrentValue; }
    }    

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
