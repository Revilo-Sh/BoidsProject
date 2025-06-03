using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Flock : MonoBehaviour
{
    [SerializeField] private GameObject flockUnitPrefab;
    [SerializeField] private int BoidAmount;
    [SerializeField] private int DiffentTypeAmount = 1;
    [SerializeField] private Vector3 spawnbounds;

    public GameObject[] allUnits { get; set; }



    private void Start()
    {
       GenerateUnits();
    }

    private void GenerateUnits()
    {
        allUnits = new GameObject[BoidAmount];
        for (int i = 0; i < allUnits.Length; i++) {
            var randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnbounds.x, randomVector.y * spawnbounds.y, randomVector.z * spawnbounds.z);
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPosition, rotation);
        }
    }
}
