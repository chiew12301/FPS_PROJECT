using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapWaypoint : MonoBehaviour
{
	public Transform MinimapCam;
	public float MinimapRadius; //size of the minimap camera
	Vector3 TempV3;

	void Update()
	{
		TempV3 = transform.parent.transform.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;
	}

	void LateUpdate()
	{
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, MinimapCam.position.x - MinimapRadius, MinimapRadius + MinimapCam.position.x),
			transform.position.y,
			Mathf.Clamp(transform.position.z, MinimapCam.position.z - MinimapRadius, MinimapRadius + MinimapCam.position.z)
		);
	}
}
