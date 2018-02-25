using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour {

    [SerializeField] Transform rotationTarget;
    [SerializeField] float rotationTime;
    [SerializeField] Transform dropBarrel;
    [SerializeField] Transform dropTarget;
    [SerializeField] float dropTime;

    private bool rotating, dropping, expended;

    private Quaternion startRot, targetRot;
    private Vector3 startPos, targetPos;
    private float currRotT, currPosT;

	// Use this for initialization
	void Start () {
        rotating = dropping = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(rotating)
        {
            currRotT += Time.deltaTime / rotationTime;
            rotationTarget.rotation = Quaternion.Slerp(startRot, targetRot, currRotT);
            if(currRotT >= 1.0f)
            {
                rotating = false;
                dropping = true;
                currPosT = 0.0f;
                startPos = dropBarrel.position;
                targetPos = dropTarget.position;
            }
        }
        else if(dropping)
        {
            currPosT += Time.deltaTime / dropTime;
            dropBarrel.position = Vector3.Lerp(startPos, targetPos, currPosT);
            if (currPosT >= 1.0f)
            {
                dropping = false;
            }
        }
	}

    public void Bullseye()
    {
        if (expended) return;
        else expended = true;
        currRotT = 0.0f;
        startRot = rotationTarget.rotation;
        targetRot = Quaternion.AngleAxis(180.0f, Vector3.up) * rotationTarget.rotation;
        rotating = true;
    }
}
