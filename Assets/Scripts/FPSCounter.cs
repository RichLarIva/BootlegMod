using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public float refresh, avgFramerate;

    private int avgFrameRate;
    private TMP_Text displayText;

    string display = "FPS: {0}";

    //private float hudRefreshRate = 1;

    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        displayText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0)
            avgFramerate = (int)(1f / timelapse);
        displayText.text = string.Format(display, avgFramerate.ToString());
        
    }
}
