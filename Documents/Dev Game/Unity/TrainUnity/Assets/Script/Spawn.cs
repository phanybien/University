using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject prefabObject;
	public float countDownSpawn;
	float timeSpawn;
	public float roundX;
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
		float x = Random.Range (-roundX, roundX);
		Instantiate (prefabObject, new Vector3(x,prefabObject.transform.position.y) , Quaternion.identity);
	}
}
