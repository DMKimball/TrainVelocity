using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour {

    [SerializeField] private float AccelerationPerFuel = 1.5f;
    [SerializeField] private float DecelerationPerSecond = 0.1f;
    [SerializeField] private float DefaultSpeed = 0.0f;
    [SerializeField] private SoundFade WindSoundFade;

    [SerializeField] private Grabbable[] looseObjects;

    private float speed;
    public float maxSpeed;
    spedometer speedy;
    heatManagement heat;
	// Use this for initialization
	void Start () {
        speed = DefaultSpeed;
        speedy = GetComponentInChildren<spedometer>();
        heat = GetComponent<heatManagement>();
	}
	
	// Update is called once per frame
	void Update () {
        // Set the volume of the persistent background loop based on speed.
        // Volume ranges from 0 to 1.
        GetComponentInChildren<SoundFade>().SetVolume(speed / maxSpeed * 0.5f);
        // Set the volume of the persistent outdoor wind sound.
        // Volume ranges from 0 to 1.
        WindSoundFade.SetVolume(speed / maxSpeed);

        // Patrick testing
        /*if (Input.GetButtonDown("Fire1")) {
            //GameObject.Find("[CameraRig]").transform.position
            //    += (new Vector3(0.1f, 0.0f, 0.0f));
            speed += maxSpeed/5;
        }
        if (Input.GetButtonDown("Fire2")) {
            //GameObject.Find("[CameraRig]").transform.position
            //    += (new Vector3(-0.1f, 0.0f, 0.0f));
            speed -= maxSpeed/5;
        }
        GameObject.Find("[CameraRig]").transform.position = 
            new Vector3(transform.position.x, transform.position.y,
                GameObject.Find("train4").transform.position.z);
        */

        speed = Mathf.Max(speed - DecelerationPerSecond * Time.deltaTime, DefaultSpeed);
        Translate(transform.forward * speed * Time.deltaTime);
        speedy.setSpeed(speed / maxSpeed);

	}

    public void AddFuel()
    {
        speed += AccelerationPerFuel;
        heat.addFuel();
    }

    public void Translate(Vector3 translation)
    {
        transform.position += translation;
        foreach (Grabbable obj in looseObjects)
        {
            obj.Translate(translation);
        }
    }
}
