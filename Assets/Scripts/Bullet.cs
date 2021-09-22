using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy target;

    public float speed = 70f;
    public int damage = 50;

    public float explosionRadius = 0f;

    public GameObject impactEffect;


    public void Seek(Enemy _target)
    {
        target = _target;
    }

    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target.position);

    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
            Explode();
        else
            Damage(target);

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            print(collider.name);
            if (collider.tag == "Enemy" ||
                (collider.transform.parent != null && collider.transform.parent.tag == "Enemy"))
                Damage(collider.GetComponent<Enemy>());
        }
    }

    void Damage(Enemy enemy)
    {
        if (enemy != null)
            enemy.TakeDamage(damage);
    }
}
