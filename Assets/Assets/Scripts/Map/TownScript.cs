using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownScript : MonoBehaviour
{
    #region Public
    
    [Header( "Measurement" )]
    public float m_speed;
    public GameObject m_endOfTrain;

    #endregion


    #region System

    private void Awake()
    {
        m_locoFront = GameObject.Find( "LocoFront" ); // arriver
        m_transform = GetComponent<Transform>();
        m_vectorMove = new Vector3( 0 , 0 , 1 );
        m_endDestination = false;

    }
	private void Update()
    {
        
        if ( !m_endDestination)
        {
			m_transform.position -= m_vectorMove * m_speed * Time.deltaTime;
            if ( m_endOfTrain.transform.position.z <= m_locoFront.transform.position.z )
            {
				m_endDestination = true;            
            }
        }
		if ( m_endDestination)
		{
			SceneManager.LoadScene( "Win Screen" );
		}
    }

    #endregion


    #region Private

    private Transform m_transform;
    private GameObject m_locoFront;
    private Vector3 m_vectorMove;
	private bool m_endDestination;

    #endregion

}
