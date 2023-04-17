using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class CheckMoveToCutScene : Node
    {
        private Transform _transform;
        private Animator _Anim;
        PlayerCinematicHandler _CineMan;

        public CheckMoveToCutScene(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _CineMan = transform.GetComponent<PlayerCinematicHandler>();
        }

        public override NodeState LogicEvaluate()
        {
            if(_CineMan._CinematicOBJ != null)
            {
                if (_CineMan._CinematicOBJ._WaitForPlayerInput == true)
                {
                    if (_CineMan._EnteredCutScene == true && _CineMan.CanStartCutScene() == false && _CineMan._InputPressed == true)
                    {
                        state = NodeState.SUCCESS;
                        return state;
                    }
                }
                else
                {
                    if (_CineMan._EnteredCutScene == true && _CineMan.CanStartCutScene() == false)
                    {
                        state = NodeState.SUCCESS;
                        return state;
                    }
                }
            }


            state = NodeState.FAILURE;
            return state;
            

        }


    }
}
