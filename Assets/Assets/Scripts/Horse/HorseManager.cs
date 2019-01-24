using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManager : DualBehaviour
{
    #region Public

    public GameObject m_horsePrefabRight;
    public GameObject m_horsePrefabLeft;
    public float m_xPos;

	#endregion


	#region System

	private void Awake()
	{
        m_originOfTrain = GameObject.Find( "OriginOfTrain" );
	}

	private void Start()
	{
        HorseInstBotRight();
        HorseInstBotLeft();
        HorseInstTopRight();
        HorseInstTopLeft();
	}

    #endregion


    #region Main

    private void HorseInstBotRight()
    {
        Vector3 pos = m_originOfTrain.transform.position;
        pos.x += m_xPos;
        pos.z -= 3;
        Instantiate( m_horsePrefabRight , pos, Quaternion.identity);
    }

    private void HorseInstTopRight()
    {
        Vector3 pos = m_originOfTrain.transform.position;
        pos.x += m_xPos;
        pos.z += 3;
        Instantiate( m_horsePrefabRight , pos , Quaternion.identity );
    }

    private void HorseInstTopLeft()
    {
        Vector3 pos = m_originOfTrain.transform.position;
        pos.x -= m_xPos;
        pos.z += 3;
        Instantiate( m_horsePrefabLeft , pos , Quaternion.identity );
    }

    private void HorseInstBotLeft()
    {
        Vector3 pos = m_originOfTrain.transform.position;
        pos.x -= m_xPos;
        pos.z -= 3;
        Instantiate( m_horsePrefabLeft , pos , Quaternion.identity );
    }

    #endregion


    #region Private

    private GameObject m_originOfTrain;

    #endregion

}
