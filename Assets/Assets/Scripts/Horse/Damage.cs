using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : DualBehaviour
{
	#region Public 

	[Header( "References" )]
	public GameObject m_shootButton;
	public GameObject m_reloadButton;
	public GameObject m_canonTip;
	public GameObject m_smokeOfShoot;

	[Header("Audio")]
    public AudioSource m_reloadAudio;
    public AudioSource m_shootAudio;

	[Header("Measurement")]
    public float m_damage;
    public int m_health;

    #endregion


    #region System

    private void Awake()
    {
        m_maxHealth = m_health;
        m_maxBullet = 2;
        m_currentBullet = m_maxBullet;
        m_canShoot = true;
        m_reloadTime = 2f;
		m_onReload = false;
    }

    protected override void Start()
    {
        base.Start();
		switch ( m_OriginOfTrain.GetComponent<Difficulties>().m_difficulty)
		{
			case 1 :
				m_damage = 15 / 4;
				break;
			case 2 :
				m_damage = 17 / 4;
				break;
			case 3 :
				m_damage = 20 / 4;
				break;
			case 4 :
				m_damage = 20 / 4;
				break;
			case 5 :
				m_damage = 25 / 4;
				break;
		}
    }
	private void Update()
	{
        m_endGame = GameObject.Find( "Manager" ).GetComponent<MapManager>().m_endGame;
        if ( m_currentBullet == 0)
		{
			m_reloadButton.SetActive( true );
			m_shootButton.SetActive( false );
		}else
		{
			m_reloadButton.SetActive( false );
			m_shootButton.SetActive( true );
		}
        
	}

	#endregion


	#region Main

	public void HorseShoot()
    {
        if ( m_canShoot && m_currentBullet > 0 && !m_endGame )
        {
            m_OriginOfTrain.GetComponent<WagonHealth>().WagonLoseHp( m_damage );
            m_currentBullet--;
			m_shootAudio.Play();
			Instantiate( m_smokeOfShoot , m_canonTip.transform.position , Quaternion.identity );
			if ( m_currentBullet <= 0 )
			{
				m_canShoot = false;
			}
		}
        // else 
        //lance un timer de récupération //plus tard
    }
    public void Reload()
    {
		if ( !m_endGame && !m_onReload)
        {
			m_onReload = true;
			m_reloadAudio.Play();
			StartCoroutine( WaitForReload() );
        }
    }
	private IEnumerator WaitForReload()
	{
		yield return new WaitForSeconds( 2.5f );
		AddBullet();
	}
    private void AddBullet()
	{
		m_currentBullet = m_maxBullet;
		if ( !m_defState)
		{
			m_canShoot = true;
        }
		m_onReload = false;
	}

    public void SwitchMode()
    {
		if ( m_currentBullet > 0)
        {
            m_canShoot = !m_canShoot;
        }
        m_defState = !m_defState;
        //Debug.Log( m_defState + name );
    }

    #endregion


    #region Debug & tools

   

    #endregion


    #region Private & Protected

	public bool m_defState;

    private float m_reloadTime;

    private bool m_endGame;
    private bool m_canShoot;
	private bool m_onReload;

    private int m_maxHealth;
    private int m_maxBullet;
    private int m_currentBullet;

    #endregion
}
