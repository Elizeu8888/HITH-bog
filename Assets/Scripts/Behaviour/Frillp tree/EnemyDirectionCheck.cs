using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BehaviorTree;

namespace EnemyManager
{
    public class EnemyDirectionCheck : MonoBehaviour
    {
        EnemyMediumBT _EnemyBT;
        public bool leftOb,rightOb,forwardOb,backOb;
        public Transform[] directions;
        public LayerMask lyMask;

        void Start()
        {
            _EnemyBT = gameObject.GetComponent<EnemyMediumBT>();
        }


        void Update()
        {

            if(Physics.Raycast(transform.position, (directions[0].position - transform.position), out RaycastHit lhit, lyMask))
            {

                leftOb = true;
            }
            else
            {
                leftOb = false;
            }
            if (Physics.Raycast(transform.position, (directions[1].position - transform.position), out RaycastHit rhit, lyMask))
            {

                rightOb = true;
            }
            else
            {
                rightOb = false;
            }
            if (Physics.Raycast(transform.position, (directions[2].position - transform.position), out RaycastHit fhit, lyMask))
            {

                forwardOb = true;
            }
            else
            {
                forwardOb = false;
            }
            if (Physics.Raycast(transform.position, (directions[3].position - transform.position), out RaycastHit bhit, lyMask))
            {

                backOb = true;
            }
            else
            {
                backOb = false;
            }

        }
    }

}



