using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HorseInfo
{
    public GameObject m_horse;
    public Slider m_healthSlider;
    public string m_emplacement;
    public Image m_image;
}

public class HorseHealthBar : DualBehaviour
{
    #region Public

    public List<HorseInfo> m_horseInfos;

    #endregion

    #region System 

    private void Awake()
    {
        m_maxValue = m_horseInfos[ 0 ].m_horse.GetComponent<HorseHealth>().m_maxHP;
    }
    private void Update()
    {
        for ( int i = 0 ; i < m_horseInfos.Count ; i++ )
        {
			if ( m_horseInfos[i].m_horse == null)
			{
				m_horseInfos.RemoveAt( i );
			}
            float currHp = m_horseInfos[ i ].m_horse.GetComponent<HorseHealth>().m_currentHealth;
            m_value = ( ( currHp * 100 ) / m_maxValue ) / 100;
            m_horseInfos[ i ].m_healthSlider.value = m_value;
            m_horseInfos[ i ].m_image.color = Color.Lerp( Color.red , Color.green , ( float ) m_value / 1f );
        }
    }
        
    #endregion


    #region Private

    private float m_value;
    private float m_maxValue;

    #endregion

}
