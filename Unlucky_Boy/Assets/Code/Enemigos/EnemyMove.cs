using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyMove : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed;
    public GameObject[] points;
    public bool isNear;
    public bool isWaiting;
    public float stopTimer = 0f;
    private float timeStopped = 5f;
    private NavMeshAgent agent;
    [Header("Detection")]
    private Collider[] detectedCollider;
    public float enemyRange;
    public LayerMask PlayerLayer;
    private Vector3 playerPosition;
    private bool isDetected;
    public GameObject player;
    private EnemyShoot enemyShoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stopTimer = timeStopped;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        points = GameObject.FindGameObjectsWithTag("RandomPoint");
        enemyShoot =  GetComponent<EnemyShoot>();
        FindRandomPoint();
    }
    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        EnemyCheck();
        MoveEnemy();
    }
    public void MoveEnemy()
    {
        //Solo persigue al jugador si lo ha detectado
        if (isDetected == true)
        {
            float distancetoShoot = Vector3.Distance(playerPosition, transform.position);
            agent.speed = speed;
            agent.SetDestination(playerPosition);
            transform.LookAt(playerPosition);
            Debug.Log("Trae ese klo pa acá");
            if(distancetoShoot <= 6)
            {
                agent.isStopped = true;
                enemyShoot.Shoot();
            }
        }
        else
        {
            //Si esta lo suficientemente cerca de su destino, busco uno nuevo
            //Le añadimos un poco a su stoppingDistance para que funcione
            //Independientemente de  lo lejos o cerca que se pare
            if (agent.remainingDistance <= 0.1f + agent.stoppingDistance)
            {
                agent.isStopped = false;
                FindRandomPoint();
            }
        }
    }
    void EnemyCheck()
    {

        detectedCollider = Physics.OverlapSphere(transform.position, enemyRange, PlayerLayer);
        if (detectedCollider.Length > 0)
        {
            isDetected = true;
            Debug.Log("PLayer detected");
        }
        else
        {
            isDetected = false;
        }

    }
    void FindRandomPoint()
    {
        int _randomIndex = Random.Range(0, points.Length);
        agent.SetDestination(points[_randomIndex].transform.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRange);
    }
}
