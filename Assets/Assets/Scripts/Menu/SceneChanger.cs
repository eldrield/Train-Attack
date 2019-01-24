using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public string m_sceneName;
	public void GoToGame()
    {
        SceneManager.LoadScene(m_sceneName);
    }
}
