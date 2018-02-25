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

    private int numTimesSwitched;

	// Use this for initialization
	void Start () {
        numTimesSwitched = 0;
	}

    void OnTriggerEnter(Collider other)
    {
        
        GameObject temp = Instantiate(terrainPrefabs[Random.Range(0, terrainPrefabs.Length)], NextTerrain.position, Quaternion.identity, transform);
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
