using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    //public Boundary bound = new Boundary();

	public float speed;
	private Rigidbody2D rigid;
	public GameObject prefabBullet;
	public Transform posInstance;
	public float timeFire;
	public float countDown;
    

	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		//speed = 5;
	}
	
	// Update is called once per frame
	void Update () {
		timeFire += Time.deltaTime;

		float move_x = Input.GetAxis("Horizontal"); //-1 -> 1
		float move_y = Input.GetAxis("Vertical"); //-1 -> 1

		//float x = transform.position.x + move_x * Time.deltaTime * speed; 
		//Time.delaTime: Thời gian giữa 2 lần Update
		//float y = transform.position.y + move_y * Time.deltaTime * speed;
		//transform.position = new Vector2 (x, y);

		Vector2 movemant = new Vector2 (move_x, move_y);
		rigid.velocity = movemant * speed;

		if (/*Input.GetButton ("Fire1") &&*/ timeFire >= countDown){
			FireBullet ();
			timeFire = 0;
		}

        //bound
        //float x = Mathf.Clamp (transform.position.x, bound.x_min, bound.x_max);
        //float y = Mathf.Clamp (transform.position.y, bound.y_min, bound.y_max);
        //transform.position = new Vector2 (x, y);
		//Touch Drag

        
	}
	public void FireBullet()
	{
		Instantiate (prefabBullet,posInstance.position , Quaternion.identity);
	}
}
