using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
	public AudioSource audioEffect;
	public float volume;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Mute()
	{
		audioEffect.Stop ();
		audioEffect.volume = volume;
	}
}
