using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : TrainManager
{

	#region System

	private void Awake()
	{
		m_transform = GetComponent<Transform>();
    }

    #endregion


    #region Main


    #endregion




    #region Private

    private Transform m_transform;

    #endregion
}
