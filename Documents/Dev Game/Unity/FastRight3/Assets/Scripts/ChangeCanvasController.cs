using UnityEngine;
using System.Collections;

public class ChangeCanvasController : MonoBehaviour {

    public GameObject[] _disable;
    public GameObject _enable;

    public void OnClick()
    {
        _enable.SetActive(true);
        FindObjectOfType<PlayerController>().OnResume();
        foreach (GameObject ob in _disable)
        {
            ob.SetActive(true);
        } 
    }
}
