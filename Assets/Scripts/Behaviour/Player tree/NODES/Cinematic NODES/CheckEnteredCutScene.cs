using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class CheckEnteredCutScene : Node
    {
        private Transform _transform;
        private Animator _Anim;
        PlayerCinematicHandler _CineMan;
        CharacterController _CharCont;

        public CheckEnteredCutScene(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _CineMan = transform.GetComponent<PlayerCinematicHandler>();
            _CharCont = transform.GetComponent<CharacterController>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_CineMan._InCutScene == true)
            {
                
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}