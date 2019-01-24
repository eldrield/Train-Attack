using UnityEngine;

public class Difficulties : MonoBehaviour {

	#region Public Members

	public int m_difficulty;

	[Header("Base elements")]
	public int m_nbWagonBase = 5;
	public int m_timerBaseInSeconds = 90;
	[Range( 1.0f, 25.0f )]
	public float m_percentModifier;

	#endregion


	#region Public Main Methods

	// Les méthodes principales
	protected void SetDifficulties ()
	{
		if(m_difficulty == 0)
		{
			Debug.Log( "Difficulty must be set" );
			return;
		}

		m_trainLength = GetWagonCount();
		m_timerCount = GetTimerCount();
	}

	private int GetWagonCount ()
	{
		int nbWagons = 0;
		nbWagons = Mathf.RoundToInt((m_nbWagonBase * (m_difficulty*0.4f)) + m_nbWagonBase);
		nbWagons = AlterPercent( nbWagons );
		return nbWagons;
	}

	private int GetTimerCount ()
	{
		int timer = 0;
		timer = Mathf.RoundToInt((m_timerBaseInSeconds * (m_difficulty*0.7f))+(m_timerBaseInSeconds/m_difficulty));
		timer = AlterPercent( timer );
		return timer;
	}

	private int AlterPercent(int _value )
	{
		float percent = ( m_percentModifier / 100 ) * _value;
		int op = Random.Range( 1, 2 );
		float alter = Random.Range( 0f, percent );
		if(op == 1 )
		{
			_value = Mathf.RoundToInt( _value + alter );
		} else
		{
			_value = Mathf.RoundToInt( _value - alter );
		}
		return _value;
	}
	#endregion


	#region System

	private void Awake()
	{
		m_difficulty = PlayerPrefs.GetInt( "Difficulty" , 1 );
	}
	void Start ()
	{
		//SetDifficulties();
		switch (m_difficulty)
		{
			case 1 :
				m_trainLength = 5;
				m_timerCount = 90;
				break;
			case 2 :
				m_trainLength = 7;
				m_timerCount = 120;
				break;
			case 3 :
				m_trainLength = 8;
				m_timerCount = 140;
				break;
			case 4 :
				m_trainLength = 8;
				m_timerCount = 120;
				break; 
			case 5 :
				m_trainLength = 10;
				m_timerCount = 160;
				break;
		}
	}

	#endregion


	#region Debug and Tools

	// Les méthodes de debug et les outils permettant de faire fonctionner la class

	#endregion


	#region Private and Protected Members

	public int m_trainLength;
	public int m_timerCount;

	#endregion
}
