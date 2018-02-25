using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalMover : MonoBehaviour {

    [SerializeField] private GameObject coalPrefab;
    [SerializeField] private Transform coalAnchor;
    [SerializeField] private TrainMovement train;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] coalPickupSounds;
    [SerializeField] private AudioClip[] coalDropSounds;

    private Grabbable grabScript;

    private bool hasCoal;
    private Transform coal;

	// Use this for initialization
	void Start () {
        hasCoal = false;
        grabScript = GetComponent<Grabbable>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hasCoal)
        {
            coal.position = coalAnchor.position;
            coal.rotation = coalAnchor.rotation;
            coal.localScale = coalAnchor.localScale;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("CoalProducer") && !hasCoal)
        {
            hasCoal = true;
            coal = Instantiate(coalPrefab).transform;
            grabScript.RegisterAttachment(coal);

            PlayRandomClip(coalPickupSounds);
        }
        else if(other.tag.Equals("CoalConsumer") && hasCoal)
        {
            hasCoal = false;
            train.AddFuel();
            grabScript.DeregisterAttachment(coal);
            Destroy(coal.gameObject);

            PlayRandomClip(coalDropSounds);
        }
    }

    void PlayRandomClip(AudioClip[] clipArray) {
        audioSource.Stop();
        audioSource.clip = clipArray[Random.Range(0, clipArray.Length)];
        audioSource.pitch = 1 + Random.Range(-0.1f, 0.1f);
        audioSource.Play();
    }
}
