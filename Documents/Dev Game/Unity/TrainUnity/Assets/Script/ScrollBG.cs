using UnityEngine;
using System.Collections;

public class ScrollBG : MonoBehaviour {

	public float speed;
	public float posYTop;
	public float posYBottom;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float y = transform.position.y + speed * Time.deltaTime;
		if(y < posYBottom){
			y = posYTop;
		}
		transform.position = new Vector2 (transform.position.x, y);
	}
}
