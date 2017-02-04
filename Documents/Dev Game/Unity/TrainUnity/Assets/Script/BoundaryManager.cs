using UnityEngine;
using System.Collections;

public class Boundary {

    public float x_max, x_min, y_max, y_min;
    public Boundary()
    {
        this.x_max = this.x_min = this.y_max = this.y_min = 0;
    }
    public Boundary(float xMax, float xMin, float yMax, float yMin)
    {
        this.x_max = xMax;
        this.x_min = xMin;
        this.y_max = yMax;
        this.y_min = yMin;
    }
}
public class BoundaryManager : MonoBehaviour
{
    public GameObject obj;
    public float x_Max, x_Min, y_Max, y_Min;
    public Boundary bound;
    void Update()
    {
        bound = new Boundary(x_Max, x_Min, y_Max, y_Min);
        float x = Mathf.Clamp (transform.position.x, bound.x_min, bound.x_max);
		float y = Mathf.Clamp (transform.position.y, bound.y_min, bound.y_max);
		obj.transform.position = new Vector2 (x, y);
    }

}
