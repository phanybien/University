using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnSound : MonoBehaviour {
    public Sprite Au_turn_on;
    public Sprite Au_turn_off;
    public AudioSource[] audios;
    public bool turn;

    private Image image;


	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick() {
        turn = !turn;
        foreach (AudioSource au in audios)
        {
            au.mute = turn;
        }
        if (turn)
        {
            image.sprite = Au_turn_on;
        }
        else
        {
            image.sprite = Au_turn_off;
        }
    }
}
