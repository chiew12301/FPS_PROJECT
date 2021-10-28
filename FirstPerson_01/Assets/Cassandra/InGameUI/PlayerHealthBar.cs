using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private float health;
    private float lerpTimer; 
    public float maxHealth = 100;
    public float chipSpeed = 2f; //how long the bar catchs up another bar
    public Image frontHealthBar;
    public Image backHealthBar;
    PlayerProfiler playerpro;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        playerpro = gameObject.GetComponent<PlayerProfiler>();
    }

    // Update is called once per frame
    void Update()
    {
        health = playerpro.currHealthPoint;

        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if(Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(Random.Range(5, 10));
        }
    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        frontHealthBar.fillAmount = hFraction;

        if (fillB > hFraction)
        {      
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }


    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }
    
}
