using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSwitcher : MonoBehaviour {

    [SerializeField] private Transform PreviousTerrain;
    [SerializeField] private Transform CurrentTerrain;
    [SerializeField] private Transform NextTerrain;

    [SerializeField] private GameObject[] terrainPrefabs;
    [SerializeField] private float terrainLength = 500.0f;
    [SerializeField] private TrainMovement train;
    [SerializeField] private float DestroyDelay = 0.1f;

    [SerializeField] private int baseNumWaterSpawns = 1;
    [SerializeField] private float bonusSpawnPerMissing = 0.25f;
    [SerializeField] private waterMeter water;
    [SerializeField] private Vector3 baseLocation;
    [SerializeField] private Vector3 maxOffset;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private Vector3 rotCorrection;

    private int numTimesSwitched;

	// Use this for initialization
	void Start () {
        numTimesSwitched = 0;
	}

    void OnTriggerEnter(Collider other)
    {
        
        GameObject temp = Instantiate(terrainPrefabs[Random.Range(0, terrainPrefabs.Length)], NextTerrain.position, Quaternion.identity, transform);
        int numSpawns = baseNumWaterSpawns + (int)Mathf.Floor((1.0f - water.waterValue) / bonusSpawnPerMissing);
        for(int count = 0; count < numSpawns; count++)
        {
            Instantiate(waterPrefab, temp.transform.position + baseLocation + Random.Range(0.0f, 1.0f) * maxOffset, Quaternion.Euler(rotCorrection), temp.transform);
        }
        PreviousTerrain.position += transform.right * terrainLength;
        CurrentTerrain.position += transform.right * terrainLength;
        NextTerrain.position += transform.right * terrainLength;
        Destroy(PreviousTerrain.gameObject, DestroyDelay);

        PreviousTerrain = CurrentTerrain;
        CurrentTerrain = NextTerrain;
        NextTerrain = temp.transform;

        train.Translate(transform.right * terrainLength);
        numTimesSwitched++;
    }

    public int GetSwitchCount()
    {
        return numTimesSwitched;
    }
}
