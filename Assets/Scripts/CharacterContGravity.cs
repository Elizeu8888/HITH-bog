using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterContGravity : MonoBehaviour
{
    public Transform _GroundedCheckPoint;
    public float _castRadius;
    public LayerMask hitLayer;
    CharacterController _CharControl;
    public bool usingnav;
    NavMeshAgent _NavMesh;
    public bool grounded;

    void Start()
    {
        _CharControl = transform.GetComponent<CharacterController>();
        if(usingnav)
        {
            _NavMesh = transform.GetComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        IsGrounded();

        if(grounded == true)
        {
            return;
        }
        else
        {
            _CharControl.Move(-transform.up * 19f * Time.deltaTime);
            if(usingnav && _NavMesh.enabled == true)
            {
                print("dsf");
                //_NavMesh.enabled = false;
            }
        }
    }


    public void IsGrounded()
    {
        grounded = (Physics.CheckSphere(_GroundedCheckPoint.position, _castRadius, hitLayer));

    }
}
