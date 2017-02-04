using UnityEngine;
using System.Collections;

public class HorizontalMovemant : MonoBehaviour {
    private Rigidbody2D rigid;
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	void Update () {
        if (rigid.velocity.x > 0)
            this.transform.localScale = new Vector2(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
        else
            if (rigid.velocity.x < 0)
                this.transform.localScale = new Vector2(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
	}
}
