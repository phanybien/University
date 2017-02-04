using UnityEngine;
using System.Collections;

public class BtnReplay : MonoBehaviour {

    // man hinh duoc play khi bam nut
    public GameObject Scr_displayed;
    public GameObject[] Scrs_non_displayed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        foreach (GameObject ob in Scrs_non_displayed)
        {
            ob.SetActive(false);
        }
        Scr_displayed.SetActive(true);
        FindObjectOfType<PlayerController>().OnResume();
      
        
    }
}
