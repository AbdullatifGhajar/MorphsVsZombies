using UnityEngine;

public class Turret : MonoBehaviour
{
    public bool isUpgraded = false;
    [HideInInspector]
    private Enemy target;

    public TurretBlueprint blueprint;
    public int cost { get { return blueprint.cost; } }
    public int upgradeCost { get { return blueprint.upgradeCost; } }

    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public AudioClip shootingClip;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public float damageOverTime = 30f;
    public float slowAmount = .5f;

    public AudioClip laserClip;
    private LineRenderer lineRenderer;
    public ParticleSystem impactEffect;

    [Header("Unity Setup Fields")]
    public float turnSpeed = 10f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        if (useLaser)
        {
            GetComponent<AudioSource>().loop = true;
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    public void Upgrade()
    {
        transform.Find("Hat").gameObject.SetActive(true);
        
        fireRate *= 1.5f;
        range *= 1.5f;
        damageOverTime *= 1.5f;

        isUpgraded = true;
        Debug.Log("Turret upgraded!");
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();

                    GetComponent<AudioSource>().Stop();
                }
            }

            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        target.TakeDamage(damageOverTime * Time.deltaTime);
        target.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();

            GetComponent<AudioSource>().clip = laserClip;
            GetComponent<AudioSource>().Play();
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 direction = target.position - transform.position;
        impactEffect.transform.position = target.position - direction.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(-direction);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        bullet.Seek(target);

        GetComponent<AudioSource>().PlayOneShot(shootingClip);
    }

    void OnMouseDown()
    {
        BuildManager.instance.SelectTurret(this);
    }

    public void getSelected()
    {
        GetComponent<Outline>().enabled = true;
    }

    public void getDeselected()
    {
        GetComponent<Outline>().enabled = false;
    }

    public int GetSellAmount()
    {
        if (isUpgraded)
            return (cost + upgradeCost) / 2;

        return cost / 2;
    }
}
