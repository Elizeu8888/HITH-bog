using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskPlayerRoll : Node
    {

        private Animator _Anim;
        private Transform _transform;
        private Transform cam;

        float _2ndLayerWeight;
        Vector3 direction;
        int dashNum;

        float turnsmoothing = 0.1f;
        float turnsmoothvelocity = 0.5f;



        private CharacterController _CharacterController;

        public TaskPlayerRoll(Transform transform, Transform camera)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            cam = camera;
            _CharacterController = transform.GetComponent<CharacterController>();
        }


        Vector3 lookPos;
        Quaternion targetRot;

        public override NodeState LogicEvaluate()
        {


            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


            _transform.GetComponent<WeaponAttack>().comboPossible = true;
            _transform.GetComponent<WeaponAttack>().comboStep = 0;

            _transform.GetComponent<WeaponAttack>().dashDir = dashNum;


            targetRot = Quaternion.LookRotation(direction);
            _transform.rotation = targetRot;

            
            Debug.Log("rolling");

            state = NodeState.RUNNING;
            return state;

        }



    }
}
