using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfiler : MonoBehaviour
{
    public Item MedKit;
    public int currHealthPoint;
    public int maxHealthPoint = 100;
    public int medkitCurrentAmount;
    private int medkitMaxAmount;
    public bool isHealing;

    // Start is called before the first frame update
    void Start()
    {
        currHealthPoint = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseMedkit();
        }
        CheckMedKitAmount();

    }

    public bool CheckMedKit()
    {
        if (Inventory.instance.CheckHaveItem(MedKit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int CheckHP()
    {
        return currHealthPoint;
    }

    public void UseMedkit()
    {
        if(currHealthPoint < maxHealthPoint && CheckMedKit())
        {
            StartCoroutine(Heal());
        }
    }

    IEnumerator Heal()
    {
        Inventory.instance.Remove(MedKit);
        isHealing = true;
        yield return new WaitForSeconds(2.0f);
        isHealing = false;
        currHealthPoint += 50;
        if (currHealthPoint >= maxHealthPoint)
        {
            currHealthPoint = maxHealthPoint;
        }
    }

    public void CheckMedKitAmount()
    {
        medkitCurrentAmount = Inventory.instance.getItemStackAmount(MedKit);
    }

    void TakeDamage(int damage)
    {
        currHealthPoint -= damage;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag("Crow"))
        {
            Debug.Log("CROW COLLISION");
            TakeDamage(5);
        }

        if (collision.transform.CompareTag("Explosion"))
        {
            Debug.Log("BOOOMM");
            TakeDamage(10);
        }
    }
}
