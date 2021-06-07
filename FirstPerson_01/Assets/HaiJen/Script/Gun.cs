using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float zoomValue = 15f;
    public float unZoomValue;
    public bool isZoom = false;

    public Camera fpsCam;
    public GameObject impactEffect;

    private float nextFire = 0f;

    private void Start()
    {
        unZoomValue = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && !isZoom)
        {
            Zoom();
            isZoom = true;
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && isZoom)
        {
            UnZoom();
            isZoom = false;
        }

        if(Input.GetKey(KeyCode.Mouse0)&& Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        //bloom
        Vector3 bloom = fpsCam.transform.position + fpsCam.transform.forward * 500f;
        bloom += Random.Range(-8f, 8f) * fpsCam.transform.up;
        bloom += Random.Range(-8f, 8f) * fpsCam.transform.right;
        bloom -= fpsCam.transform.position;
        bloom.Normalize();

        if (Physics.Raycast(fpsCam.transform.position, bloom, out hit, range))
        {
            Debug.Log(hit.transform.name);
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
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
