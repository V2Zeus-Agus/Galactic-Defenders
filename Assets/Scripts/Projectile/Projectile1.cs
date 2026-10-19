using System;
using System.Collections;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    public GameManager gameManager;
    public float moveSpeed;
    public float damage;
    public float lifespan = 1f;
    private PoolBulletPlayer bulletPool;

    public AudioClip shootSound;
    public AudioClip impactSound;
    private AudioSource audioSource;

    public ProjectileShoot projectileShoot;

    private Rigidbody2D rb;

    void OnEnable()
    {
        if (bulletPool == null)
            bulletPool = FindObjectOfType<PoolBulletPlayer>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.velocity = transform.up * moveSpeed * 2f;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            projectileShoot = player.GetComponent<ProjectileShoot>();

        audioSource = GetComponent<AudioSource>();
        if (shootSound != null && audioSource != null)
            audioSource.PlayOneShot(shootSound);

        StartCoroutine(DeactivateAfterTime(lifespan));
    }

    void Update()
    {
        // Vacío - el Rigidbody maneja el movimiento
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPhasatron"))
        {
            Phasatron enemy = collision.gameObject.GetComponent<Phasatron>();
            Phasatron2 enemy2 = collision.gameObject.GetComponent<Phasatron2>();
            Phasatron3 enemy3 = collision.gameObject.GetComponent<Phasatron3>();
            if (enemy != null)
            {
                float damageAmount = enemy.maxHealth * 0.21f;
                enemy.TakeDamage(damageAmount);
            }
            if (enemy2 != null)
            {
                float damageAmount = enemy2.maxHealth * 0.16f;
                enemy2.TakeDamage(damageAmount);
            }
            if (enemy3 != null)
            {
                float damageAmount = enemy3.maxHealth * 0.13f;
                enemy3.TakeDamage(damageAmount);
            }

            if (projectileShoot != null)
                projectileShoot.IncreaseCharge();

            DeactivateBullet();
            return;
        }

        if (collision.gameObject.CompareTag("EnemyWall"))
        {
            EnemyWall healthPared = collision.gameObject.GetComponent<EnemyWall>();
            EnemyWall2 healthPared2 = collision.gameObject.GetComponent<EnemyWall2>();
            if (healthPared != null)
            {
                float damageAmount = healthPared.maxHealth * 0.40f;
                healthPared.TakeDamage(damageAmount);
            }
            if (healthPared2 != null)
            {
                float damageAmount = healthPared2.maxHealth * 0.35f;
                healthPared2.TakeDamage(damageAmount);
            }

            if (projectileShoot != null)
                projectileShoot.IncreaseCharge();

            DeactivateBullet();
            return;
        }

        if (collision.gameObject.CompareTag("EnemyKamikaze"))
        {
            Kamikaze enemyHealth = collision.GetComponent<Kamikaze>();
            Kamikaze2 enemyHealth2 = collision.GetComponent<Kamikaze2>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(60f);
            if (enemyHealth2 != null)
                enemyHealth2.TakeDamage(55f);

            if (projectileShoot != null)
                projectileShoot.IncreaseCharge();

            DeactivateBullet();
            return;
        }

        if (collision.gameObject.CompareTag("CrazyKamikaze"))
        {
            Kamikaze2 enemyHealth = collision.GetComponent<Kamikaze2>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(100f);

            if (projectileShoot != null)
                projectileShoot.IncreaseCharge();

            DeactivateBullet();
            return;
        }

        if (collision.gameObject.CompareTag("EnemyShip"))
        {
            EnemyFlyThrough enemyHealth = collision.GetComponent<EnemyFlyThrough>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(100f);

            if (projectileShoot != null)
                projectileShoot.IncreaseCharge();

            DeactivateBullet();
            return;
        }

        if (collision.gameObject.CompareTag("1stBoss"))
        {
            BossMovement bossHealth = collision.GetComponent<BossMovement>();
            if (bossHealth != null)
            {
                float damageAmount = bossHealth.maxHealth * 0.04f;
                bossHealth.TakeDamage(damageAmount);
            }

            if (projectileShoot != null)
                projectileShoot.IncreaseCharge();

            DeactivateBullet();
            return;
        }

        if (collision.gameObject.CompareTag("2ndBoss"))
        {
            Boss_2_movement bossHealth = collision.GetComponent<Boss_2_movement>();
            if (bossHealth != null)
            {
                float damageAmount = bossHealth.maxHealth * 0.017f;
                bossHealth.TakeDamage(damageAmount);
            }

            if (projectileShoot != null)
                projectileShoot.IncreaseCharge();

            DeactivateBullet();
            return;
        }

        if (collision.gameObject.CompareTag("3rdBoss"))
        {
            Boss3 bossHealth = collision.GetComponent<Boss3>();
            if (bossHealth != null)
            {
                float damageAmount = bossHealth.maxHealth * 0.008f;
                bossHealth.TakeDamage(damageAmount);
            }

            if (projectileShoot != null)
                projectileShoot.IncreaseCharge();

            DeactivateBullet();
            return;
        }

        if (collision.gameObject.CompareTag("Meteorito"))
        {
            Meteorito meteoritoHealth = collision.GetComponent<Meteorito>();
            if (meteoritoHealth != null)
            {
                float damageAmount = meteoritoHealth.life * 0.34f;
                meteoritoHealth.TakeDamage(damageAmount);
            }
            return;
        }

        if (collision.gameObject.CompareTag("ProyectileBoss2"))
        {
            Misil_boss_2 health = collision.GetComponent<Misil_boss_2>();
            if (health != null)
                health.TakeDamage(1f);

            DeactivateBullet();
            return;
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            DeactivateBullet();
            return;
        }
    }

    private IEnumerator DeactivateAfterTime(float lifespan)
    {
        yield return new WaitForSeconds(lifespan);
        DeactivateBullet();
    }

    void DeactivateBullet()
    {
        if (!gameObject.activeSelf) return; // Evitar llamadas dobles

        rb.velocity = Vector2.zero;
        gameObject.SetActive(false); // Desactivar primero, siempre

        if (bulletPool != null)
            bulletPool.ReturnBullet(gameObject);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}