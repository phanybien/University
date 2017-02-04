using UnityEngine;
using System.Collections;

public class YellowLight : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOn()
    {
        GetComponent<Renderer>().enabled = true;
    }

    public void TurnOff()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
