using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskDashAttack : Node
    {

        private Animator _Anim;
        private Transform _transform;

        float _2ndLayerWeight;
        Vector3 direction;
        int dashNum;

        public TaskDashAttack(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            
        }


        public override NodeState LogicEvaluate()
        {
                
            if (_Anim.GetFloat("InputX") == 0f)
            {
                if (_Anim.GetFloat("InputY") > 0f)
                {
                    dashNum = 0;
                }
                if (_Anim.GetFloat("InputY") < 0f)
                {
                    dashNum = 1;
                }
            }
            if (_Anim.GetFloat("InputX") > 0f)
            {
                dashNum = 2;
            }
            if (_Anim.GetFloat("InputX") < 0f)
            {
                dashNum = 3;
            }

            _transform.GetComponent<WeaponAttack>().comboPossible = true;
            _transform.GetComponent<WeaponAttack>().comboStep = 0;

            _transform.GetComponent<WeaponAttack>().dashDir = dashNum;

            if (!_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                PlayerBT._WeapAttack.DashAttack();
            }

            state = NodeState.RUNNING;
            return state;

        }



    }
}
