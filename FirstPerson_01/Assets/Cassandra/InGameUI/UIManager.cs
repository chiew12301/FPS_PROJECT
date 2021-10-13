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
    public GameObject player;
    [SerializeField]
    private RawImage bulletIcon;

    //updating ammo count
    public void UpdateAmmo(int Count, int TotalAmmo, bool isReloading)
    {
        if (isReloading)
        {
            txtCurAmmo.text = 0 + "";
            txtAmmoAmount.text = (TotalAmmo+Count) + "";
            bulletIcon.gameObject.SetActive(false);
        }
        else
        {
            txtCurAmmo.text = Count + "";
            txtAmmoAmount.text = TotalAmmo + "";
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
