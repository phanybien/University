using UnityEngine;
using System.Collections;

public class DestroyObj : MonoBehaviour {
	public GameObject obj;

	void OnTriggerExit2D(Collider2D other)
	{
		//if (other.tag == "Tree")
			Destroy (other.gameObject);
	}
}
