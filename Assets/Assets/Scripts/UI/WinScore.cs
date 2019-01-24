using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScore : MonoBehaviour 
{
	#region Public 

	public TextMeshProUGUI m_text;

    #endregion


	#region System

	private void Awake()
    {
        m_currScore = PlayerPrefs.GetInt( "Score" );
    }
	private void Start()
	{
		m_text.SetText("{0}", m_currScore);
	}
	#endregion


	#region Private 

	private int m_currScore;

    #endregion
}

