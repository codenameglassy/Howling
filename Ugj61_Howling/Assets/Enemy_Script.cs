using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_Script : MonoBehaviour
{
    private AIDestinationSetter aIDestinationSetter;
    private AIPath aiPath;
    Seeker seeker;
    Rigidbody2D rb;
    Path path;
   
   
    [Space]
    [Header("Movement")]
    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;
    public Transform player_Target;
    Transform deer_Target;
    [HideInInspector] public bool canMove = true;
    bool isPlayerNear, isDeerNear;
    private Transform currentTarget;
    [Space]
    [Header("Patroling")]
    [SerializeField] private Transform[] patrolPoints;


    private void Awake()
    {
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SelectRandomPatrolPoint();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    public void UpdatePath()
    {
       
        seeker.StartPath(rb.position, CurrentTarget().position, OnPathComplete);
    } 

    private void FixedUpdate()
    {
        Move();
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
           
        }
    }

    Transform CurrentTarget()
    {
        if (isPlayerNear)
        {
            currentTarget = player_Target;
        }

        if (isDeerNear)
        {
            currentTarget = deer_Target;
        }

        return currentTarget = currentPatrolPoint;

    }

    Transform currentPatrolPoint;
    private Transform SelectRandomPatrolPoint()
    {
        int index = Random.Range(0, patrolPoints.Length);
        currentPatrolPoint = patrolPoints[index];
        return patrolPoints[index];
    }

    private void Move()
    {

        if(path == null)
        {
            return;
        }

        if (!canMove)
        {
            return;
        }


        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            SelectRandomPatrolPoint();
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }
}
