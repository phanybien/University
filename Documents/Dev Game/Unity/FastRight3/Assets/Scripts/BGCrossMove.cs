    using UnityEngine;
using System.Collections;

public class BGCrossMove : MonoBehaviour {
	private Rigidbody2D rigid;
	//public GameObject prefabCross;

	//float spawnTime = 0;

	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		//spawnTime = Time.realtimeSinceStartup+5;
	}
	
	void Update () {
		//if (Time.realtimeSinceStartup > spawnTime )
		//{
		//	spawnTime = Time.realtimeSinceStartup+5;
		//	Debug.Log(this.transform.position);
		//	AddCross();
		//}
		Vector2 v = new Vector2(0,-GlobalManager.velocity);
		if (GlobalManager.quangduong >=10000)
		{
			if(this.tag == "Cross1")
			rigid.velocity = v * GlobalManager.speedBG;
		}
		if (GlobalManager.quangduong >=60000)
		{
			if(this.tag == "Cross2")
				rigid.velocity = v * GlobalManager.speedBG;
		}
	}
	//public void AddCross(){
	//	GameObject a = (GameObject)Instantiate(prefabCross ,transform.position,Quaternion.identity);
	//	a.transform.top = 200;
	//	a.transform.parent = transform;
	//}
}
