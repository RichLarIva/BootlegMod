using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DayCycleController : MonoBehaviour
{
    
    [Range(0,24)]
    public float timeOfDay;
    [Range(0, 60)]
    public float minuteOfHour;
    public Light sun;
    public Light moon;
    public bool realTime;
    public float orbitSpeed = 1.0f;
    public AnimationCurve starsCurve;

    public Volume skyVolume;

    private bool isNight;
    private PhysicallyBasedSky sky;

    public TMP_Text timeTeller;
    public Toggle realTimeToggle;
    
    public void RealTimeSetter(bool isRealTime)
    {
        realTime = isRealTime;
        if (isRealTime == true)
        {
            PlayerPrefs.SetInt("IsRealTime", 1);
            realTime = true;
        }
        else if (isRealTime == false)
        {
            PlayerPrefs.SetInt("IsRealTime", 0);
            realTime = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        skyVolume.profile.TryGet(out sky);

        int fullScreenInt = PlayerPrefs.GetInt("IsRealTime");

        if (fullScreenInt == 1)
        {
            realTimeToggle.isOn = true;
            realTime = true;
        }
        else if (fullScreenInt == 0)
        {
            realTime = false;
            realTimeToggle.isOn = false;
        }
        if (realTime == true)
        {
            timeOfDay = System.DateTime.Now.Hour;
            minuteOfHour = System.DateTime.Now.Minute;
        }
        else if (realTime == false)
        {
            timeOfDay = 0;
            minuteOfHour = 0;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (realTime == true)
        {
            timeOfDay = System.DateTime.Now.Hour;
            minuteOfHour = System.DateTime.Now.Minute;
        }
        else if (realTime == false)
        {
            timeOfDay += Time.deltaTime * orbitSpeed;
            if (timeOfDay > 24)
                timeOfDay = 0;
        }

        if(timeOfDay < 10 && minuteOfHour < 10)
            timeTeller.text = string.Format("0{0}:0{1}", timeOfDay.ToString("0"), minuteOfHour);
        else if(timeOfDay < 10 && minuteOfHour >= 10)
            timeTeller.text = string.Format("0{0}:{1}", timeOfDay.ToString("0"), minuteOfHour);
        else if(timeOfDay >= 10 && minuteOfHour < 10)
            timeTeller.text = string.Format("{0}:0{1}", timeOfDay.ToString("0"), minuteOfHour);
        else
            timeTeller.text = string.Format("{0}:{1}", timeOfDay.ToString("0"), minuteOfHour);

        UpdateTime();
    }

    private void OnValidate()
    {
        skyVolume.profile.TryGet(out sky);
        UpdateTime();
    }

    private void UpdateTime()
    {
        float alpha = timeOfDay / 24.0f;
        float sunRot = Mathf.Lerp(-90, 270, alpha);
        float moonRot = sunRot - 180;

        sun.transform.rotation = Quaternion.Euler(sunRot, -150.0f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRot, -150.0f, 0);

        sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(alpha) * 1000;

        CheckNightDayTransition();
    }

    private void CheckNightDayTransition()
    {
        if (isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180)
                StartDay();
        }
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
                StartNight();
        }
    }

    private void StartDay()
    {
        isNight = false;
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;
    }

    private void StartNight()
    {
        isNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.Soft;
    }
}
