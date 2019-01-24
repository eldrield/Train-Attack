using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WagonHealthBar : DualBehaviour
{
	#region Public

	public Image m_sliderFill;

	#endregion


	#region System

	private void Awake()
	{
        m_healthSlider = GameObject.Find("WagonHealthBar").GetComponent<Slider>();
        m_wagonHealth = gameObject.GetComponent<WagonHealth>();
        m_maxValue = m_wagonHealth.m_maxHealth;
		//m_wagonHealth = gameObject.GetComponent<WagonHealth>();
    }
    private void Update()
    {
		m_value = (( m_wagonHealth.m_health * 100 ) / m_maxValue)/100;
		m_healthSlider.value = m_value;
		m_sliderFill.color = Color.Lerp( Color.red , Color.green , ( float ) m_value / 1f);
	}
	#endregion

	#region Debug

	//private void OnGUI()
	//{
	//	GUILayout.Button(m_value +"current / "+ m_maxValue+" max" ) ; 
	//}

	#endregion

	#region Private

	private float m_maxValue;
	private float m_value;
	private Slider m_healthSlider;
    private WagonHealth m_wagonHealth;

    #endregion

}
