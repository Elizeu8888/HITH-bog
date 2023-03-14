using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckInCombat : Node
    {
        bool _incombat = false;

        private Animator _Anim;
        private Transform _transform;

        float _2ndLayerWeight;

        public CheckInCombat(Transform transform, bool incombat)
        {
            incombat = _incombat;

            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }


        public override NodeState LogicEvaluate()
        {
            if (Input.GetKeyDown("r") && !_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw") && !_Anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
            {
                _incombat = !_incombat;
                if (_Anim.GetBool("InCombat") == true)
                    _Anim.SetBool("InCombat", false);


            }
            if (_Anim.GetBool("InCombat") == true)
            {
                _incombat = true;
            }




            if (_incombat == true)
            {
                if (_2ndLayerWeight <= 1)
                    _Anim.SetLayerWeight(1, _2ndLayerWeight += 3f * Time.deltaTime);

                if (_Anim.GetBool("InCombat") == false)
                    _Anim.SetBool("EnteringCombat", true);
                else
                    _Anim.SetBool("EnteringCombat", false);


                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                if(_2ndLayerWeight >= 0)
                    _Anim.SetLayerWeight(1, _2ndLayerWeight -= 1.2f * Time.deltaTime);


                _Anim.SetBool("EnteringCombat", false);


                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}
