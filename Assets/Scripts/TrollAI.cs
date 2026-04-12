using UnityEngine;
using UnityEngine.AI;

public class TrollAI : MonoBehaviour
{
    public enum State{Patrol, Attack};
    public State currentState = State.Patrol;
    public float aggroRange = 5;
    public Transform[] patrolPoints;
    private NavMeshAgent agent;
    private Transform target;
    private int currentPatrolPoint = 0;
    private float patrolBuffer = .1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            target = patrolPoints[currentPatrolPoint];
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.Log("No patrol points set");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);

        switch(currentState) {
            case State.Patrol:
                Patrol();
                break;
            case State.Attack:
                Attack();
                break;
        }

        if (patrolBuffer > 0)
        {
            patrolBuffer -= Time.deltaTime;
        }
    }

    private void Patrol()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, aggroRange);

        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Gnome"))
            {
                target = collider.transform;
                currentState = State.Attack;
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance && patrolBuffer <= 0)
        {
            currentPatrolPoint ++;
            if (currentPatrolPoint >= patrolPoints.Length)
            {
                currentPatrolPoint = 0;
            }
            target = patrolPoints[currentPatrolPoint];
            patrolBuffer = .1f;
        }
    }

    private void Attack()
    {
        bool escaped = true;

        Collider[] colliders = Physics.OverlapSphere(transform.position, aggroRange);

        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Gnome"))
            {
                escaped = false;
            }
        }

        if (escaped)
        {
            currentState = State.Patrol;
            target = patrolPoints[currentPatrolPoint];
            patrolBuffer = .1f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
