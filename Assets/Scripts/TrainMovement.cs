﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour {

    [SerializeField] private float AccelerationPerFuel = 1.5f;
    [SerializeField] private float DecelerationPerSecond = 0.1f;
    [SerializeField] private float DefaultSpeed = 0.0f;

    [SerializeField] private Grabbable[] looseObjects;

    private float speed;
    public float maxSpeed;
    spedometer speedy;
    heatManagement heat;
	// Use this for initialization
	void Start () {
        speed = DefaultSpeed;
        speedy = GameObject.FindObjectOfType<spedometer>();
        heat = GetComponent<heatManagement>();
	}
	
	// Update is called once per frame
	void Update () {
        speed = Mathf.Max(speed - DecelerationPerSecond * Time.deltaTime, DefaultSpeed);
        transform.position += transform.forward * speed * Time.deltaTime;
        speedy.setSpeed(speed / maxSpeed);
        foreach(Grabbable obj in looseObjects)
        {
            obj.Translate(transform.forward * speed * Time.deltaTime);
        }

	}

    public void AddFuel()
    {
        speed += AccelerationPerFuel;
        heat.addFuel();
    }
}
