using UnityEngine;

// Fade sound volume gradually instead of immediately.
public class SoundFade : MonoBehaviour {

    private float targetVolume = 0.0f;
    private float snapSpeed = 0.5f;

    public void SetVolume(float v) {
        targetVolume = v;
    }

    public void SetSnapSpeed(float s) {
        snapSpeed = s;
    }

	void Update () {
        float delta = targetVolume - GetComponent<AudioSource>().volume;
		GetComponent<AudioSource>().volume +=
            delta * snapSpeed * Time.deltaTime;

        if (Mathf.Abs(delta) < 0.01f) {
            GetComponent<AudioSource>().volume = targetVolume;
        }
	}
}
