using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUICtrl : MonoBehaviour {

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }  
}
