using UnityEngine;
using System.Collections;

public class BgManager : MonoBehaviour {
    public GameObject background;
    public GameObject curr_background;
    public float minY, maxY;

    private bool end_map = false;
    private int max_bg = 2;
    private int index = 0;

    public bool End_Map {
        get { return end_map; }
        set { end_map = value; }
    }

	// Use this for initialization
	void Start () {
        curr_background.GetComponent<BgHandler>().minY = minY;
    }
	
	// Update is called once per frame
	void Update () {
        if (index < 2)
        {
            if (Mathf.Abs(GlobalManager.velocity * GlobalManager.speedBG) > 0)
            {
                BgHandler bg_handler = curr_background.GetComponent<BgHandler>();
                if (bg_handler.Pos_Top <= maxY)
                {
                    GameObject bg = Instantiate(background) as GameObject;
                    bg.GetComponent<BgHandler>().minY = minY;
                    bg.GetComponent<BgHandler>().set_pos_y(bg_handler.Pos_Top + bg_handler.Bg_size.y / 2 - 0.4f);
                    curr_background = bg;
                    index++;
                }
            }
        }
        else end_map = true;
	}
}
