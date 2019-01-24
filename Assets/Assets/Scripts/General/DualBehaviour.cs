using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualBehaviour : MonoBehaviour
{
   
    #region System

    protected virtual void Start()
	{
        m_OriginOfTrain = GameObject.Find( "OriginOfTrain" );
	}

	#endregion


	#region Main
    
	protected int GetRandom( int _min , int _max )
    {
        var i = Random.Range( _min , _max );
        return i;
    }

	#endregion


	#region Private and Protected


    protected GameObject m_OriginOfTrain;

    #endregion
}
