using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public GameObject pointer;
    public GameObject target; //where want the pointer point towards
    public GameObject player;
    public RectTransform compassLine;
    RectTransform rect;

    void Start()
    {
        rect = pointer.GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3[] v = new Vector3[4];
        compassLine.GetLocalCorners(v);
        float pointerScale = Vector3.Distance(v[1], v[2]); //both bottom corners

        Vector3 direction = target.transform.position - player.transform.position;
        float angleToTarget = Vector3.SignedAngle(player.transform.forward, direction, player.transform.up);
        angleToTarget = Mathf.Clamp(angleToTarget, -90, 90) / 180.0f * pointerScale;
        rect.localPosition = new Vector3(angleToTarget, rect.localPosition.y, rect.localPosition.z);
    }
}
