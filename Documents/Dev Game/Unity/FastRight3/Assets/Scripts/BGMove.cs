using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BGMove : MonoBehaviour {
	private Rigidbody2D rigid;
   
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		Vector2 v = new Vector2(0,-GlobalManager.velocity);	
		rigid.velocity = v * GlobalManager.speedBG;
	}
}
