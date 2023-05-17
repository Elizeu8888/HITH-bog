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
                
            float horizontal = InputManager.movementInput.x;
            float vertical = InputManager.movementInput.y;// uses input to find direction

            if (horizontal < 0.2f)
            {
                if (vertical > 0f)
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
