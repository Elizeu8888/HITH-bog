using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMediumBT : BT_Tree
{
    public Transform _CurrentEnemyTransform;
    public GameObject plyRefrence;
    public static GameObject _Player;
    public static float _Dir_Change_Timer;
    public float _PlayerDistance;
    public NavMeshAgent _NavMesh;
    public CharacterController _charControl;

    public static Transform _EnemyRestPos;
    public Transform enemyRestPos;


    void Awake()// cant use start since Tree node uses it
    {
        _EnemyRestPos = enemyRestPos;
        _Player = plyRefrence;
        _NavMesh = gameObject.GetComponent<NavMeshAgent>();
        _charControl = gameObject.GetComponent<CharacterController>();
        _NavMesh.updatePosition = false;
        _NavMesh.updateRotation = false;
    }

    void OnAnimatorMove()// this allows root motion to work
    {

    }

    void FixedUpdate()//use this spparingly
    {
        _Dir_Change_Timer -= Time.deltaTime;
        _PlayerDistance = Vector3.Distance(_Player.transform.position, _CurrentEnemyTransform.position);
    }

    protected override Node SetupTree()// this is the behaviour tree
    {
        Node root = new Selector(new List<Node>
        {

            new Sequence(new List<Node>
            {
                new CheckMoveToPlayer(_CurrentEnemyTransform, _PlayerDistance),
                new TaskMoveToPlayer(_CurrentEnemyTransform,_PlayerDistance, _NavMesh, _charControl),            

            }),
            new Sequence(new List<Node>
            {
                new TaskBackAway(_CurrentEnemyTransform, _NavMesh, _charControl),

            }),
            new TaskEnemyIdle(_CurrentEnemyTransform,_NavMesh),

        });

        return root;
    }


}