using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] private FlockUnit flockUnitPrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBound;

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

    private void SpawnUnits()
    {
        allUnits = new FlockUnit[flockSize];

        for (int i = 0; i < flockSize; i++)
        {
            var randVector = UnityEngine.Random.insideUnitSphere;
            randVector = new Vector3(randVector.x * spawnBound.x, randVector.y * spawnBound.y, randVector.z * spawnBound.z);

            var spawnPos = transform.position + randVector;
            var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

            allUnits[i] = Instantiate(flockUnitPrefab, spawnPos, rotation);
            allUnits[i].AssignFlock(this);
            allUnits[i].InitialiseSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));
        }
    }
}
