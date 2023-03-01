using System.Collections.Generic;
using BehaviorTree;

public class PlayerBT : BT_Tree
{

    public static float speed = 2f;

    public bool _InCombat = false;

    public string[] _AnimationNames;

    public UnityEngine.Transform _Camera;

    public static int combostep;


    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {

            new Sequence(new List<Node>
            {
                new CheckInCombat(transform, _InCombat),
                new TaskEnteringCombat(transform),

                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {  
                        
                        new CheckAttackPressed(transform),
                        new Selector(new List<Node>
                        {
                            
                            new Sequence(new List<Node>
                            {
                                new CheckComboAttack(transform, _AnimationNames),
                                new TaskSecondLightAttack(transform, _AnimationNames),
                            }),

                            new TaskLightAttack(transform, _AnimationNames),

                        }),


                    }),


                    new Sequence(new List<Node>
                    {
                        new CheckMovementPressed(transform),
                        new TaskMovement(transform, _Camera),

                    }),
                    new TaskCombatIdle(transform,_Camera),

                }),


              
            }),

            new Sequence(new List<Node>
            {

                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckSprintPressed(transform),
                        new TaskSprinting(transform, _Camera),

                    }),
                    new Sequence(new List<Node>
                    {
                        new CheckMovementPressed(transform),
                        new TaskMovement(transform, _Camera),

                    }),

                }),

            }),
            new TaskIdle(transform),

        });

        return root;
    }


}