using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyAI : AIController
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
                Invoke("DoPatrolState", 3.5f);
                break;
            case AIState.Seek:
                DoSeekState();
                if (IsDistanceLessThan(target, 8))
                {                    
                    ChangeState(AIState.Attack);
                }
                if (!CanSee(target))
                {                    
                    ChangeState(AIState.Idle);
                }
                break;
            case AIState.Patrol:
                DoPatrolState();                
                // Check for transitions
                if (CanSee(target))
                {
                    ChangeState(AIState.Seek);
                }
                Invoke("DoScanState", 5);
                break;
            case AIState.Attack:
                DoAttackState();
                if (!CanSee(target))
                {                    
                    ChangeState(AIState.Scan);
                }
                Invoke("DoScanState", 3);
                break;
            

        }
    }
}