using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class TrainInfo
{
    public int m_number;
    public GameObject m_wagon;
    public CinemachineVirtualCamera m_camera;
    public bool m_isDestroyed = false;
    public Vector3 m_length;
    public Vector3 m_position;
}
public class TrainManager : DualBehaviour
{
    #region Public

    [Header( "References" )]
    public GameObject m_prefabOfWagon;
    public GameObject m_prefabOfLocomotive;
    public GameObject m_prefabOfCar;
    public Transform m_trainOrigin;

    [Header( "Infos" )]
    public List<TrainInfo> m_trainInfos;
    public TrainInfo m_currentWagon;
    public TrainInfo m_nextWagon;

    [Header( "Measurement" )]
    public float m_locomotiveLength;
    public float m_wagonLength;
    public float m_carLength;

    [Header( "ForLerp" )]
    public float m_backSpeed;
    public float m_backTime;
    public float m_destroyWagonDist;
    public float m_speed;
    public float m_speedWagon;

	[HideInInspector]
	public bool m_onChange;

    #endregion


    #region System

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_difficulty = GetComponent<Difficulties>();
        m_wagonDestroyCount = 1;
    }

	protected override void  Start()
    {
        m_endGame = GameObject.Find( "Manager" ).GetComponent<MapManager>().m_endGame;
        m_wagonCount = 0;
        m_carCount = 0;
        GenerateTrain();
		m_onChange = false;
    }
    private void Update()
    {

        if ( m_launchedWagon )
        {
			m_onChange = true;
            float distCovered = ( Time.time - m_startTimeWagon ) * m_speedWagon;
            float fracJourney = distCovered / m_journeyLengthWagon;
            m_currentWagon.m_wagon.transform.position = Vector3.Lerp( m_startWagon , m_finishWagon , fracJourney );
            SlowTrain();
            if ( Mathf.Approximately( Vector3.Distance( m_currentWagon.m_wagon.transform.position , m_finishWagon ) , 0f ) )
            {
                Destroy( m_currentWagon.m_wagon , 2f );
                m_wagonDestroyCount++;
                m_currentWagon.m_isDestroyed = true;
                m_currentWagonIndex++;
                RefreshWagons();
                m_launchedWagon = false;
            }
        }
        if ( m_launched )
        {
            float distCovered = ( Time.time - m_startTime ) * m_speed;
            float fracJourney = distCovered / m_journeyLength;
            transform.position = Vector3.Lerp( m_start , m_finish , fracJourney );

            if ( Mathf.Approximately( Vector3.Distance( transform.position , m_finish ) , 0f ) )
            {
                Debug.Log( m_currentWagon.m_wagon.transform.position + " cur wagon pos" );
                m_launched = false;
				m_onChange = false;
            }

        }
        if ( m_wagonDestroyCount == m_trainInfos.Count )
        {
            SceneManager.LoadScene( "Win Screen" );
        }
    }

    #endregion


    #region Main

    private TrainInfo FindWagon( int i )
    {
        return m_trainInfos.Find( x => x.m_number == i );
    }

    private void RefreshWagons() // Find new current and next wagon 
    {
        m_currentWagon = FindWagon( m_currentWagonIndex );
        m_nextWagon = FindWagon( m_currentWagonIndex + 1 );
    }


    private void GenerateTrain()
    {
        m_currentWagonIndex = 1;
        m_wagonPosition = m_trainOrigin.transform.position;
        for ( int i = 0 ; i <= m_difficulty.m_trainLength ; i++ )
        {
            GameObject curwagon = GetRandomWagon();
            switch ( curwagon.name )
            {
                case "Car":
                    m_carCount++;
                    m_counter = m_carCount;
                    break;

                case "Wagon":
                    m_wagonCount++;
                    m_counter = m_wagonCount;
                    break;

            }
            if ( i == 0 ) // first one to spawn
            {
                GenerateNextWagon( curwagon , true );
            }
            else if ( m_difficulty.m_trainLength == i ) // locomotive in front
            {
                GenerateNextWagon( m_prefabOfLocomotive , false );
                m_counter = 1;
            }
            else // every other one
            {
                GenerateNextWagon( curwagon , false );
            }
        }
        RefreshWagons();
    }


    private void GenerateNextWagon( GameObject _toGenerate , bool _isFirst )
    {

        TrainInfo trainInfo = new TrainInfo();

        if ( _isFirst )
        {
            trainInfo.m_wagon = Instantiate( _toGenerate , m_trainOrigin.GetComponent<Transform>().position , Quaternion.identity );

        }
        else
        {
            GameObject previousWagon = m_trainInfos[ m_trainInfos.Count - 1 ].m_wagon;
            m_wagonPosition.z += previousWagon.GetComponent<Renderer>().bounds.extents.z;
            m_wagonPosition.z += _toGenerate.GetComponent<Renderer>().bounds.extents.z;
            trainInfo.m_wagon = Instantiate( _toGenerate , m_wagonPosition , Quaternion.identity );

        }

        trainInfo.m_wagon.name = _toGenerate.name + " " + m_counter;
        // communes 
        trainInfo.m_camera = trainInfo.m_wagon.GetComponentInChildren<CinemachineVirtualCamera>();
        trainInfo.m_camera.Priority = m_difficulty.m_trainLength - m_trainInfos.Count;
        trainInfo.m_number = m_trainInfos.Count + 1;
        trainInfo.m_position = m_wagonPosition;
        trainInfo.m_length = _toGenerate.GetComponent<Renderer>().bounds.extents;


        trainInfo.m_wagon.transform.parent = transform;
        m_trainInfos.Add( trainInfo );
    }

    private GameObject GetRandomWagon()
    {
        GameObject[] arrayOfWagon = { m_prefabOfCar , m_prefabOfWagon };
        return arrayOfWagon[ UnityEngine.Random.Range( 0 , arrayOfWagon.Length ) ];
    }

    public void GoToNextWagon()
    {
        //change wagon camera
        CinemachineVirtualCamera cvc = FindWagon( m_currentWagonIndex + 1 ).m_camera;
        cvc.Priority = m_currentWagon.m_camera.Priority + 1;
        StartCoroutine( WaitForCameraTransition( cvc ) ); // start coroutine to move the trian and destroy wagon;
    }

    private IEnumerator WaitForCameraTransition( CinemachineVirtualCamera _cvc )
    {
        while ( !CinemachineCore.Instance.IsLive( _cvc ) )
        {
            yield return null;
        }
        SlowWagon();
    }

    public int FindCurrentWagonToStop()
    {
        return m_currentWagonIndex;
    }

    private void SlowTrain()
    {
        m_startTime = Time.time;
        m_start = transform.position;
        m_finish = m_start;
        m_finish.z -= m_currentWagon.m_length.z + m_trainInfos[ m_currentWagonIndex ].m_length.z;
        //m_finish.z -= m_currentWagon.m_length.z;
        //Debug.Log( m_finish + " m_finish" );
        m_journeyLength = Vector3.Distance( m_start , m_finish );
        m_launched = true;
    }

    private void SlowWagon()
    {
        m_startTimeWagon = Time.time;
        m_startWagon = m_currentWagon.m_wagon.transform.position;
        m_finishWagon = m_currentWagon.m_wagon.transform.position;
        m_finishWagon.z -= 30f;
        m_journeyLengthWagon = Vector3.Distance( m_startWagon , m_finishWagon );
        m_launchedWagon = true;
    }

    #endregion


    Vector3 m_start;
    Vector3 m_finish;

    float m_journeyLength;
    float m_startTime;

    Vector3 m_startWagon;
    Vector3 m_finishWagon;

    float m_journeyLengthWagon;
    float m_startTimeWagon;


    #region Debug and tools


    #endregion


    #region Private

	private Transform m_transform;
	private Difficulties m_difficulty;

    private Vector3 m_Position;
    private Vector3 m_wagonPosition;

    private Renderer m_rendWagon;
    private Renderer m_rendCar;
    private Renderer m_rendLocomotive;

    private int m_counter;
    private int m_carCount;
    private int m_wagonCount;
    private int m_random;
    private int m_currentWagonIndex;
    private int m_wagonDestroyCount;

	private bool m_firstWagonInstantiate;
    private bool m_launched;
    private bool m_launchedWagon;
    private bool m_endGame;

    #endregion
}
