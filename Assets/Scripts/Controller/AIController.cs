using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Guard, Chase, Seek, Flee, Patrol, Attack, Scan, BackToPost, ChooseTarget };
    public AIState currentState;
    public float lastStateChangeTime;
    public GameObject target;
    public float fleeDistance;
    public Transform[] waypoints;
    public float waypointStopDistance;
    private int currentWaypoint = 0;
    public bool isLooping;
    public float hearingDistance;
    public float fieldOfView;
    private float timer;
    public GameObject postObj;

    // Start is called before the first frame update
    public override void Start()
    {
        currentState = AIState.Idle;
        timer = Time.time;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        MakeDecisions();
    }

    // Target a player
    public void TargetPlayerOne()
    {
        // If there is a game manager
        if (GameManager.instance != null)
        {
            // And there is an array of players
            if (GameManager.instance.players.Count > 0)
            {
                // Then target the gameObject of the pawn of the first player controller in the list
                target = GameManager.instance.players[0].pawn.gameObject;
            }
        }
    }

    // Check is a target is already set
    protected bool IsHasTarget()
    {
        // Returns true if there is a target, or false if there is none
        return (target != null);
    }

    public virtual void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Idle:
                DoIdleState();
                // Check for transitions
                if (!IsHasTarget())
                {
                    ChooseTarget();
                }
                if (IsDistanceLessThan(target, 15))
                {
                    ChangeState(AIState.Seek);
                }
                if (CanSee(target))
                {
                    ChangeState(AIState.Seek);
                }
                if (CanHear())
                {
                    ChangeState(AIState.Patrol);
                }
                break;
            case AIState.Seek:
                DoSeekState();
                // Check for transitions
                if (!IsDistanceLessThan(target, 15))
                {
                    ChangeState(AIState.Idle);
                }
                if (IsDistanceLessThan(target, 8))
                {
                    ChangeState(AIState.Attack);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                // Check for transitions
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Seek);
                }
                if (IsDistanceLessThan(target, 3))
                {
                    ChangeState(AIState.Flee);
                }
                break;
            case AIState.Flee:
                DoFleeState();
                // Check for transitions
                if (!IsDistanceLessThan(target, 20))
                {
                    ChangeState(AIState.Idle);
                }
                break;
            case AIState.Patrol:
                DoPatrolState();
                // Check for transitions
                if (IsDistanceLessThan(target, 15))
                {
                    ChangeState(AIState.Seek);
                }
                if (CanSee(target))
                {
                    ChangeState(AIState.Attack);
                }
                break;
            case AIState.Scan:
                DoScanState();
                break;

        }
        
    }

    public virtual void ChangeState(AIState newState)
    {
        // Change current state
        currentState = newState;

        // Save time since last state change
        lastStateChangeTime = Time.time;
    }

    // CREATE STATES
    #region States
    // Idle state - do nothing
    protected virtual void DoIdleState()
    {
        // Idle does nothing
        ChangeState(AIState.Idle);
    }

    // Move towards the target
    protected virtual void DoSeekState()
    {
        Seek(target.transform);
    }

    // Attack the target
    protected virtual void DoAttackState()
    {
        ChangeState(AIState.Attack);
        // Chase after the player
        Seek(target.transform);
        // Shoot
        Shoot();
    }

    // Flee from the target
    protected virtual void DoFleeState()
    {
        ChangeState(AIState.Flee);
        Flee();
    }

    // Patrol from one point to another
    protected virtual void DoPatrolState()
    {
        ChangeState(AIState.Patrol);
        Patrol();
    }

    // Choose a target
    protected virtual void DoChooseTargetState()
    {
        ChooseTarget();
    }

    // Scan the area for the target
    protected virtual void DoScanState()
    {
        ChangeState(AIState.Scan);
        Scan();
    }

    // Seek out a post object
    protected virtual void DoBackToPostState()
    {
        ChangeState(AIState.BackToPost);
        Seek(postObj.transform);
    }

    #endregion States


    // CREATE BEHAVIORS
    #region Behaviors
    // Seek out the target via vector3
    public void Seek(Vector3 targetVector)
    {
        pawn.RotateTowards(targetVector);
        pawn.MoveForward();
    }

    // Seek out the target via transform
    public void Seek(Transform targetTransform)
    {
        Seek(targetTransform.position);
    }

    // Seek out the target via pawn
    public void Seek(Pawn targetPawn)
    {
        Seek(targetPawn.transform);
    }

    // Tell tank to shoot
    public void Shoot()
    {
        pawn.Shoot();
    }

    // Flee from the target
    public void Flee()
    {
        // Vector to the target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        // Find the inverse - vector away from target
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Create two values for the distance between the target and itself and this as a percentage
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        float percentOfFleeDistance = targetDistance / fleeDistance;
        // Clamp this percent and invert
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        percentOfFleeDistance = 1 - percentOfFleeDistance;
        // Find the vector to travel away from target
        Vector3 fleeVector = vectorAwayFromTarget.normalized * (fleeDistance * (1 - percentOfFleeDistance));
        // Move towards this position
        Seek(pawn.transform.position + fleeVector);
    }

    // Seek out a waypoint to patrol to
    public void Patrol()
    {
        // If there are enough waypoints in the list to move
        if (waypoints.Length > currentWaypoint)
        {
            // Seek the waypoint
            Seek(waypoints[currentWaypoint]);
            // If tank is close enough, go to next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) <= waypointStopDistance)
            {
                currentWaypoint++;
            }
            if (isLooping == true && currentWaypoint >= waypoints.Length)
            {
                RestartPatrol();
            }
            
        }

    }

    // Restart the Patrol
    public void RestartPatrol()
    {
        // Set the current waypoint to 0
        currentWaypoint = 0;
    }

    // Choose a target
    public void ChooseTarget()
    {
        if (!IsHasTarget())
        {
            TargetPlayerOne();
        }
    }

    // Target the nearest tank
    public void TargetNearestTank()
    {
        // Get a list of all tank pawns
        Pawn[] allTanks = FindObjectsOfType<Pawn>();

        // Set the closest tank initially to the first in the list
        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        // Iterate through the list
        foreach (Pawn tank in allTanks)
        {
            // If this tank is closer than the one currently set
            if (Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                // Set this tank as the closest one
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
            }
        }

        // Target the closest tank
        target = closestTank.gameObject;
    }

    // Check if the tank can hear the target
    public bool CanHear()
    {
        // Get the target's noisemaker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();

        // If there is no noise maker, they cannot hear
        if (noiseMaker == null)
        {
            return false;
        }

        // If they are making no noise, they cannot hear
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        // If they are making noise, add the noisemaker noise distance to this tank's hearing distance
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        // If the total distance between the two objects is closer than the noises' distance
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            // Then the target can be heard
            return true;
        }
        // Otherwise, they are too far and this tank cannot hear
        else
        {
            return false;
        }

    }

    // If the tank can see
    public bool CanSee(GameObject target)
    {
        // Find the vector from this ai to the target
        Vector3 agentToTargetVector = target.transform.position - transform.position;
        // Find the angle between the direction the ai is facing and the vector to the target
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);

        // Attempt to use raycasting
        RaycastHit2D ray = Physics2D.Raycast(pawn.transform.position, agentToTargetVector);

        // If the target is within the AI's FOV
        if (angleToTarget < fieldOfView)
        {                                    
            // Target can be seen
            return true;       
        }
        // Otherwise they cannot be seen
        else
        {            
            return false;
        }     
        
    }
    
    // Scan around to look for the target
    public void Scan()
    {
        pawn.RotateClockwise();        
    }

    #endregion Behaviors


    // CREATE TRANSITIONS
    #region Transitions
    // Check distance transition
    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance (pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    // Has a certain amount of time passed transition
    protected bool HasTimePassed(float timeToPass)
    {        
        timer += Time.deltaTime;

        if (timer >= (timeToPass + Time.time))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion Transitions


}
