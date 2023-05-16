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

        float turnsmoothing = 1f;
        float turnsmoothvelocity = 1f;



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


            _transform.GetComponent<WeaponAttack>().comboPossible = true;
            _transform.GetComponent<WeaponAttack>().comboStep = 0;

            _transform.GetComponent<WeaponAttack>().dashDir = dashNum;


            //float targetangle = Mathf.Atan2(_CharacterController.velocity.x, _CharacterController.velocity.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;// finds direction of movement


            //float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
            float velDirection = Mathf.Atan2(_CharacterController.velocity.x, _CharacterController.velocity.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, velDirection, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
            _transform.rotation = Quaternion.Euler(0f, angle, 0f);



            Debug.Log("rolling");

            state = NodeState.RUNNING;
            return state;

        }



    }
}
