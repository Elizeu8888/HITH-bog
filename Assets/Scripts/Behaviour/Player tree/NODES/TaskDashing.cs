using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class TaskDashing : Node
    {
        private Animator _Anim;
        private Transform _transform;

        Transform cam;
        float targetangle = 0f;

        private CharacterController _CharacterController;

        Vector3 direction = new Vector3(0,0,0);


        float turnsmoothing = 0.12f;
        float turnsmoothvelocity = 0.45f;

        public TaskDashing(Transform transform, Transform camera)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _CharacterController = transform.GetComponent<CharacterController>();
            cam = camera;
        }

        public override NodeState LogicEvaluate()
        {
            




            if(_Anim.GetCurrentAnimatorStateInfo(1).IsTag("Attack"))
            {
                float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, cam.transform.eulerAngles.y, ref turnsmoothvelocity, turnsmoothing);
                _transform.rotation = Quaternion.Euler(0f, angle, 0f);

                state = NodeState.RUNNING;
                return state;
            }



            float horizontal = InputManager.movementInput.x;
            float vertical = InputManager.movementInput.y;// uses input to find direction

            if (horizontal < 0.2f)
            {
                if (vertical > 0f)
                {
                    _Anim.SetFloat("InputY", 1);
                }
                if (vertical < 0f)
                {
                    _Anim.SetFloat("InputY", -1f);
                }
            }
            if (horizontal > 0.2f)
            {
                _Anim.SetFloat("InputY", 1);
            }
            if (horizontal < -0.2f)
            {
                _Anim.SetFloat("InputY", -1);
            }


            // finds direction of movement
            if(_Anim.GetBool("Dashing") == false)
            {
                if(horizontal == 0f && vertical == 0f)
                {
                    direction = new Vector3(0f, 0f, -1f).normalized;
                }
                else
                    direction = new Vector3(horizontal, 0f, vertical).normalized;


                targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            }

            // here is the movement
            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;

            
            _CharacterController.Move(movedir.normalized * 16f * Time.deltaTime);


            if (_transform.GetComponent<PlayerHealthAndDamaged>().rolling == true)
            {

                //_transform.GetComponent<WeaponAttack>().dashDir = dashNum;

                float velDirection = Mathf.Atan2(_CharacterController.velocity.x, _CharacterController.velocity.z) * Mathf.Rad2Deg;

                float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, velDirection, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
                _transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }


            //here it sets the animation parameters for strafing. it first gets input from the mouses X input so it can add to make the character move their feet when rotating

            _Anim.SetBool("Dashing", true);
            _Anim.SetBool("Moving", false);
            _Anim.SetBool("Sprinting", false);

            state = NodeState.RUNNING;
            return state;


        }


    }
}

