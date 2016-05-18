using UnityEngine;
using System.Collections;
using System;

public class ClockController : MonoBehaviour {

    GameObject hourPointer;
    GameObject minutePointer;
    GameObject secondPointer;

    Vector3 middle;

    Vector3 direction = new Vector3(0f, 0f, -1f);

    void Awake ()
    {
        hourPointer = transform.Find("hour").gameObject;
        minutePointer = transform.Find("minute").gameObject;
        secondPointer = transform.Find("second").gameObject;

        middle = transform.position;

        DateTime now = System.DateTime.Now;

        //Calibrate current time
        hourPointer.transform.RotateAround(middle, direction, (360f / 12f) * now.Hour + (360f / 60f / 12f) * now.Minute + (360f / 60f / 60f / 12f) * now.Second);
        minutePointer.transform.RotateAround(middle, direction, (360f / 60f) * now.Minute + (360f / 60f / 60f / 12f) * now.Second);
        secondPointer.transform.RotateAround(middle, direction, (360f / 60) * now.Second);
    }
	
	void Update ()
    {
        hourPointer.transform.RotateAround(middle, direction, (Time.deltaTime / (60 * 60 * 60)) * 360);
        minutePointer.transform.RotateAround(middle, direction, (Time.deltaTime / (60 * 60)) * 360);
        secondPointer.transform.RotateAround(middle, direction, Time.deltaTime / 60 * 360);
    }
}
