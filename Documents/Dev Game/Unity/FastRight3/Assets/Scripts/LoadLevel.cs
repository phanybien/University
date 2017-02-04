using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
    public string level;
    
    public void OnClick()
    {
        SceneManager.LoadScene(level);
        if (FindObjectOfType<PlayerController>() != null)
            FindObjectOfType<PlayerController>().OnResume();
    }


}
