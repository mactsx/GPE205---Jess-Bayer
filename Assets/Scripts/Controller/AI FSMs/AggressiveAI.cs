using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveAI : AIController
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
                // If the target can be heard, look around for them
                if (CanHear())
                {
                    ChangeState(AIState.Scan);
                }
                break;
            case AIState.Scan:
                DoScanState();
                // Check for transitions
                // If they can see the target, seek them out
                if (CanSee(target))
                {
                    ChangeState(AIState.Seek);
                }
                // Otherwise if an amount of time has passed, return to idle
                if (HasTimePassed(4))
                {
                    ChangeState(AIState.Idle);
                }
                break;
            case AIState.Seek:
                DoSeekState();
                // If the target is greater than 15 units away, switch to idle
                if (!IsDistanceLessThan(target, 15))
                {
                    ChangeState(AIState.Scan);
                }
                // If the distance is less than 10 units, enter attcking state
                if (IsDistanceLessThan(target, 10))
                {
                    pawn.moveSpeed *= 0.8f;
                    ChangeState(AIState.Attack);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Seek);
                    pawn.moveSpeed = speed;
                }
                break;  

        }
    }
    

}
