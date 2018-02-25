using System.Collections.Generic;
using UnityEngine;

public class Whistle : MonoBehaviour {

    private AudioSource audioSource;
    private bool playing = false;
    private float dist = 0.0f;
    private float distSpeed = 4.0f;
    private float yStart;
    private float dY = 0.1f;

    private bool pressing = false;
    private List<SteamVR_TrackedController> touchingControllers;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        yStart = transform.position.y;
        touchingControllers = new List<SteamVR_TrackedController>();
    }
    
    void Update() {
        pressing = false;
        for (int i=0; i<touchingControllers.Count; i++) {
            if (touchingControllers[i].triggerPressed) {
                pressing = true;
            }
        }

        // Patrick testing
        //pressing = Input.GetButton("Fire1");

        if (pressing) {
            dist += distSpeed * Time.deltaTime;

            if (!playing) {
                playing = true;
                audioSource.Play();
                audioSource.volume = 0.0f;
            }
        } else {
            dist -= distSpeed * Time.deltaTime;
        }

        dist = Mathf.Clamp(dist, 0.0f, 1.0f);

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Lerp(yStart, yStart-dY, dist),
            transform.position.z);

        if (playing) {
            if (pressing) {
                audioSource.volume = dist * 1.0f;
            } else {
                audioSource.volume += 
                    (dist * 0.5f - audioSource.volume) * 2.0f * Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("GameController")) {
            SteamVR_TrackedController controller =
                other.transform.GetComponent<SteamVR_TrackedController>();

            if (!touchingControllers.Contains(controller)) {
                touchingControllers.Add(controller);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag.Equals("GameController")) {
            SteamVR_TrackedController controller =
                other.transform.GetComponent<SteamVR_TrackedController>();

            if (touchingControllers.Contains(controller)) {
                touchingControllers.Remove(controller);
            }
        }
    }
}
