using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public class ScoreInfo
{
	public int m_score;
}
[Serializable]
public class ScoreText
{
	public TextMeshProUGUI m_text;
}
public class Score : MonoBehaviour
{
	#region Public 

	public List<ScoreInfo> m_scoreList;
	public List<ScoreText> m_scoreTextList;

	#endregion


	#region System

	private void Awake()
	{
		m_currScore = PlayerPrefs.GetInt( "Score", 0 );
	}

	#endregion


	#region Private 

	private int m_currScore;

	#endregion
}
