using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heatManagement : MonoBehaviour {
    public float heatLevel = 0;
    HeatMeter heatMeter;
    public int heatReductionRate;
    float heatReductionProgress=0;
	// Use this for initialization
	void Start () {
        heatMeter = GameObject.FindObjectOfType<HeatMeter>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (heatReductionProgress > heatReductionRate)
        {
            heatReductionProgress = 0;
            heatLevel=Mathf.Clamp(--heatLevel,0,100);
            heatMeter.setHeat(heatLevel / 100);
        }
        else
        {
            heatReductionProgress++;
        }
	}
    public void addFuel()
    {
        heatLevel += 15;
        heatMeter.setHeat(heatLevel / 100);
    }
}
