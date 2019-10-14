using System.Collections;
using UnityEngine;


public class Hejter : MonoBehaviour
{
    public int damageForThePlayer = 1;
    public int health = 8;
    [SerializeField] GameObject leftGun, rightGun, projectile;
    [SerializeField] Transform waypoint1, waypoint2;
    [SerializeField] float minAttackDistance = 5f;
    [SerializeField] float secondsBeforeNextShoot = 1;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] GameObject healthBar1, healthBar2, healthBar3, healthBar4, healthBar5, healthBar6, healthBar7, healthBar8;
    [SerializeField] GameObject ufSound;
    GameObject target;
    Vector3 currentTarget;
    Vector2 playerPos;
    Vector2 myPos;
    Vector2 jxMovePos;
    public static bool isRight;
    bool canShoot = true;
    bool canMove = true;
    float distance;
    int damageForHejter;
    PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        transform.position = waypoint1.transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pog"))
        {
            GetComponent<Animator>().SetTrigger("Damage");
            health -= damageForHejter;
        }

        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().DamagePlayer(damageForThePlayer);
            other.GetComponent<Animator>().SetTrigger("Damage");
        }
    }

    void Update()
    {
        #region HealthBar
        if (health == 7)
        {
            healthBar8.SetActive(false);

        }
        else if (health == 6)
        {
            healthBar8.SetActive(false);
            healthBar7.SetActive(false);

        }
        else if (health == 5)
        {
            healthBar8.SetActive(false);
            healthBar7.SetActive(false);
            healthBar6.SetActive(false);

        }
        else if (health == 4)
        {
            healthBar7.SetActive(false);
            healthBar6.SetActive(false);
            healthBar5.SetActive(false);

        }
        else if (health == 3)
        {
            healthBar8.SetActive(false);
            healthBar7.SetActive(false);
            healthBar6.SetActive(false);
            healthBar5.SetActive(false);
            healthBar4.SetActive(false);

        }
        else if (health == 2)
        {
            healthBar8.SetActive(false);
            healthBar7.SetActive(false);
            healthBar6.SetActive(false);
            healthBar5.SetActive(false);
            healthBar4.SetActive(false);
            healthBar3.SetActive(false);

        }
        else if (health == 1)
        {
            healthBar8.SetActive(false);
            healthBar7.SetActive(false);
            healthBar6.SetActive(false);
            healthBar5.SetActive(false);
            healthBar4.SetActive(false);
            healthBar3.SetActive(false);
            healthBar2.SetActive(false);

        }
        else if (health == 0)
        {
            healthBar8.SetActive(false);
            healthBar7.SetActive(false);
            healthBar6.SetActive(false);
            healthBar5.SetActive(false);
            healthBar4.SetActive(false);
            healthBar3.SetActive(false);
            healthBar2.SetActive(false);
            healthBar1.SetActive(false);

        }
        #endregion
        #region Shoot Movement
        playerPos = FindObjectOfType<PlayerController>().GetComponent<Transform>().position;
        myPos = transform.position;
        var relativePoint = playerPos - myPos;
        if (relativePoint.x < 0)
            isRight = false;
        else if (relativePoint.x > 0)
            isRight = true;
        #endregion
        #region Movement
        if (transform.position == waypoint1.position)
        {
            currentTarget = waypoint2.position;
        }
        else if (transform.position == waypoint2.position)
        {
            currentTarget = waypoint1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, maxSpeed * Time.deltaTime);
        #endregion
        #region Shoot
        jxMovePos = target.transform.position;
        if (target != null && canShoot)
        {
            StartCoroutine(CheckDistance());
        }
        #endregion
        #region Death
        if (health <= 0)
        {
            PlayerController.killCount++;
            ParticleSystem deathParticleGO = Instantiate(deathParticle, transform.position, Quaternion.identity) as ParticleSystem;
            Destroy(gameObject);
        }
        #endregion
    }

    private void LateUpdate()
    {
        damageForHejter = player.hejterDamage;
    }

    IEnumerator CheckDistance()
    {
        distance = Vector2.Distance(this.transform.position, target.transform.position);
        if (distance < minAttackDistance)
        {
            if (canShoot)
            {
                if (!isRight)
                {
                    GameObject jxLeft = Instantiate(projectile, leftGun.transform.position, transform.rotation) as GameObject;
                }
                else
                {
                    GameObject jxRight = Instantiate(projectile, rightGun.transform.position, transform.rotation) as GameObject;
                }
                if (canMove)
                {
                    canMove = false;
                }
                canShoot = false;
                yield return new WaitForSeconds(secondsBeforeNextShoot);
                canShoot = true;
            }
        }
    }
}