using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMeter : MonoBehaviour {
    public float maxDegrees;
    public float minDegrees;
    [Range(0,100)]
    public float heatLevel = 100;
    float currAngle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void setHeat(float newHeat)
    {
        heatLevel = Mathf.Clamp01(newHeat)*100;
        currAngle = Mathf.Clamp(minDegrees + (heatLevel / 100 * (maxDegrees - minDegrees)), minDegrees, maxDegrees);
        Debug.Log(currAngle);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currAngle);
    }
}
