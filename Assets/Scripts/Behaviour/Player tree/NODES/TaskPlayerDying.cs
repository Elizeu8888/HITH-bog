using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class TaskPlayerDying : Node
    {
        //PLAYER NODE
        private Animator _Anim;
        private Transform _transform;
        PlayerHealthAndDamaged plyHealth;
        CharacterController characterController;
        EventSwitch eventSwitch;

        float tim = 1f;

        PlayerBT _plyBT;
        public TaskPlayerDying(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            characterController = _transform.GetComponent<CharacterController>();
            eventSwitch = _transform.GetComponent<EventSwitch>();
            plyHealth = _transform.GetComponent<PlayerHealthAndDamaged>();
            _plyBT = _transform.GetComponent<PlayerBT>();
        }

        public override NodeState LogicEvaluate()
        {
            characterController.velocity.Set(0f,0f,0f);           

            

            _Anim.SetLayerWeight(3, 1f);


            if (!_Anim.GetCurrentAnimatorStateInfo(3).IsName("Death"))
            {
                _Anim.Play("Death", 3);
            }
            
            tim = Mathf.Lerp(1f, 0.1f, 0.2f);


            if(Time.timeScale <= 0.2)
            {
                Camera.main.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
                _transform.GetComponent<PlayerBT>()._DeathScreen.SetActive(true);
                PlayerBT._CanPressDeath = true;
            }
            else
            {
                Time.timeScale -= Time.deltaTime;
                Time.fixedDeltaTime = Time.timeScale;
            }

            

            state = NodeState.RUNNING;
            return state;
        }


    }
}
