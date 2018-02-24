using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour {

    [SerializeField] private float AccelerationPerFuel = 1.5f;
    [SerializeField] private float DecelerationPerSecond = 0.1f;
    [SerializeField] private float DefaultSpeed = 0.0f;

    [SerializeField] private Transform[] looseObjects;

    private float speed;

	// Use this for initialization
	void Start () {
        speed = DefaultSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        speed = Mathf.Max(speed - DecelerationPerSecond * Time.deltaTime, DefaultSpeed);
        transform.position += transform.forward * speed * Time.deltaTime;
        
        foreach(Transform obj in looseObjects)
        {
            obj.position += transform.forward * speed * Time.deltaTime;
        }
	}

    public void AddFuel()
    {
        speed += AccelerationPerFuel;
    }
}
