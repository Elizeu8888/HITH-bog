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

            Debug.Log("running dashattack");
            if (_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                state = NodeState.RUNNING;
                return state;
            }


            float horizontal = InputManager.movementInput.x;
            float vertical = InputManager.movementInput.y;// uses input to find direction

            if (horizontal < 0.2f)
            {
                if (vertical >= 0f)
                {
                    dashNum = 0;
                }
                if (vertical < 0f)
                {
                    dashNum = 1;
                }
            }
            if (horizontal > 0.2f)
            {
                dashNum = 2;
            }
            if (horizontal < -0.2f)
            {
                dashNum = 3;
            }
            

            Debug.Log(dashNum);

        

           

            _transform.GetComponent<WeaponAttack>().dashDir = dashNum;


            if (!_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {

                PlayerBT._WeapAttack.DashAttack();
                _transform.GetComponent<WeaponAttack>().comboPossible = false;
            }

            state = NodeState.RUNNING;
            return state;

        }



    }
}
