using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonHealth : DualBehaviour
{
    #region Public

    public float m_maxHealth;
    public int m_score;

    #endregion


    #region System

    private void Awake()
    {
        m_health = m_maxHealth;
        m_score = 0;
    }
	private void Update()
	{
		PlayerPrefs.SetInt( "Score" , m_score );
	}

	#endregion


	#region Main

	public void WagonLoseHp( float _damage )
    {
        m_health -= _damage;
        // doit réafficher les pv
        if ( m_health <= 0 )
        {

            TrainManager trainManager = m_OriginOfTrain.GetComponent<TrainManager>();
            //incrémente le score
            int score = trainManager.m_currentWagon.m_wagon.GetComponent<WagonPoint>().m_wagonScore;
            m_score += score;
            //change de wagon
            trainManager.GoToNextWagon();
            //redonne pv max
            m_health = m_maxHealth;
            // reafficher les pv
        }
    }

    #endregion


    #region Private 

    public float m_health;

    #endregion
}
