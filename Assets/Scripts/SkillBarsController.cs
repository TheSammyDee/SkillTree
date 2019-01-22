using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarsController : MonoBehaviour {

    private TimeInputManager timeInputManager;
    private SkillManager skillManager;

	// Use this for initialization
	void Start () {
        timeInputManager = FindObjectOfType<TimeInputManager>();
        skillManager = new SkillManager();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InputTime() {
        timeInputManager.GetTimeInput((int num) => {
            Debug.Log(num);
        }, () => {
            Debug.Log("Cancelled");
        });
    }
}
