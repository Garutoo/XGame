using System.Collections;
using UnityEngine;


public class Hejter : MonoBehaviour
{
    public int damageForThePlayer = 1;
    public int health = 10;
    [SerializeField] GameObject gun, projectile;
    [SerializeField] Transform waypoint1, waypoint2;
    [SerializeField] float minAttackDistance = 5f;
    [SerializeField] float secondsBeforeNextShoot = 1;
    [SerializeField] float maxSpeed = 5;
    GameObject target;
    Vector3 currentTarget;
    Vector2 jxMovePos;
    bool canShoot = true;
    bool canMove = true;
    float distance;
    void Start()
    {
        transform.position = waypoint1.transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pog"))
        {
            health -= other.GetComponent<Pogchamp>().damageHater;
        }

        else if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().DamagePlayer(damageForThePlayer);
        }
    }

    void Update()
    {
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
            Destroy(gameObject);
        }
        #endregion
    }

    IEnumerator CheckDistance()
    {
        distance = Vector2.Distance(this.transform.position, target.transform.position);
        if (distance < minAttackDistance)
        {
            if (canShoot)
            {
                GameObject jx = Instantiate(projectile, gun.transform.position, transform.rotation) as GameObject;
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