using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class BT_Tree : MonoBehaviour
    {
        public static float attackCooldown;

        private Node _root = null;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void FixedUpdate()
        {
            if (_root != null)
            {
                _root.FixedEvaluate();

            }
               
        }


        private void Update()
        {
            attackCooldown -= UnityEngine.Time.deltaTime;

            if (_root != null)
                _root.LogicEvaluate();

        }

        protected abstract Node SetupTree();

    }

}