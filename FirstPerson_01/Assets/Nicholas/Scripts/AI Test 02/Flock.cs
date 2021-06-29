using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] private FlockUnit flockUnitPrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBound;
    [SerializeField] private GameObject target;



    [Header("Speed Setup")]
    [Range(0, 10)]
    [SerializeField] private float _minSpeed;
    public float minSpeed { get { return _minSpeed; } }
    [Range(0, 10)]
    [SerializeField] private float _maxSpeed;
    public float maxSpeed { get { return _maxSpeed; } }



    [Header("Detection Distance")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionDist;
    public float cohesionDistance { get { return _cohesionDist; } }

    [Range(0, 10)]
    [SerializeField] private float _avoidanceDist;
    public float avoidanceDistance { get { return _avoidanceDist; } }

    [Range(0, 10)]
    [SerializeField] private float _alignmentDist;
    public float alignmentDistance { get { return _alignmentDist; } }

    [Range(0, 100)]
    [SerializeField] private float _boundsDist;
    public float boundsDistance { get { return _boundsDist; } }

    [Range(0, 10)]
    [SerializeField] private float _obstacleDist;
    public float obstacleDistance { get { return _obstacleDist; } }



    [Header("Behavior Weights")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionWeight;
    public float cohesionWeight { get { return _cohesionWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _avoidanceWeight;
    public float avoidanceWeight { get { return _avoidanceWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _alignmentWeight;
    public float alignmentWeight { get { return _alignmentWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _boundsWeight;
    public float boundsWeight { get { return _boundsWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _obstacleWeight;
    public float obstacleWeight { get { return _obstacleWeight; } }

    public FlockUnit[] allUnits { get; set; }

    private void Start()
    {
        SpawnUnits();
    }

    private void Update()
    {
        for (int i = 0; i < allUnits.Length; i++)
        {
            allUnits[i].MoveUnit();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, spawnBound);
    }

    private void SpawnUnits()
    {
        allUnits = new FlockUnit[flockSize];

        for (int i = 0; i < flockSize; i++)
        {
            var randVector = UnityEngine.Random.insideUnitSphere;
            //randVector = new Vector3(randVector.x * spawnBound.x, Mathf.Abs(randVector.y * spawnBound.y), randVector.z * spawnBound.z);
            randVector = new Vector3(randVector.x * spawnBound.x, 10, randVector.z * spawnBound.z);

            var spawnPos = transform.position + randVector;
            var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

            allUnits[i] = Instantiate(flockUnitPrefab, spawnPos, rotation, gameObject.transform);
            allUnits[i].AssignFlock(this);
            allUnits[i].InitialiseSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
