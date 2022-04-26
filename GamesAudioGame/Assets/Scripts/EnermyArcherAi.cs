using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnermyArcherAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    public LayerMask isGround, isPlayer;
    public GameObject firingPoint;
    public GameObject projectile;
    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //Attacking
    public float timeBetweenAttacks;
    bool attacked;
    //states
    public float sightRange, attackRange;
    public bool playerInSightRange,playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //Checsk if player is in range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //check if the point is on the map
        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        else
        {
            animator.SetBool("IsWalking", true);
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //if we reach the walk point
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            animator.SetBool("IsWalking", false);
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(new Vector3(player.position.x,transform.position.y,player.position.z));
        animator.SetBool("IsWalking", true);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("IsWalking", false);
        //looks at the player as if it was on the ground
        transform.LookAt((new Vector3(player.position.x, transform.position.y, player.position.z)));
        //however the firing point still looks at the player to shoot at it
        firingPoint.transform.LookAt(player);

        if (!attacked)
        {
            animator.SetBool("IsAttacking", true);
        }
    }
    private void ResetAttack()
    {
        attacked = false;
        animator.SetBool("IsAttacking", false);
    }

    public void attack()
    {
        Rigidbody rb = Instantiate(projectile, firingPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(firingPoint.transform.forward * 32f, ForceMode.Impulse);
        rb.transform.LookAt(player);
        attacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }
    //used to visualise stuff
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
