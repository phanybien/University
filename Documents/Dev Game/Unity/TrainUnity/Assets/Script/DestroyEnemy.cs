using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyEnemy : MonoBehaviour {
	public GameObject prefabExplosion;
    public GameObject obj;
    void Start()
    {
    }
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == obj.tag)
		{
			Destroy (other.gameObject);
			Destroy (gameObject);
			Instantiate(prefabExplosion, transform.position, Quaternion.identity);

            GlobalManager.score += 1;
            if (GlobalManager.score >= GlobalManager.highscore)
            {
                GlobalManager.highscore = GlobalManager.score;
                PlayerPrefs.SetInt("highScore", GlobalManager.highscore);
                
            }
		}
	}
    void Update(){
        
    }
}
