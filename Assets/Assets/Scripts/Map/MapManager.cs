using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[Serializable]
public class MapInfos
{
	public string m_name;
    public GameObject m_ground;
    public float m_groundLength;
}

[Serializable]
public class TownInfos
{
    public string m_name;
    public GameObject m_town;
    public float m_groundLength;
}
public class MapManager : DualBehaviour
{
    #region Public

    [Header ("Infos")]
    public bool m_endGame;
    [Space (10)]

    public List<MapInfos> m_mapInfos;
    [Space(10)]

    public List<TownInfos> m_townInfos;

	#endregion


	#region System

	protected override void Start()
	{
        base.Start();
        m_difficulties = m_OriginOfTrain.GetComponent<Difficulties>();
        m_timer = m_difficulties.m_timerCount;
        m_endGame = false;
	}
	private void Update()
	{
        if ( Time.time > m_timer)
        {
            m_endGame = true;
        }
	}

	#endregion


	#region Main

    private void SpawnTown()
    {
        
    }

    #endregion


    #region Private 
   
	private float m_timer;
    private Difficulties m_difficulties;

    #endregion

}
