using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : DualBehaviour
{
    #region System

    private void Start()
    {
        m_timerLeft = GameObject.Find( "TimesLeftCount" ).GetComponent<TextMeshProUGUI>();
        m_difficulties = GameObject.Find( "OriginOfTrain" ).GetComponent<Difficulties>();
    }
	private void Update()
	{
        if ( m_time-Time.time >= 0)
        {
			m_time = m_difficulties.m_timerCount;
			m_timerLeft.SetText( "{0}" , ( m_time - Time.time ) );
        }else 
        {
            m_timerLeft.SetText( "0" );
        }
	}

    #endregion


    #region Private

    private float m_time;
    private TextMeshProUGUI m_timerLeft;
    private Difficulties m_difficulties;

	#endregion
}
