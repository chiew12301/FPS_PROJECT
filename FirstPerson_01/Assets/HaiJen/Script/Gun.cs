using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 5f;
    public float range = 100f;
    public float fireRate = 15f;
    public float zoomValue = 15f;
    public float unZoomValue;
    public bool isZoom = false;

    public float bloomRange = 8f;
    public int maxAmmo = 30;
    public int curAmmo;
    public float reloadTime = 2.6f;
    private bool isReloading = false;

    public Camera fpsCam;
    public GameObject impactEffect;
    public Transform player;

    private float nextFire = 0f;

    private UIManager ui;

    private void Start()
    {
        unZoomValue = Camera.main.fieldOfView;
        curAmmo = maxAmmo;

        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerMovementNew>().isRunning)
        {
            bloomRange = 16f;
        }
        else if (player.GetComponent<PlayerMovementNew>().isWalking)
        {
            bloomRange = 12f;
        }
        else
        {
            bloomRange = 8f;
        }
        if (isReloading)
            return;
        if(curAmmo<=0||Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetKey(KeyCode.Mouse1))
        {
            Zoom();
        }
        else 
        {
            UnZoom();
        }

        if(Input.GetKey(KeyCode.Mouse0)&& Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        UnZoom();
        isReloading = true;
        isZoom = false;
        AudioManager.instance.Play("Reload", "SFX");
        yield return new WaitForSeconds(reloadTime);
        curAmmo = maxAmmo;
        ui.UpdateAmmo(curAmmo);
        isReloading = false;
    }

    void Shoot()
    {
        RaycastHit hit;
        //bloom
        Vector3 bloom = fpsCam.transform.position + fpsCam.transform.forward * 500f;
        bloom += Random.Range(-bloomRange, bloomRange) * fpsCam.transform.up;
        bloom += Random.Range(-bloomRange, bloomRange) * fpsCam.transform.right;
        bloom -= fpsCam.transform.position;
        bloom.Normalize();

        /*if (Physics.Raycast(fpsCam.transform.position, bloom, out hit, range))
        {
            Debug.Log(hit.transform.name);
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }*/

        if (Physics.Raycast(fpsCam.transform.position, bloom, out hit, range))
        {
            AudioManager.instance.Play("Shooting", "SFX");
            GameObject obj = ObjectPooling.current.GetPooledObject();
            if (obj == null) return;
            obj.transform.position = hit.point;
            obj.SetActive(true);
            TargetScript target = hit.transform.GetComponent<TargetScript>();
            curAmmo--;
            ui.UpdateAmmo(curAmmo);
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    void Zoom()
    {
        Camera.main.fieldOfView = zoomValue;
    }

    void UnZoom()
    {
        Camera.main.fieldOfView = unZoomValue;
    }
}
