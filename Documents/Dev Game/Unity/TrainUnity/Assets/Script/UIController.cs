using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public GameObject gameStart;
	public GameObject gamePlay;
	public GameObject game;
	// Use this for initialization
	void Start () {
		gamePlay.SetActive (false);
		game.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{

		}

		//Lưu điểm
		//PlayerPrefs.SetInt ("highscore",highscore);
		//PlayerPrefs.GetInt(key,score)

	}
	public void btnPlay()
	{
		Debug.Log ("Bam nut");
		gamePlay.SetActive (true);
		gameStart.SetActive (false);
		game.SetActive (false);
	}
	public void btnBack()
	{
		gamePlay.SetActive (false);
		gameStart.SetActive (false);
		game.SetActive (true);
	}
}
