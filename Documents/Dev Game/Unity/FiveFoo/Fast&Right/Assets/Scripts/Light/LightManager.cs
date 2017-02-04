using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LightManager : MonoBehaviour {

    public int Time2Yellow;
    public int Time2Red;
    public int Time2Green;
    private float timePoint;
    private int stage;
    private float displayTime;

	public static bool rendered;
	//public GameObject obj;

    // Use this for initialization
    void Start () {
        stage = 0;
        GetComponentInChildren<RedLight>().TurnOff();
        GetComponentInChildren<YellowLight>().TurnOff();
		rendered = false;
    }
	
	// Update is called once per frame
	void Update () {
        displayTime = Time.realtimeSinceStartup - timePoint;
        switch (stage)
        {
            case 0: // đang xanh
                if (displayTime > Time2Yellow)
                {
                    timePoint = Time.realtimeSinceStartup;
                    GetComponentInChildren<GreenLight>().TurnOff();
                    GetComponentInChildren<YellowLight>().TurnOn();
                    stage = 1;
                }
                else
                    GetComponentInChildren<Text>().text = (((int)(Time2Yellow - displayTime)).ToString().Length == 1) ? ("0" + ((int)(Time2Yellow - displayTime)).ToString()) : (((int)(Time2Yellow - displayTime)).ToString()); 
                break;
            case 1: // đang vàng
                if (displayTime > Time2Red)
                {
                    timePoint = Time.realtimeSinceStartup;
                    GetComponentInChildren<YellowLight>().TurnOff();
                    GetComponentInChildren<RedLight>().TurnOn();
                    stage = 2;
                }
                else
                    GetComponentInChildren<Text>().text = (((int)(Time2Red - displayTime)).ToString().Length == 1) ? ("0" + ((int)(Time2Red - displayTime)).ToString()) : (((int)(Time2Red - displayTime)).ToString());
                break;
            case 2: // đang đỏ
                if (displayTime > Time2Green)
                {
                    timePoint = Time.realtimeSinceStartup;
                    GetComponentInChildren<RedLight>().TurnOff();
                    GetComponentInChildren<GreenLight>().TurnOn();
                    stage = 0;
                }
                else
                    GetComponentInChildren<Text>().text = (((int)(Time2Green - displayTime)).ToString().Length == 1) ? ("0" + ((int)(Time2Green - displayTime)).ToString()) : (((int)(Time2Green - displayTime)).ToString()); break;
        }
	
    }
}
