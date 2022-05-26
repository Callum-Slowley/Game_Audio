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
    public FMOD.Studio.EventInstance Alert;

    // Fmod Stuff
    FMOD.Studio.EventInstance ArcherFireSound;
    public GameObject FMODObject;
    public float BowState = 0;
    private FMOD.Studio.PLAYBACK_STATE AlertPlaybackState;

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
        AlertPlaybackState = PlaybackState(Alert);
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

        if (AlertPlaybackState == FMOD.Studio.PLAYBACK_STATE.PLAYING || AlertPlaybackState == FMOD.Studio.PLAYBACK_STATE.STARTING)
        {

             Alert.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(new Vector3(player.position.x,transform.position.y,player.position.z));
        animator.SetBool("IsWalking", true);
        
        if (AlertPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            Alert = FMODUnity.RuntimeManager.CreateInstance("event:/Villager_Alert/AlertHorn");
            Alert.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(firingPoint.gameObject));
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("CloseToVillage", 0);
            Alert.start();
            Alert.release();
        }
    }

    FMOD.Studio.PLAYBACK_STATE PlaybackState(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE pS;
        instance.getPlaybackState(out pS);
        return pS;
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

    // I added this jank please forgive me (Signed Kris)
    private void ShootsFired(int BowState)
    {
        ArcherFireSound = FMODUnity.RuntimeManager.CreateInstance("event:/ArcherDialogue/Bow");
        ArcherFireSound.setParameterByName("BowState", BowState);
        // Needs Replaced with 3d emmitter on Model
        ArcherFireSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(FMODObject.gameObject));
        ArcherFireSound.start();
        ArcherFireSound.release();

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
