using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaCam : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook freeCamera;
    public Transform target;
    public Transform player;



    private void Awake()
    {
        freeCamera = GetComponent<Cinemachine.CinemachineFreeLook>();
    }
    public void SetCamTarget()
    {
        freeCamera.m_Priority = 1;
        freeCamera.m_LookAt = target.transform;
        freeCamera.m_Follow = target.transform;
        
    }

    public void SetCamTargetToPlayer()
    {
        freeCamera.m_Priority = 15;
        freeCamera.m_LookAt = player.transform;
        freeCamera.m_Follow = player.transform;

    }

}
