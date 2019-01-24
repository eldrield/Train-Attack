using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SelectOnInput : MonoBehaviour
{
    #region Public

    public EventSystem eventSystem;
    public GameObject selectedObject;

    #endregion


    #region Main

    void Update ()
    {
        if ( Input.GetAxisRaw( "Vertical" ) != 0 && m_ButtonSelect == false)
        {
            eventSystem.SetSelectedGameObject( selectedObject );
            m_ButtonSelect = true;
        }
	}

    private void OnDisable()
    {
        m_ButtonSelect = false;
    }
    #endregion


    #region Private

    private bool m_ButtonSelect;

    #endregion
}
