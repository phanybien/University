using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollisionRoadSide : MonoBehaviour {
    public Sprite player;
    private Image img;
    RectTransform recTransform;
    public GameObject _source;
    public GameObject _taget;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Roadside") {
            img = GetComponent<Image>();
            recTransform = GetComponent<RectTransform>();
            img.sprite = player;
            recTransform.localScale = new Vector3((float)0.8, 1, 1);
            FindObjectOfType<PlayerController>().isDie = true;
            FindObjectOfType<PlayerController>().gameOver();
        }
       
    }  
}	

