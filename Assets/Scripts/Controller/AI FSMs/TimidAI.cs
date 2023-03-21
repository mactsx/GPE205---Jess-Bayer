using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimidAI : AIController
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
                    Debug.Log("Idle to Scan");
                }
                break;
            case AIState.Scan:
                DoScanState();
                // Check for transitions
                // If they can see the target, seek them out
                if (CanSee(target))
                {
                    Debug.Log("Scan to Seek");
                    ChangeState(AIState.Seek);
                    pawn.moveSpeed = 0;
                }
                // Otherwise if an amount of time has passed, return to idle
                Invoke("DoBackToPostState", 3);
                break;
            case AIState.Seek:
                DoSeekState();
                if (IsDistanceLessThan(target, 5))
                {
                    Debug.Log("Seek to Attack");
                    ChangeState(AIState.Attack);
                    pawn.moveSpeed = speed;
                    pawn.moveSpeed *= 0.6f;
                }
                if(!CanSee(target))
                {
                    Debug.Log("Return to idle - from seek/ been 2 sec");
                    ChangeState(AIState.Idle);
                }
                break;
            case AIState.BackToPost:
                DoBackToPostState();
                // Check for transitions
                if (IsDistanceLessThan(target, 6))
                {
                    Debug.Log("Too close to ai, from post switch to attack");
                    ChangeState(AIState.Attack);
                    pawn.moveSpeed *= 0.6f;
                }
                if (IsDistanceLessThan(postObj, 2))
                {
                    Debug.Log("Return to Idle");
                    ChangeState(AIState.Idle);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                if (!CanSee(target))
                {
                    Debug.Log("Attack to scan if losing sights");
                    ChangeState(AIState.Scan);
                    pawn.moveSpeed = speed;
                }
                Invoke("DoFleeState", 4);
                break;
            case AIState.Flee:
                DoFleeState();
                Invoke("DoBackToPostState", 1.5f);
                Debug.Log("Fleeing then back to the post after time - " + currentState);
                
                break;

        }
    }
}


