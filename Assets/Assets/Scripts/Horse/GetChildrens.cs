using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HorsesInfos
{
    public GameObject m_horse;
    public Transform m_transform;
    public bool m_isRight;
    public Animator m_animator;
}
public class GetChildrens : DualBehaviour
{
    #region Public

    public List<HorsesInfos> m_horseList;

    #endregion

}
