using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalMover : MonoBehaviour {

    [SerializeField] private GameObject coalPrefab;
    [SerializeField] private Transform coalAnchor;

    private bool hasCoal;
    private Transform coal;

	// Use this for initialization
	void Start () {
        hasCoal = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(hasCoal)
        {
            coal.position = coalAnchor.position;
            coal.rotation = coalAnchor.rotation; 
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("CoalProducer") && !hasCoal)
        {
            hasCoal = true;
            coal = Instantiate(coalPrefab).transform;
        }
        else if(other.tag.Equals("CoalConsumer") && hasCoal)
        {
            hasCoal = false;
            Destroy(coal.gameObject);
        }
    }
}
