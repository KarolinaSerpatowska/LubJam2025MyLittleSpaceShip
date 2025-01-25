using UnityEngine;
using System.Collections;

public class FlyingAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private WaypointHolder waypointHolder;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;


    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float rotationSpeed = 7.5f;
    [SerializeField] private float circleDuration = 5f;
    [SerializeField] private float waypointDistanceThreshold = 2f;


    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 35f;
    [SerializeField] private float attackDuration = 3f;
    [SerializeField] private float shootInterval = 0.5f;


    [Header("Projectile Settings")]
    [SerializeField] private float projectileSpeed = 60f;
    [SerializeField] private float angleToShootAtPlayer = 0.1f;


    private Transform currentWaypointTarget;
    private Transform[] waypoints;


    private void Start()
    {
        if (waypointHolder != null)
        {
            waypointHolder.RefreshWaypoints();
            waypoints = waypointHolder.Waypoints;
        }


        if (waypoints == null || waypoints.Length == 0) return;


        StartCoroutine(StateMachine());
    }


    private void FaceTarget(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        if (dir.sqrMagnitude < 0.0001f) return;


        dir.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
    }


    private bool IsFacingPlayer(float angleThreshold)
    {
        if (!player) return true;
        Vector3 toPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, toPlayer);
        return angle <= angleThreshold;
    }


    private IEnumerator RotateUntilFacingPlayer(float angleThreshold)
    {
        while (!IsFacingPlayer(angleThreshold))
        {
            FaceTarget(player.position);
            yield return null;
        }
    }


    private void PickRandomWaypoint()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            currentWaypointTarget = waypoints[Random.Range(0, waypoints.Length)];
        }
    }


    private bool ReachedWaypoint()
    {
        if (!currentWaypointTarget) return false;
        return Vector3.Distance(transform.position, currentWaypointTarget.position) < waypointDistanceThreshold;
    }


    private void MoveTowardsTarget(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        if (dir.sqrMagnitude < 0.0001f) return;


        dir.Normalize();
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(dir),
            Time.deltaTime * rotationSpeed
        );
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }


    private float DistanceToPlayer()
    {
        if (!player) return float.MaxValue;
        return Vector3.Distance(transform.position, player.position);
    }


    private void FireProjectile()
    {
        if (!projectilePrefab) return;
        var spawn = projectileSpawnPoint ? projectileSpawnPoint : transform;


        var proj = Instantiate(projectilePrefab, spawn.position, spawn.rotation);
        var rb = proj.GetComponent<Rigidbody>();


        if (rb)
            rb.linearVelocity = spawn.forward * projectileSpeed;
    }


    private IEnumerator CircleState(float duration)
    {
        float timer = 0f;
        PickRandomWaypoint();


        while (timer < duration)
        {
            timer += Time.deltaTime;
            if (currentWaypointTarget)
                MoveTowardsTarget(currentWaypointTarget.position);


            if (ReachedWaypoint()) PickRandomWaypoint();


            yield return null;
        }
    }


    private IEnumerator AttackState(float duration)
    {
        yield return StartCoroutine(RotateUntilFacingPlayer(angleToShootAtPlayer));
        FireProjectile();


        float timer = 0f;
        float shootTimer = 0f;


        while (timer < duration)
        {
            timer += Time.deltaTime;
            shootTimer += Time.deltaTime;


            FaceTarget(player.position);


            if (DistanceToPlayer() > attackRange)
            {
                MoveTowardsTarget(player.position);
            }


            if (shootTimer >= shootInterval)
            {
                shootTimer = 0f;
                FireProjectile();
            }
            yield return null;
        }
    }


    private IEnumerator StateMachine()
    {
        while (true)
        {
            yield return StartCoroutine(CircleState(circleDuration));
            yield return StartCoroutine(AttackState(attackDuration));
        }
    }

}
