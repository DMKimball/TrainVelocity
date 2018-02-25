using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class waterMeter : MonoBehaviour {
    [Range(0,1)]
    public float waterValue;
    Slider meter;
	// Use this for initialization
	void Start () {
        meter = GetComponent<Slider>();
        updateSlider();
	}
	
	// Update is called once per frame
	void Update () {       
	}
    public void updateSlider()
    {
        meter.value = Mathf.Clamp01(waterValue);
    }
}
