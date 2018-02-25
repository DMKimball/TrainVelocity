using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<SteamVR_TrackedController>().MenuButtonClicked += OnMenuClick;
	}
	
	private void OnMenuClick(object obj, ClickedEventArgs args)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
