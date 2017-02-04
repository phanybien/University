using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public int score;
    public string title;

    private Text text;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        text.text = title + score;
    }
}
