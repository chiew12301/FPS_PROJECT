using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtCurAmmo;
    [SerializeField]
    private Text txtAmmoAmount;
    [SerializeField]
    private Text txtReload;
    public GameObject player;
    [SerializeField]
    private RawImage bulletIcon;

    //updating ammo count
    public void UpdateAmmo(int Count, int TotalAmmo, bool isReloading)
    {
        if (isReloading)
        {
            txtCurAmmo.gameObject.SetActive(false);
            txtAmmoAmount.gameObject.SetActive(false);
            bulletIcon.gameObject.SetActive(false);
            txtReload.gameObject.SetActive(true);
        }
        else if(player.GetComponent<PlayerProfiler>().GetUsingMedkit())
        {
            txtCurAmmo.gameObject.SetActive(false);
            bulletIcon.gameObject.SetActive(false);
            txtReload.gameObject.SetActive(false);
            txtAmmoAmount.gameObject.SetActive(true);
            txtAmmoAmount.text = "Medkit: " + player.GetComponent<PlayerProfiler>().ReturnMedKitAmount() + "";
        }
        else
        {
            txtCurAmmo.text = Count + "";
            txtAmmoAmount.text = TotalAmmo + "";
            txtCurAmmo.gameObject.SetActive(true);
            txtAmmoAmount.gameObject.SetActive(true);
            txtReload.gameObject.SetActive(false);
            bulletIcon.gameObject.SetActive(true);
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
