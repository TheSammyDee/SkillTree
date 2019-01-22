using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInputManager : MonoBehaviour {

    [SerializeField]
    TimeInput timeInput;

    private string timeString;
    public delegate void AcceptDelegate(int num);
    private AcceptDelegate onAccept;
    private Action onCancel;
    private int timeNum;

	// Use this for initialization
	void Start () {
        timeInput.Hide();
        timeString = "";
        timeNum = 0;
	}

    public void GetTimeInput(AcceptDelegate onAccept, Action onCancel) {
        this.onAccept = onAccept;
        this.onCancel = onCancel;

        timeString = "";
        timeNum = 0;
        timeInput.Show();
    }

    public void AddToTimeString(string numString) {
        if (!(numString == "0" && timeString.Length == 0)) {
            timeString = timeString + numString;
            timeInput.SetTimerText(timeString);
        }
    }

    public void BackspaceTimeString() {
        if (timeString.Length > 0) {
            timeString = timeString.Substring(0, timeString.Length - 1);
            timeInput.SetTimerText(timeString);
        }
    }

    public void OnAccept() {
        timeInput.Hide();
        ConvertTimeString();
        onAccept(timeNum);
    }

    public void OnCancel() {
        timeInput.Hide();
        onCancel();
    }

    private void ConvertTimeString() {
        if (timeString.Length > 0) {
            if (timeString.Length > 2) {
                timeNum = Int32.Parse(timeString.Substring(0, timeString.Length - 2)) * 60;
                timeNum += Int32.Parse(timeString.Substring(timeString.Length - 2, 2));
            } else {
                timeNum = Int32.Parse(timeString);
            }
            
        } else {
            timeNum = 0;
        }
    }
}
