using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{

	public Text vText;
	public Text sText;
	public float speed;
	public float speedStop;
	private Rigidbody2D rigid;
	private float velocityChange;

	float timeEnd;
	//private float sEnd;

	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		timeEnd += Time.deltaTime;
		float move_x = Input.GetAxis("Horizontal"); //-1 -> 1
		//float move_y = Input.GetAxis("Vertical"); //-1 -> 1
		Vector2 movemant = new Vector2 (move_x, 0);
		rigid.velocity = movemant * speed;


		if (Input.GetMouseButton(0)) {
			GlobalManager.velocity += Time.deltaTime;
		} else
			GlobalManager.velocity -= Time.deltaTime * speedStop;

		if(GlobalManager.velocity <= 0)
			GlobalManager.velocity = 0;

		//Text Velocity
		velocityChange = GlobalManager.velocity * 5;
		vText.text = (int)velocityChange + " km/h";

		//Quãng đường kết thúc
		GlobalManager.quangduong = GlobalManager.quangduong + velocityChange * timeEnd;
		sText.text = (int)GlobalManager.quangduong + "km";
		if(GlobalManager.quangduong >= 1000000)
			Debug.Log("Đến nơi");
	}

}
