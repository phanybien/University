using UnityEngine;
using System.Collections;

public class DestroyPlayer : MonoBehaviour {
	public GameObject prefabExplosion;
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			Destroy (other.gameObject);
			Destroy (gameObject);
			Instantiate(prefabExplosion, transform.position, Quaternion.identity);
		}
	}
}
