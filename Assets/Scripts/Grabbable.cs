using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    
    [SerializeField] private Transform positionAnchor;
    [SerializeField] private Transform rotationAnchor;
    [SerializeField] private Vector3 PosCorrection;
    [SerializeField] private Vector3 RotCorrection;
    [SerializeField] private float FPTolerance = 0.001f;

    private Rigidbody grabbableBody;
    private List<SteamVR_TrackedController> touchingControllers;
    private Transform positionController;
    private Transform rotationController;

	// Use this for initialization
	void Start () {
        grabbableBody = GetComponent<Rigidbody>();
        touchingControllers = new List<SteamVR_TrackedController>();
        positionController = null;
        rotationController = null;
    }
	
	// Update is called once per frame
	void Update () {
        if(positionController != null)
        {
            grabbableBody.isKinematic = true;

            Vector3 posOffset = positionAnchor.transform.position - transform.position;
            transform.position = positionController.position + posOffset + positionController.rotation * PosCorrection;

            if(rotationController != null)
            {
                Vector3 posContPos = positionController.position + positionController.rotation * PosCorrection;
                Vector3 rotContPos = rotationController.position + rotationController.rotation * PosCorrection;
                Vector3 targetDirection = (rotContPos - posContPos).normalized;
                Vector3 currentDirection = (rotationAnchor.transform.position - positionAnchor.transform.position).normalized;
                Quaternion rotation = Quaternion.FromToRotation(currentDirection, targetDirection);
                transform.rotation = rotation * transform.rotation;

                Vector3 projectedContUp = Vector3.ProjectOnPlane(rotationController.up, rotationAnchor.up);
                if(projectedContUp.magnitude > FPTolerance)
                {
                    float rotAngle = Vector3.Angle(rotationAnchor.right, projectedContUp.normalized);
                    Debug.Log("Rotation Angle: " + rotAngle);
                    if (rotAngle > FPTolerance) transform.rotation = Quaternion.AngleAxis(-rotAngle, rotationAnchor.up) * transform.rotation;
                }
            }
            else
            {
                transform.rotation = positionController.rotation * Quaternion.Euler(RotCorrection.x, 0, 0) * Quaternion.Euler(0, RotCorrection.y, 0) * Quaternion.Euler(0, 0, RotCorrection.z);
            }
        }
        else
        {
            grabbableBody.isKinematic = false;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("GameController"))
        {
            SteamVR_TrackedController controller = other.transform.GetComponent<SteamVR_TrackedController>();
            controller.TriggerClicked += OnControllerTriggerChange;
            controller.TriggerUnclicked += OnControllerTriggerChange;
            touchingControllers.Add(controller);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("GameController"))
        {
            SteamVR_TrackedController controller = other.transform.GetComponent<SteamVR_TrackedController>();
            controller.TriggerClicked += OnControllerTriggerChange;
            controller.TriggerUnclicked += OnControllerTriggerChange;
            if(controller.transform == positionController || controller.transform == rotationController)
            {
                UpdateAnchors();
            }

            touchingControllers.Remove(controller);
        }
    }

    private void OnControllerTriggerChange(object obj, ClickedEventArgs eventArgs)
    {
        UpdateAnchors();
    }

    private void UpdateAnchors()
    {
        List<int> activeIndices = new List<int>();

        for(int index = 0; index < touchingControllers.Count; index++) if (touchingControllers[index].triggerPressed) activeIndices.Add(index);

        if(activeIndices.Count == 1)
        {
            positionController = touchingControllers[activeIndices[0]].transform;
            rotationController = null;
        }
        else if(activeIndices.Count >= 2)
        {
            float minPosDistance = float.PositiveInfinity;
            int minPosIndex = -1;

            foreach(int index in activeIndices)
            {
                float distance = Vector3.Distance(touchingControllers[index].transform.position, positionAnchor.position);
                if(distance < minPosDistance)
                {
                    minPosDistance = distance;
                    minPosIndex = index;
                }
            }

            int minRotIndex = -1;

            if(activeIndices.Count > 2)
            {
                float minRotDistance = float.PositiveInfinity;
                foreach (int index in activeIndices)
                {
                    if (index == minPosIndex) continue;
                    float distance = Vector3.Distance(touchingControllers[index].transform.position, positionAnchor.position);
                    if (distance < minRotDistance)
                    {
                        minRotDistance = distance;
                        minRotIndex = index;
                    }
                }
            }
            else
            {
                minRotIndex = (minPosIndex == activeIndices[0]) ? minRotIndex = activeIndices[1] : minRotIndex = activeIndices[0];
            }

            positionController = touchingControllers[minPosIndex].transform;
            rotationController = touchingControllers[minRotIndex].transform;
        }
        else
        {
            positionController = rotationController = null;
        }
    }
}
