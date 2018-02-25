using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour {
    
    [SerializeField] private float MinSpeed = 2.5f;
    [SerializeField] private float MaxSpeed = 150.0f;
    [SerializeField] private float MinAcceleration = 0.0f;
    [SerializeField] private float MaxAcceleration = 5.0f;
    [SerializeField] private float MinWaterUse = 0.005f;
    [SerializeField] private float MaxWaterUse = 0.05f;
    [SerializeField] private float RotateTime = 10.0f;
    [SerializeField] private SoundFade WindSoundFade;

    [SerializeField] private Grabbable[] looseObjects;
    [SerializeField] private heatManagement heatSystem;
    [SerializeField] private waterMeter waterSystem;

    private float speed;
    spedometer speedy;
    heatManagement heat;
    private bool flying;
    private float flightT;

	// Use this for initialization
	void Start () {
        speed = MinSpeed;
        speedy = GetComponentInChildren<spedometer>();
        heat = GetComponent<heatManagement>();
        flying = false;
	}
	
	// Update is called once per frame
	void Update () {
        // Set the volume of the persistent background loop based on speed.
        // Volume ranges from 0 to 1.
        GetComponentInChildren<SoundFade>().SetVolume(speed / MaxSpeed * 0.5f);
        // Set the volume of the persistent outdoor wind sound.
        // Volume ranges from 0 to 1.
        WindSoundFade.SetVolume(speed / MaxSpeed);

        float lerpT = heatSystem.heatLevel / 100.0f;
        waterSystem.waterValue -= Mathf.Lerp(MinWaterUse, MaxWaterUse, lerpT) * Time.deltaTime;
        if(waterSystem.waterValue <= 0.0f)
        {
            waterSystem.waterValue = 0.0f;
        }
        else
        {
            speed += Mathf.Lerp(MinAcceleration, MaxAcceleration, lerpT) * Time.deltaTime;
        }
        waterSystem.updateSlider();

        Translate(transform.forward * speed * Time.deltaTime);
        speedy.setSpeed(speed / MaxSpeed);

        if(flying)
        {
            flightT += Time.deltaTime / RotateTime;
            transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(-90.0f, 0, 0), flightT);
        }
	}

    public void AddFuel()
    {
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

    public void StartFlying()
    {
        flightT = 0.0f;
        flying = true;
    }

    public bool IsAtMaxSpeed()
    {
        return Mathf.Abs(speed - MaxSpeed) < 0.01f;
    }
}
