using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Timer classes pulled from old system
// TODO: redo timer classes
public class TimeInputManager {

    private TimeInputViewModel timeInput;

    private string timeString;
    public delegate void AcceptDelegate(int num);
    private AcceptDelegate onAccept;
    private Action onCancel;
    private int timeNum;

    public TimeInputManager(TimeInputViewModel timeInput, Canvas canvas)
    {
        this.timeInput = timeInput;
        timeInput.gameObject.transform.SetParent(canvas.transform, false);
        timeInput.OnNumberClick += AddToTimeString;
        timeInput.OnBackspaceClick += BackspaceTimeString;
        timeInput.OnCancelClick += OnCancel;
        timeInput.OnAcceptClick += OnAccept;
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

    private void AddToTimeString(string numString) {
        if (!(numString == "0" && timeString.Length == 0)) {
            timeString = timeString + numString;
            timeInput.SetTimerText(timeString);
        }
    }

    private void BackspaceTimeString() {
        if (timeString.Length > 0) {
            timeString = timeString.Substring(0, timeString.Length - 1);
            timeInput.SetTimerText(timeString);
        }
    }

    private void OnAccept() {
        timeInput.Hide();
        ConvertTimeString();
        if (onAccept != null)
            onAccept(timeNum);
    }

    public void OnCancel() {
        timeInput.Hide();
        if (onCancel != null)
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
