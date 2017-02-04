using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Light : MonoBehaviour {

	public GameObject greenLight;
	public GameObject yellowLight;
	public GameObject redLight;
	private float timeNext;
	private int state;

	public int toYellow;
	public int toRed;
	public int toGreen;
	private float displayTime;

	public Text numberText;
	// Use this for initialization
	void Start () {
		greenLight.SetActive(true);
		yellowLight.SetActive(false);
		redLight.SetActive(false);
		state = 0;
		timeNext = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		//timeNext = Time.realtimeSinceStartup
		displayTime = Time.realtimeSinceStartup - timeNext;
		switch(state)
		{
		case 0:
			if (Time.realtimeSinceStartup - timeNext >= toYellow)
			{
				yellowLight.SetActive(true);
				greenLight.SetActive(false);
				redLight.SetActive(false);
				timeNext = Time.realtimeSinceStartup;
				state = 1;
			}
			else
					GetComponentInChildren<Text>().text = (((int)(toYellow - displayTime)).ToString().Length == 1) ? ("0" + ((int)(toYellow - displayTime)).ToString()) : (((int)(toYellow - displayTime)).ToString()); 
			break;
		case 1:
			if (Time.realtimeSinceStartup - timeNext >= toRed)
			{
				redLight.SetActive(true);
				greenLight.SetActive(false);
				yellowLight.SetActive(false);
				timeNext = Time.realtimeSinceStartup;
				state = 2;
			}
			else
				GetComponentInChildren<Text>().text = (((int)(toRed - displayTime)).ToString().Length == 1) ? ("0" + ((int)(toRed - displayTime)).ToString()) : (((int)(toRed - displayTime)).ToString()); 

			break;
		case 2:
			if (Time.realtimeSinceStartup - timeNext >= toGreen)
			{
				redLight.SetActive(false);
				greenLight.SetActive(true);
				yellowLight.SetActive(false);
				timeNext = Time.realtimeSinceStartup;
				state = 0;
			}
			else
				GetComponentInChildren<Text>().text = (((int)(toGreen - displayTime)).ToString().Length == 1) ? ("0" + ((int)(toGreen - displayTime)).ToString()) : (((int)(toGreen - displayTime)).ToString()); 

			break;
		}
	}
}
