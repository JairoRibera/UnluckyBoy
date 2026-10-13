using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class EnemyMove : MonoBehaviour, IsPusheable
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
    public float enemyShootRange;
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
        agent.speed = speed;
        player = GameObject.FindWithTag("Player");
        //points = GameObject.FindGameObjectsWithTag("RandomPoint");
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
            agent.stoppingDistance = enemyShootRange;
            float distancetoShoot = Vector3.Distance(playerPosition, transform.position);
            agent.SetDestination(playerPosition);
            transform.LookAt(playerPosition);
            if(distancetoShoot <= enemyShootRange)
            {
                agent.isStopped = true;
                enemyShoot.Shoot();
            }
            else
            {
                agent.isStopped = false;
                agent.stoppingDistance = enemyShootRange;
                agent.SetDestination(playerPosition);
            }
        }
        else
        {
            agent.isStopped = false;
            //Si esta lo suficientemente cerca de su destino, busco uno nuevo
            //Le añadimos un poco a su stoppingDistance para que funcione
            //Independientemente de  lo lejos o cerca que se pare
            //if (agent.remainingDistance <= 0.1f + agent.stoppingDistance)
            //{
            //    FindRandomPoint();
            //}
            // 2. Si venía de perseguir al jugador (o se quedó sin ruta), forzamos punto nuevo
            if (!agent.hasPath || agent.remainingDistance <= 0.1f + agent.stoppingDistance)
            {
                agent.stoppingDistance = 1;
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyShootRange);
    }
    public void Knockback(Vector3 dir, float distance)
    {
        StartCoroutine(KnockbackCO(dir, distance));
    }
    public IEnumerator KnockbackCO(Vector3 sourcePosition, float force)
    {
        agent.isStopped = true;
        agent.updatePosition = false;

        // Calcular dirección contraria a la fuente del daño
        Vector3 direction = (transform.position - sourcePosition).normalized;
        direction = Vector3.Scale(direction, new Vector3(1, 0, 1));

        float timer = 0f;
        while (timer < 0.25)
        {
            transform.position += direction * force * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.25f / 2);

        // Reactivar el agente
        agent.Warp(transform.position);
        agent.updatePosition = true;
        agent.isStopped = false;
    }
}
