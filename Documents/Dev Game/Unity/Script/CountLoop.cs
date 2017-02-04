using UnityEngine;
using System.Collections;

public class CountLoop : MonoBehaviour {
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
