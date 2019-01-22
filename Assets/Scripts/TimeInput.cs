using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInput : MonoBehaviour {

    [SerializeField]
    UnityEngine.UI.Text timerText;

    [SerializeField]
    GameObject ui;

    // Use this for initialization
    void Start () {
        ClearTimer();
    }

    public void SetTimerText(string time) {
        string text;

        if (time.Length < 3) {
            text = "0:";
            if (time.Length < 2) {
                text = text + "0";
                if (time.Length == 0) {
                    text = text + "0";
                }
            }
            text = text + time;
        } else {
            text = time.Substring(0, time.Length - 2) + ":" + time.Substring(time.Length - 2, 2);
        }

        timerText.text = text;
    }

    public void Hide() {
        ui.SetActive(false);
    }

    public void Show() {
        ClearTimer();
        ui.SetActive(true);
    }

    private void ClearTimer() {
        SetTimerText("");
    }
}
