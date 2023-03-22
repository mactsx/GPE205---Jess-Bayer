using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrictAI : AIController
{
    private float speed;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        speed = pawn.moveSpeed;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        MakeDecisions();
    }


    public override void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Idle:
                DoIdleState();
                // Check for transitions
                // If no target is selected, pick a target
                if (!IsHasTarget())
                {
                    ChooseTarget();
                }
                // After 2 seconds pass, switch to the scan state
                Invoke("DoScanState", 2);
                break;
            case AIState.Scan:
                DoScanState();
                // Check for transitions
                // If they can see the target, attack the target
                if (CanSee(target))
                {
                    ChangeState(AIState.Attack);
                }
                // If the target is more than 10 units away, return to patrol
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Patrol);
                }
                break;
            case AIState.Patrol:
                DoPatrolState();
                // Check for transitions
                // If the ai is patrolling and can hear, scan the area
                if (CanHear())
                {
                    ChangeState(AIState.Scan);
                }                
                break;
            case AIState.Attack:
                DoAttackState();
                // Check for transitions
                // If the player can no longer be seen, scan the area
                if (!CanSee(target))
                {
                    ChangeState(AIState.Scan);
                }
                // If the target is more than 12 units away, enter the idle state
                if (!IsDistanceLessThan(target, 12))
                {
                    ChangeState(AIState.Idle);
                }
                break;


        }
    }
}