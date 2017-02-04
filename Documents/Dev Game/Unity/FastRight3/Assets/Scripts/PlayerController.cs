using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{

	public Text vText;
    public bool isDie = false;
	public float speed;
	public float speedStop;
	private Rigidbody2D rigid;
	public float velocityChange;
    public Sprite _characterAccident;
    public Sprite _characterDefault;
    public GameObject _gameStart;
    public GameObject _gamePlay;
    public GameObject _gameEnd;
    public Vector2 pos;



    

    float timeEnd;
	//private float sEnd;

	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
        pos = transform.position;
        OnResume();
    }
	
	void Update () {
		timeEnd += Time.deltaTime;
		float move_x = Input.GetAxis("Horizontal"); //-1 -> 1
		//float move_y = Input.GetAxis("Vertical"); //-1 -> 1
		Vector2 movemant = new Vector2 (move_x, 0);
        if (isDie == true)
            movemant = new Vector2(0, 0);
		rigid.velocity = movemant * speed;


        if (Input.GetMouseButton(0) && isDie == false)
        {
            GlobalManager.velocity += Time.deltaTime;
        }
        else if (GlobalManager.velocity > 0)
        {
            GlobalManager.velocity -= Time.deltaTime * speedStop;
        }
		if(GlobalManager.velocity <= 0)
			GlobalManager.velocity = 0;

		//Text Velocity
		velocityChange = GlobalManager.velocity * 5;
		vText.text = (int)velocityChange + " km/h";

		//Quãng đường kết thúc
		GlobalManager.quangduong = GlobalManager.quangduong + velocityChange * timeEnd;
		if(GlobalManager.quangduong >= 1000000)
			Debug.Log("Đến nơi");
        if(GlobalManager.time <= 0)
        {
            isAccident();
            //sleep           
            gameOver();
        }

	}
    void isAccident()
    {
        RectTransform recTransform;
        Image img = GetComponent<Image>();
        recTransform = GetComponent<RectTransform>();
        img.sprite = _characterAccident;
        recTransform.localScale = new Vector3((float)0.8, 1, 1);
        isDie = true;
}
    public void gameOver()
    {
        StartCoroutine(LoadNewScene("Gameover"));
    }

    public void OnResume() {
        isDie = false;
        GlobalManager.time = GlobalManager.StaticTime;
        RectTransform recTransform;
        Image img = GetComponent<Image>();
        recTransform = GetComponent<RectTransform>();
        recTransform.position = pos;
        img.sprite = _characterDefault;
        recTransform.localScale = new Vector3((float)0.4, (float)0.7, 1);
        velocityChange = 0;
        GlobalManager.velocity = 0;
    }
    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene(string scene)
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(2   );

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = Application.LoadLevelAsync(scene);
        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }
        OnResume();
    }
}
