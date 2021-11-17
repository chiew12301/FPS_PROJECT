using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProfiler : MonoBehaviour
{
    public Item MedKit;
    public int currHealthPoint;
    public int maxHealthPoint = 100;
    public int medkitCurrentAmount;
    [SerializeField]
    private bool usingMedkit;
    public bool isHealing;
    [SerializeField]
    GameObject bloodUI;

    //[SerializeField]
    //float throwForce = 30f;
    //public GameObject molotovPrefab;

    [SerializeField]
    GameObject playerCam;

    public Animator dieScene;

    // Start is called before the first frame update
    void Start()
    {
        currHealthPoint = 80;
        Inventory.instance.Add(MedKit);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMedKitAmount();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(!usingMedkit)
            {
                usingMedkit = true;
            }
            else if(usingMedkit)
            {
                usingMedkit = false;
            }
        }
        //if(Input.GetKeyDown(KeyCode.G))
        //{
        //    ThrowUtility();
        //}
        if(usingMedkit)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                UseMedkit();
            }
        }
        if(currHealthPoint <= 0)
        {
            StartCoroutine(Die());
        }
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
        usingMedkit = false;
    }

    public void CheckMedKitAmount()
    {
        medkitCurrentAmount = Inventory.instance.getItemStackAmount(MedKit);
    }

    public int ReturnMedKitAmount()
    {
        return Inventory.instance.getItemStackAmount(MedKit);
    }

    void TakeDamage(int damage)
    {
        currHealthPoint -= damage;
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            AudioManager.instance.Play("Hurt01", "SFX");
        }
        else
        {
            AudioManager.instance.Play("Hurt02", "SFX");
        }
        StartCoroutine(bleed());
    }

    IEnumerator bleed()
    {
        bloodUI.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        bloodUI.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag("EnemyCollider"))
        {
            Debug.Log("CROW COLLISION");
            TakeDamage(5);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("EnemyCollider"))
        {
            Debug.Log("Explosion hit");
            TakeDamage(10);
        }
    }

    public bool GetUsingMedkit()
    {
        return usingMedkit;
    }

    IEnumerator Die()
    {
        if (dieScene.GetCurrentAnimatorStateInfo(0).IsName("DieScene") != true)
        {
            dieScene.Play("DieScene");
        }
        AudioManager.instance.Play("Dead", "SFX");
        yield return new WaitForSeconds(4.0f);
        //Load Scene
        SceneManager.LoadScene(0);
    }

    //void ThrowUtility()
    //{
    //    GameObject molotov = Instantiate(molotovPrefab, playerCam.transform.position, transform.rotation);
    //    Rigidbody rb = molotov.GetComponent<Rigidbody>();
    //    rb.AddForce(playerCam.transform.forward * throwForce);
    //}
}
