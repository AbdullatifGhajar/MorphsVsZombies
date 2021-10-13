using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wayPointIndex;

    private Enemy enemy;

    void Start()
    {
        wayPointIndex = 0;
        enemy = GetComponent<Enemy>();
        target = WayPoints.points[0];
    }

    void Update()
    {
        Vector3 targetDirection = target.position - transform.position;
        transform.Translate(targetDirection.normalized * enemy.speed * Time.deltaTime, Space.World);

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * 7f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            GetNextWaypoint();

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wayPointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wayPointIndex++;
        target = WayPoints.points[wayPointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

}
