using UnityEngine;
using System.Collections;

public class ChopChop : MonoBehaviour {

    public float flashTime;
    private bool active;
    private float timePoint, timePoint2;
    public float velocity;

	// Use this for initialization
	void Start () {
        active = false ;
        GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if (Time.realtimeSinceStartup - timePoint < flashTime)
            {
                if (Time.realtimeSinceStartup - timePoint2 > velocity)
                {
                    GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
                    timePoint2 = Time.realtimeSinceStartup;
                }
            }
            else
                active = false;
        }
	}

    public void Active()
    {
        active = true;
        timePoint2 = timePoint = Time.realtimeSinceStartup;
    }
}
