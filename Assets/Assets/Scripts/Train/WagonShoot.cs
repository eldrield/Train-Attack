using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[Serializable]
public class AudioShoot
{
    public AudioSource m_audio;
}
public class WagonShoot : DualBehaviour
{
    #region Public

    [Header( "Info" )]
    public int m_damage;
    public float m_timeBetweenBullet;
    public Canvas m_gameOverCanvas;
	public GameObject m_gunSmoke;

    [Header( "List" )]
    public List<HorsesInfos> m_horsesList;
	public List<AudioShoot> m_shootAudioList;

    #endregion


    #region System

    protected override void Start()
    {
		base.Start();
		switch ( m_OriginOfTrain.GetComponent<Difficulties>().m_difficulty )
        {
            case 1:
				m_damage = 5;
                break;
            case 2:
				m_damage = 6;
                break;
            case 3:
				m_damage = 6;
                break;
            case 4:
				m_damage = 6;
                break;
            case 5:
				m_damage = 7;
                break;
        }
        m_horsesList = GameObject.Find( "Horse" ).GetComponent<GetChildrens>().m_horseList;
        StartCoroutine( TimedShoot() );
    }
    private void Update()
    {
        if ( m_horsesList.Count < 1 )
        {
			SceneManager.LoadScene( "GameOver" );
        }
    }
    #endregion


    #region Main

    public IEnumerator TimedShoot()
    {
		yield return new WaitForSeconds( 2 );
        while ( true )
        {
            Shoot();
            yield return new WaitForSeconds( m_timeBetweenBullet );
        }
    }

    private void Shoot()
    {
		m_onChangeWagon = m_OriginOfTrain.GetComponent<TrainManager>().m_onChange;

		Debug.Log( m_onChangeWagon );
		if ( !m_onChangeWagon )
		{
			int i = GetRandom( 0 , m_horsesList.Count );
			int f = GetRandom( 0 , m_shootAudioList.Count );
			m_horsesList[ i ].m_horse.GetComponent<HorseHealth>().HorseGetToutch( m_damage , i );
			Vector3 pos = m_OriginOfTrain.GetComponent<TrainManager>().m_currentWagon.m_wagon.transform.position;
			pos.z = m_horsesList[ i ].m_horse.transform.position.z;
			if ( m_horsesList[ i ].m_isRight )
			{
				pos.x += m_OriginOfTrain.GetComponent<TrainManager>().m_currentWagon.m_length.x + 0.5f;
			}
			else
			{
				pos.x -= m_OriginOfTrain.GetComponent<TrainManager>().m_currentWagon.m_length.x + 0.5f;
			}
			Instantiate( m_gunSmoke , pos , Quaternion.identity );
			m_shootAudioList[ f ].m_audio.Play();
			if ( m_horsesList[ i ].m_horse.GetComponent<HorseHealth>().m_currentHealth <= 0 )
			{
				m_horsesList.RemoveAt( i );
				if ( m_horsesList.Count <= 0 )
				{
					StartCoroutine( "GameOver" );
				}

			}

		}
    }
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds( 2 );
        //for ( int i = 0 ; i <= m_canvasInfo.Count ; i++ )
        //{
        //    m_canvasInfo[ i ].m_canvas.enabled = false;
        //}
        //m_gameOverCanvas.enabled = true;
        SceneManager.LoadScene( "GameOver" );
    }

	#endregion


	#region Private

	private bool m_onChangeWagon;

    #endregion
}