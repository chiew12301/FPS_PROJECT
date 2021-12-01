using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float horizontalRadius;
    [SerializeField] private float verticalRadius;
    private Vector3 waypoint;

    // Start is called before the first frame update
    void Start()
    {
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * moveSpeed * Time.deltaTime;

        if ((transform.position - waypoint).magnitude <= 0.5)
        {
            Wander();
        }
    }

    private void Wander()
    {
        waypoint = new Vector3(Random.Range(transform.position.x - horizontalRadius, transform.position.x + horizontalRadius), 
            Random.Range(transform.position.y - verticalRadius, transform.position.y + verticalRadius), 
            Random.Range(transform.position.z - horizontalRadius, transform.position.z + horizontalRadius));

        transform.LookAt(waypoint);
        //transform.position += waypoint * moveSpeed * Time.deltaTime;
    }
}
