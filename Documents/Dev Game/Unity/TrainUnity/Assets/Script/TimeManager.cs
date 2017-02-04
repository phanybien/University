using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    public Text timeText;

	void Start () {
	}
	void Update () {
        GlobalManager.time -= Time.deltaTime;
        timeText.text = "Time: " + (int)GlobalManager.time;	
	}
}
