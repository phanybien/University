using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
	public GameObject gameObjectTodrag; //refer to GO that being dragged
	public Vector3 GOcenter; //gameobjectcenter
	public Vector3 touchPosition; //touch or click position
	public Vector3 offset;//vector between touchpoint/mouseclick to object center
	public Vector3 newGOCenter; //new center of gameObject
	RaycastHit hit; //store hit object information
	public bool draggingMode = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//CLICK DRAG
		/*if (Input.GetMouseButtonDown(0))
		{
			//convert mouse click position to a ray
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			//if ray hit a Collider ( not 2DCollider)
			if (Physics.Raycast(ray, out hit))
			{
				gameObjectTodrag = hit.collider.gameObject;
				GOcenter = gameObjectTodrag.transform.position;
				touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				offset = touchPosition - GOcenter;
				draggingMode = true;
			}
		}
		
		//every frame when user hold on left mouse
		if (Input.GetMouseButton(0))
		{
			if (draggingMode)
			{
				touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				newGOCenter = touchPosition - offset;
				gameObjectTodrag.transform.position = new Vector3(newGOCenter.x, newGOCenter.y, GOcenter.z);
			}
		}
		
		//when mouse is released
		if (Input.GetMouseButtonUp(0))
		{
			draggingMode = false;
		}*/

		//TOUCH DRAG
		foreach (Touch touch in Input.touches)
		{
			switch (touch.phase)
			{
				//When just touch
			case TouchPhase.Began:
				//convert mouse click position to a ray
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				
				//if ray hit a Collider ( not 2DCollider)
				// if (Physics.Raycast(ray, out hit))
				if (Physics.SphereCast(ray, 0.3f, out hit))
				{
					gameObjectTodrag = hit.collider.gameObject;
					GOcenter = gameObjectTodrag.transform.position;
					touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					offset = touchPosition - GOcenter;
					//draggingMode = true;
				}
				break;
				
			case TouchPhase.Moved:
				if (draggingMode)
				{
					touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					newGOCenter = touchPosition - offset;
					gameObjectTodrag.transform.position = new Vector3(newGOCenter.x, newGOCenter.y, GOcenter.z);
				}
				break;
				
			//case TouchPhase.Ended:
				//draggingMode = false;
				//break;
                 
			}
		}
	}
}
