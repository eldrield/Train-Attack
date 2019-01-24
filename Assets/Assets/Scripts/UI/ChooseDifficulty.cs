using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseDifficulty : MonoBehaviour {

	public void One()
	{
		PlayerPrefs.SetInt( "Difficulty" , 1 );
		SceneManager.LoadScene( "Game" );
	}
	public void Two()
    {
        PlayerPrefs.SetInt( "Difficulty" , 2 );
        SceneManager.LoadScene( "Game" );
    }
	public void Three()
    {
        PlayerPrefs.SetInt( "Difficulty" , 3 );
        SceneManager.LoadScene( "Game" );
    }
	public void Four()
    {
        PlayerPrefs.SetInt( "Difficulty" , 4 );
        SceneManager.LoadScene( "Game" );
    }
	public void Five()
    {
        PlayerPrefs.SetInt( "Difficulty" , 5 );
        SceneManager.LoadScene( "Game" );
    }
}
