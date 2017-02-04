using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler { 

	// Use this for initialization
	void Start () {
		//Vector3 temp = Camera.main.ScreenToWorldPoint ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log ("Nhan xuong");
	}

	//Chỉ xử lý trên image
	public void OnBeginDrag(PointerEventData eventData)
	{

	}
	public void OnDrag(PointerEventData eventData)
	{
		Vector3 temp = Camera.main.ScreenToWorldPoint (eventData.position);//Chuyển tọa độ
		Debug.Log ("Dang Drag" + transform.position);
	}
}
