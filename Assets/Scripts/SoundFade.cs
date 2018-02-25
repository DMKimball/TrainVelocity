using UnityEngine;

// Fade sound volume gradually instead of immediately.
public class SoundFade : MonoBehaviour {

    private float targetVolume = 0.0f;

    public void SetVolume(float v) {
        targetVolume = v;
    }

	void Update () {
        float delta = targetVolume - GetComponent<AudioSource>().volume;
		GetComponent<AudioSource>().volume +=
            delta * 0.5f * Time.deltaTime;

        if (Mathf.Abs(delta) < 0.01f) {
            GetComponent<AudioSource>().volume = targetVolume;
        }
	}
}
