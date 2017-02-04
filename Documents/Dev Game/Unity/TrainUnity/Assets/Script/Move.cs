using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float speed;
	public Rigidbody2D rigid;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		rigid.velocity = new Vector2 (0, speed);
	}
}
