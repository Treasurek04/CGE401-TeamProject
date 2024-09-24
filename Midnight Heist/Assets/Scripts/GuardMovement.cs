using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardMovement : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for the guard to patrol
    public Transform player; // Player transform
    public float detectionRadius = 5f; // How close the player needs to be for the guard to stop
    public float patrolSpeed = 2f; // Speed at which the guard patrols
    public float stopDistance = 0.1f; // Distance at which guard considers it has reached a waypoint

    private int currentWaypointIndex;
    private bool isPlayerNearby;
    private Vector3 targetWaypoint;
    private Animator animator; 

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component

        if (waypoints.Length > 0)
        {
            currentWaypointIndex = 0;
            targetWaypoint = waypoints[currentWaypointIndex].position;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within the detection radius
        if (distanceToPlayer <= detectionRadius)
        {
            isPlayerNearby = true;
            StopMovement();
        }
        else
        {
            isPlayerNearby = false;
            Patrol(); // Guard patrols when player is not nearby
        }
    }

    void Patrol()
    {
        if (isPlayerNearby || waypoints.Length == 0)
            return;

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, patrolSpeed * Time.deltaTime);

        // Check if the guard reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint) < stopDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            targetWaypoint = waypoints[currentWaypointIndex].position;
        }

        // Play walking animation
        animator.SetBool("isWalking", true);
    }

    void StopMovement()
    {
        // Stop guard movement and play idle animation
        animator.SetBool("isWalking", false);
    }
}
 
 
