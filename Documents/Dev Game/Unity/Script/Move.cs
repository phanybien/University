using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Move : MonoBehaviour {
	public float speed;
	private Rigidbody2D rigid;
	float vectX, vectY;
   
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		float move_x = Input.GetAxis("Horizontal"); //-1 -> 1
		float move_y = Input.GetAxis("Vertical"); //-1 -> 1
		Vector2 movemant = new Vector2 (move_x, move_y);
		
		Vector2 v = new Vector2(vectX,vectY);	
		
		//Auto Move
		rigid.velocity = v * speed;
		
		//Move by Arrow
		rigid.velocity = movemant * speed;
	}
}
