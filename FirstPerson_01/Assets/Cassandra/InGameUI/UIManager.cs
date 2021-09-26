using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text TxtAmmo;
    public GameObject player;

    //updating ammo count
    public void UpdateAmmo(int Count, int TotalAmmo, bool isReloading)
    {
        if (isReloading)
        {
            TxtAmmo.text = "Reloading...";
        }
        else
        {
            TxtAmmo.text = "Ammo: " + Count + " | " + TotalAmmo;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmo(player.GetComponent<Gun>().curAmmo,player.GetComponent<Gun>().ammoAmount , player.GetComponent<Gun>().isReloading);
    }
}
