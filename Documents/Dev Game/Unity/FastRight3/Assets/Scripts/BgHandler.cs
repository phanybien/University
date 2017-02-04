using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BgHandler : MonoBehaviour {
    public float Pos_Top;
    public Vector2 Bg_size;
    public float minY;

    private float pos_Y;
    private float vel_Y;
    private SpriteRenderer renderer;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        Bg_size = renderer.bounds.size;
        vel_Y = - GlobalManager.velocity * GlobalManager.speedBG;
        pos_Y = transform.position.y;
        Pos_Top = pos_Y + Bg_size.y / 2;
        renderer.sortingOrder = -1;
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {
        vel_Y = -GlobalManager.velocity * GlobalManager.speedBG;
        set_pos_y(pos_Y + vel_Y * Time.deltaTime);
    }

    public void set_pos_y(float y) {
        pos_Y = y;
        transform.position = new Vector2(0, pos_Y);
        Pos_Top = pos_Y + Bg_size.y / 2;
        if (Pos_Top <= minY)
            Destroy(gameObject);
    }
}
