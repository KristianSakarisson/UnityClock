using UnityEngine;
using System.Collections;
using System;

public class ClockController : MonoBehaviour {

    GameObject hourPointer;
    GameObject minutePointer;
    GameObject secondPointer;

    Vector3 middle;

    Vector3 direction = new Vector3(0f, 0f, -1f);

    float secondPassed = 0f;
    float minutePassed = 0f;

    void Awake ()
    {
        Application.runInBackground = true;

        hourPointer = transform.Find("hour").gameObject;
        minutePointer = transform.Find("minute").gameObject;
        secondPointer = transform.Find("second").gameObject;

        middle = transform.position;

        DateTime now = System.DateTime.Now;

        minutePassed = now.Second;

        //Calibrate current time
        secondPointer.transform.RotateAround(middle, direction, (now.Second / 60f * 360f));

        minutePointer.transform.RotateAround(middle, direction, now.Minute / 60f * 360f);

        hourPointer.transform.RotateAround(middle, direction, now.Hour / 12f * 360f + now.Minute / (12f * 60f) * 360f);

    }
	
	void FixedUpdate ()
    {
        secondPassed += Time.deltaTime;
        minutePassed += Time.deltaTime;

        if (secondPassed >= 1)
        {
            secondPointer.transform.RotateAround(middle, direction, 1f / 60f * 360f);
            secondPassed %= 1;
        }
        //Rotate minute and hour pointers only when a full minute has passed
        if (minutePassed >= 60)
        {
            minutePointer.transform.RotateAround(middle, direction, 1f / 60 * 360);
            hourPointer.transform.RotateAround(middle, direction, 1f / (60 * 12) * 360);
            minutePassed %= 60;
        }
    }
}
