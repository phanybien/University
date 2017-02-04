using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class VelocBar : MonoBehaviour
{
    public float maxVeloc;

    // Update is called once per frame
    void Update()
    {
        float vantoc = FindObjectOfType<PlayerController>().velocityChange;
        if (vantoc > 0 && vantoc <= maxVeloc)
        {
            float phantram = (vantoc * 100) / maxVeloc; // BAo nhieu phan tram cua thanh hien thi
            float xscale = (phantram * 7) / 100;
            transform.localScale = new Vector3(xscale, 4f, 59.1f);
        }
    }
}

