using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoove : MapManager
{
    #region Public

    [Header( "Infos" )]
    public MapManager m_mapManager;

    [Header( "Measurement" )]
    public float m_speed;
    public float m_endGround;
    public float m_instantiatePosZ;

    #endregion


    #region System

    private void Awake()
    {
        m_mapManager = GameObject.Find( "Manager" ).GetComponent<MapManager>();
        m_transform = GetComponent<Transform>();
        m_vectorMove = new Vector3( 0 , 0 , 1 );
        m_isInstantiate = false;
    }

    private void Update()
    {
        m_endOfGame = GameObject.Find( "Manager" ).GetComponent<MapManager>().m_endGame;
        m_transform.position -= m_vectorMove * m_speed * Time.deltaTime; // Déplacement
        if ( !m_endOfGame )
        {
            if ( m_transform.position.z < m_endGround & !m_isInstantiate )
            {
                m_isInstantiate = !m_isInstantiate;
                Instantiate( m_mapManager.m_mapInfos[ GetRandom( 0 , m_mapManager.m_mapInfos.Count ) ].m_ground , new Vector3( 0 , 0 , m_instantiatePosZ ) , Quaternion.identity );
            }
            if ( m_transform.position.z < m_endGround * 2 & m_isInstantiate )
            {
                m_isInstantiate = !m_isInstantiate;
                Destroy( gameObject );
            }
        }
        else if ( m_endOfGame )
        {
            if ( m_transform.position.z < m_endGround & !m_isInstantiate )
            {
                m_isInstantiate = !m_isInstantiate;
                Instantiate( m_mapManager.m_townInfos[ GetRandom( 0 , m_mapManager.m_townInfos.Count ) ].m_town , new Vector3( 0 , 0 , m_instantiatePosZ ) , Quaternion.identity );
            }
            if ( m_transform.position.z < m_endGround * 2 & m_isInstantiate )
            {
                m_isInstantiate = !m_isInstantiate;
                Destroy( gameObject );
            }
        }
    }

    #endregion

    #region Main



    #endregion

    #region Private

    private bool m_endOfGame;
    private bool m_isInstantiate;
    private Vector3 m_vectorMove;
    private Transform m_transform;

    #endregion

}
