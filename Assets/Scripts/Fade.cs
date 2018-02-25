using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    Material mat;
    float currT, fadeTime;
    bool fading;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        currT = 0.0f;
        fading = false;
        fadeTime = 10.0f;
    }

    void Update()
    {
        if(fading)
        {
            currT += Time.deltaTime / fadeTime;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, Mathf.Lerp(0, 1, currT));
        }
    }

    public void FadeOut(float time)
    {
        fading = true;
        fadeTime = time;
    }
}
