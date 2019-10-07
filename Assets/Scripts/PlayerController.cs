using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed = 400;
    [SerializeField] GameObject gun, pogChamp, virtualCamera;
    [SerializeField] GameObject deathCanvas;
    Vector2 deathPos = new Vector2(4, -10);
    Vector3 offset = new Vector3(0, 0, 335);
    float dirX;
    public static bool isEnabledCanvasOfDeath = false;
    public bool faceRight = true;
    bool hasWaited = true;
    Vector3 localScale;
    [SerializeField] Vector3 defaultCameraPosition;
    public int health = 2;
    public float timeBetweenShots = 0.5f;
    bool canOpenDoor = false;
    public static bool canOpen = false;
    public static bool canOpenDoorBool = false;
    bool canMove = true;
    bool oneTime;
    #endregion
    #region Cached Referance
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    #endregion

    void Start()
    {
        isEnabledCanvasOfDeath = false;
        deathCanvas.SetActive(false);
        oneTime = true;
        moveSpeed = 4;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        localScale = transform.localScale;
    }

    void Update()
    {
        if (canOpenDoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                canOpenDoorBool = true;
                canOpenDoor = false;
            }
        }

        #region Death
        if (health <= 0 && oneTime)
        {
            StartCoroutine(WaitToLoadDeath());
        }
        #endregion
        #region Movement
        if (canMove)
        {
            dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

            if (Input.GetButtonDown("Jump") && myRigidbody2D.velocity.y == 0)
                myRigidbody2D.AddForce(Vector2.up * jumpSpeed);
            if (Mathf.Abs(dirX) > 0 && myRigidbody2D.velocity.y == 0)
                myAnimator.SetBool("isWalking", true);
            else
                myAnimator.SetBool("isWalking", false);
            if (myRigidbody2D.velocity.y == 0)
                myAnimator.SetBool("isJumping", false);
            if (myRigidbody2D.velocity.y > 0)
                myAnimator.SetBool("isJumping", true);
            if (myRigidbody2D.velocity.y < 0)
                myAnimator.SetBool("isJumping", false);
        }
        #endregion
        #region Shooting
        Shoot();
        #endregion
    }

    private void Shoot()
    {
        if (hasWaited)
            StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canMove)
        {
            Instantiate(pogChamp, gun.transform.position, transform.rotation);

            switch (faceRight)
            {
                case true:
                    hasWaited = false;
                    pogChamp.transform.localScale = new Vector3(1, 1, 1);
                    yield return new WaitForSeconds(timeBetweenShots);
                    hasWaited = true;
                    break;
                case false:
                    hasWaited = false;
                    pogChamp.transform.localScale = new Vector3(-1, 1, 1);
                    yield return new WaitForSeconds(timeBetweenShots);
                    hasWaited = true;
                    break;
            }

        }
    }

    void FixedUpdate()
    {
        myRigidbody2D.velocity = new Vector2(dirX, myRigidbody2D.velocity.y);
    }
    void LateUpdate()
    {
        if (canMove)
        {
            if (dirX > 0)
                faceRight = true;
            else if (dirX < 0)
                faceRight = false;

            if ((faceRight) && (localScale.x < 0) || ((!faceRight) && (localScale.x > 0)))
                localScale.x *= -1;

            transform.localScale = localScale;
        }
    }

    public IEnumerator WaitToLoadDeath()
    {
        canMove = false;
        yield return new WaitForSeconds(0.5f);
        virtualCamera.SetActive(false);
        transform.position = deathPos;
        Camera.main.transform.position = defaultCameraPosition;
        isEnabledCanvasOfDeath = true;
        deathCanvas.SetActive(true);
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            canOpenDoor = true;
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            canOpen = false;
            canOpenDoor = false;
            canOpenDoorBool = false;
        }
    }

}