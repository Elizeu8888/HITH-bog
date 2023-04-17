using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskCombatIdle : Node
    {
        private Animator _Anim;

        float turnsmoothing = 0.1f;
        float turnsmoothvelocity = 0.5f;

        Transform _transform, cam;


        public TaskCombatIdle(Transform transform, Transform camera)
        {
            _Anim = transform.GetComponent<Animator>();
            _transform = transform;
            cam = camera;
            
        }


        public override NodeState LogicEvaluate()
        {
            _Anim.SetLayerWeight(1, 1);

            //rotation

            float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, cam.transform.eulerAngles.y, ref turnsmoothvelocity, turnsmoothing);
            _transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Animations

            _Anim.SetBool("Moving", false);
            state = NodeState.RUNNING;
            return state;
        }

    }
}