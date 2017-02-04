using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject prefabObject;
	public float countDownSpawn;
	float timeSpawn;
	public float roundY;
	void Start () {
	
	}
	void Update () {
		timeSpawn += Time.deltaTime;
		if(timeSpawn >= countDownSpawn)
		{
			Spaw ();
			timeSpawn = 0;
		}
	
	}
	public void Spaw()
	{
		float y = Random.Range (-roundY, roundY);
		Instantiate (prefabObject, new Vector3(prefabObject.transform.position.x,y) , Quaternion.identity);
	}
}
