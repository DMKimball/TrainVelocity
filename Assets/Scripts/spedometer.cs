using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spedometer : MonoBehaviour {

    public float maxDegrees;
    public float minDegrees;
    [Range(0, 100)]
    public float speedLevel = 100;
    float currAngle;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void setSpeed(float newSpeed)
    {
        speedLevel = Mathf.Clamp01(newSpeed) * 100;
        currAngle = Mathf.Clamp(minDegrees + (speedLevel / 100 * (maxDegrees - minDegrees)), minDegrees, maxDegrees);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currAngle);
    }
}
