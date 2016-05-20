using UnityEngine;
using System.Collections;
using System;

public class ClockController : MonoBehaviour {

    GameObject hourPointer;
    GameObject minutePointer;
    GameObject secondPointer;

    private const float
        hoursToDegrees = 360f / 12f,
        minutesToHourDegrees = 360f / 60f / 12f,
        minutesToDegrees = 360f / 60f,
        secondsToDegrees = 360f / 60f;

    Vector3 direction = new Vector3(0f, 0f, -1f);

    float secondPassed = 0f;
    float minutePassed = 0f;

    void Awake ()
    {
        Application.runInBackground = true;

        hourPointer = transform.Find("hour").gameObject;
        minutePointer = transform.Find("minute").gameObject;
        secondPointer = transform.Find("second").gameObject;

        DateTime now = System.DateTime.Now;

        minutePassed = now.Second;

        //Calibrate current time
        secondPointer.transform.RotateAround(transform.position, direction, now.Second * secondsToDegrees);

        minutePointer.transform.RotateAround(transform.position, direction, now.Minute * minutesToDegrees);

        hourPointer.transform.RotateAround(transform.position, direction, now.Hour * hoursToDegrees + (now.Minute * minutesToDegrees) / 12f);

    }
	
	void FixedUpdate ()
    {
        secondPassed += Time.deltaTime;
        minutePassed += Time.deltaTime;

        if (secondPassed >= 1)
        {
            secondPointer.transform.RotateAround(transform.position, direction, secondsToDegrees);
            secondPassed %= 1;
        }
        //Rotate minute and hour pointers only when a full minute has passed
        if (minutePassed >= 60)
        {
            minutePointer.transform.RotateAround(transform.position, direction, minutesToDegrees);
            hourPointer.transform.RotateAround(transform.position, direction, minutesToHourDegrees);
            minutePassed %= 60;
        }
    }
}
