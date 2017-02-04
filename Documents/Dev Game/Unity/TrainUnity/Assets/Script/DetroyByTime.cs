using UnityEngine;
using System.Collections;

public class DetroyByTime : MonoBehaviour {
	public float timeLife;
	void Start () {
		Invoke ("DestroyMe", timeLife);
	
	}
	void DestroyMe()
	{
		Destroy (gameObject);
	}
	void Update () {
	
	}
}
