using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HorseHealth : Damage
{
    #region Public

    [Header( "Infos" )]
    public int m_maxHP;
    public int m_speed;

    [Header( "Touche pas a sa pti Con" )]
    public int m_currentHealth;

    #endregion


    #region System 

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_isDead = false;
        m_vectorMove = new Vector3( 0 , 0 , 1 );
        m_currentHealth = m_maxHP;
    }
	protected override void Start()
    {
        m_horsesInfo = GameObject.Find( "Horse" ).GetComponent<GetChildrens>().m_horseList;
    }
    private void Update()
    {
        if ( m_isDead )
        {
            m_transform.position -= m_vectorMove * m_speed * Time.deltaTime;
        }
		//Debug.Log( m_defState );
    }
	#endregion


	#region Main

	public void HorseGetToutch( int _damage , int _index )
    {
		if ( !m_defBool )
        {
            m_currentHealth -= _damage;
            if ( m_currentHealth <= 0 )
            {
                int index = _index;
                HorseDead( index );
            }
        }
		else if ( m_defBool )
        {
			if ( m_currentHealth<m_maxHP)
			{
				m_currentHealth += 5;
				if ( m_currentHealth > m_maxHP )
					m_currentHealth = m_maxHP;
            }
        }
    }
	public void SwitchToDef()
	{
		m_defBool = !m_defBool;
	}
    private void HorseDead( int _index )
    {
        m_isDead = true;
        m_horsesInfo[ _index ].m_animator.SetTrigger( "Death" );
        Destroy( gameObject , 5f );
    }
    #endregion


    #region Private

    private Vector3 m_vectorMove;
	private bool m_defBool;
    private bool m_isDead;
    private Transform m_transform;
    private List<HorsesInfos> m_horsesInfo;

    #endregion

}
