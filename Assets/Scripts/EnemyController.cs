using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using static EnemyController;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] patrolPoints;
    public int currentindex;
    public EnumStates currentstate;
    private float currentTime;
    public float maxtimetowait = 2f;
    public Transform personaje;
    public int Enemy_Damage = 20;

    public enum EnumStates
    {
        patrol,
        wait,
        chase,
        attak,
        DEAD
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void GotoNextpoint()
    {
        currentindex++;

        if (currentindex >= patrolPoints.Length)
        {
            currentindex = 0;
        }
        agent.SetDestination(patrolPoints[currentindex].position);
    }

    void Start()
    {
        gameObject.SetActive(true);
        GotoNextpoint();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (currentstate)
        {
            case EnumStates.patrol:
                if (agent.remainingDistance < 1)
                {
                    currentTime = 0;
                    currentstate = EnumStates.wait;
                }
                break;
            case EnumStates.wait:
                currentTime += Time.deltaTime;
                if (currentTime > maxtimetowait)
                {
                    GotoNextpoint();

                    currentstate = EnumStates.patrol;
                }
                break;
            case EnumStates.chase:
                agent.SetDestination(personaje.position);
                if (agent.remainingDistance < 0.5)
                {
                    currentTime = 0;
                    currentstate = EnumStates.attak;
                }
                break;
            case EnumStates.attak:
                if (agent.remainingDistance > 0.5)

                {
                    currentTime = 0;
                    currentstate = EnumStates.chase;
                }
                break;
        }
    }
}


