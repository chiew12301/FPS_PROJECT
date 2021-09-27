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
    public int ammoAmount;
    public float reloadTime = 2.6f;
    public bool isReloading = false;
    public bool isShooting;

    public Camera fpsCam;
    public GameObject impactEffect;
    public GameObject crosshair;
    public Transform player;
    public Transform gun;

    private float nextFire = 0.5f;
    public int bulletCount;
    float tempBloom;
    //float tempRotX;
    //Vector3 currRot;
    PauseMenu pM;

    private UIManager ui;
    private Crosshair ch;

    public Vector3[] recoilPattern { get; private set; } = new Vector3[30]
    {
        new Vector3(-1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0),
        new Vector3(0, -0.5f, 0),
        new Vector3(0, -0.5f, 0),
        new Vector3(0, -0.5f, 0),
        new Vector3(0, -0.5f, 0),
        new Vector3(0, -0.5f, 0),
        new Vector3(0, -0.5f, 0),
        new Vector3(0, -0.5f, 0),
        new Vector3(0, -0.5f, 0),
        new Vector3(0, -0.5f, 0),
        new Vector3(0, -0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(0, 0.5f, 0)
    };

    private void Start()
    {
        unZoomValue = Camera.main.fieldOfView;
        curAmmo = maxAmmo;

        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        ch = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        pM = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pM.pauseMenuUI.activeSelf)
        {
            crosshair.SetActive(true);
            if (player.GetComponent<PlayerMovementNew>().isRunning)
            {
                bloomRange = 20f;
            }
            else if (player.GetComponent<PlayerMovementNew>().isWalking)
            {
                bloomRange = 16f;
            }
            else
            {
                bloomRange = 12f;
            }
            if (isReloading)
                return;
            if (curAmmo <= 0 || Input.GetKeyDown(KeyCode.R) && curAmmo < maxAmmo)
            {
                StartCoroutine(Reload());
                return;
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Zoom();
            }
            else
            {
                UnZoom();
            }


            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
                if(isReloading)
                {
                    isShooting = false;
                    fpsCam.GetComponent<CameraControl>().SetGunRotation(Vector3.Lerp(fpsCam.GetComponent<CameraControl>().gunRotation, Vector3.zero, fireRate * Time.deltaTime));
                }
                else
                {
                    isShooting = true;
                }
            }
            else if (!Input.GetKey(KeyCode.Mouse0))
            {
                bulletCount = 0;
                isShooting = false;
                fpsCam.GetComponent<CameraControl>().SetGunRotation(Vector3.Lerp(fpsCam.GetComponent<CameraControl>().gunRotation, Vector3.zero, fireRate * Time.deltaTime));
            }
        }
        else
        {
            crosshair.SetActive(false);
        }
    }

    IEnumerator Reload()
    {
        bulletCount = 0;
        UnZoom();
        isReloading = true;
        isZoom = false;
        int tempAmmo;
        tempAmmo = maxAmmo - curAmmo;
        AudioManager.instance.Play("Reload", "SFX");
        yield return new WaitForSeconds(reloadTime);
        if(ammoAmount >= 30)
        {
            curAmmo = maxAmmo;
        }
        else
        {
            curAmmo += ammoAmount;
        }

        if(ammoAmount > 0)
        {
            ammoAmount -= tempAmmo;
        }
        else
        {
            ammoAmount = 0;
        }
        isReloading = false;
        bulletCount = 0;
    }

    IEnumerator HitFeedback()
    {
        ch.hitFeedback.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ch.hitFeedback.SetActive(false);
    }

    void Shoot()
    {
        RaycastHit hit;
        //bloom
        Vector3 bloom = fpsCam.transform.position + fpsCam.transform.forward * 500f;
        if(bulletCount <= 9)
        {
            tempBloom = bloomRange - (bloomRange / (bulletCount+1));
            bloom += tempBloom * fpsCam.transform.up;
        }
        else
        {
            bloom += bloomRange * fpsCam.transform.up;
            bloom += Random.Range(-bloomRange, bloomRange) * fpsCam.transform.right;
        }
        //bloom += Random.Range(-bloomRange, bloomRange) * fpsCam.transform.up;
        //bloom += Random.Range(-bloomRange, bloomRange) * fpsCam.transform.right;
        bloom -= fpsCam.transform.position;
        bloom.Normalize();

        //Recoil
        fpsCam.GetComponent<CameraControl>().SetGunRotation(fpsCam.GetComponent<CameraControl>().gunRotation + recoilPattern[bulletCount]);

        /*if (Physics.Raycast(fpsCam.transform.position, bloom, out hit, range))
        {
            Debug.Log(hit.transform.name);
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }*/
        if (Physics.Raycast(fpsCam.transform.position, bloom, out hit, range))
        {
            AudioManager.instance.Play("Shoot", "SFX");
            GameObject obj = ObjectPooling.current.GetPooledObject();
            if (obj == null) return;
            if (hit.transform.tag != "Border")
            {
                obj.transform.position = hit.point;
                obj.SetActive(true);
            }
            TargetScript target = hit.transform.GetComponent<TargetScript>();
            curAmmo--;
            bulletCount++;
            //ui.UpdateAmmo(curAmmo);
            if (target != null)
            {
                target.TakeDamage(damage);
                StartCoroutine(HitFeedback());
            }
        }
    }

    void Zoom()
    {
        Camera.main.fieldOfView = zoomValue;
        gun.localPosition = new Vector3(0, -0.168f, gun.localPosition.z);
    }

    void UnZoom()
    {
        Camera.main.fieldOfView = unZoomValue;
        gun.localPosition = new Vector3(0.5f, -0.244f, gun.localPosition.z);
    }

    /*public void Recoil(bool isShooting,float rotX, Transform cam)
    {
        if (isShooting)
        {
            cam.localRotation = Quaternion.Euler(currRot.x, currRot.y, currRot.z);
        }
        else
        {
            cam.localRotation = Quaternion.Euler(rotX, 0, 0);
        }
    }*/
}
