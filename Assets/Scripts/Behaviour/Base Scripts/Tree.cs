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

        private void Update()
        {

            if (_root != null)
                _root.LogicEvaluate();

        }

        protected abstract Node SetupTree();

    }

}