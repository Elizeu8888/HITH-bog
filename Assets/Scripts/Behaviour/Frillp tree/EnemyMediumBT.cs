using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMediumBT : BT_Tree
{
    public GameObject plyRefrence;
    public static GameObject _Player;
    public static Transform[] _directions;
    public Transform[] directions;
    public static float _Dir_Change_Timer;

    public static float _PlayerDistance;
    public static NavMeshAgent _NavMesh;
    public static CharacterController _charControl;

    public float distance;


    void Awake()// cant use start since Tree node uses it
    {
        _Player = plyRefrence;
        _directions = directions;
        _NavMesh = gameObject.GetComponent<NavMeshAgent>();
        _charControl = gameObject.GetComponent<CharacterController>();
        _NavMesh.updatePosition = false;
        _NavMesh.updateRotation = false;
    }

    void OnAnimatorMove()// this allows root motion to work
    {

    }

    void FixedUpdate()
    {
        distance = _PlayerDistance;
        _Dir_Change_Timer -= Time.deltaTime;
    }

    protected override Node SetupTree()// this is the behaviour tree
    {
        Node root = new Selector(new List<Node>
        {

            new Sequence(new List<Node>
            {
                new CheckMoveToPlayer(transform),
                new TaskMoveToPlayer(transform),            

            }),
            new Sequence(new List<Node>
            {
                new TaskBackAway(transform),

            }),
            new TaskEnemyIdle(transform),

        });

        return root;
    }


}