using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject gun, pogChamp, virtualCamera;
    [SerializeField] GameObject deathCanvas, healthCanvas;
    [SerializeField] Animator[] powerUpButtons;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI coinText;
    int currentSceneRunning;
    Vector2 deathPos = new Vector2(4, -10);
    float dirX;
    public static bool isEnabledCanvasOfDeath = false;
    public bool faceRight = true;
    bool hasWaited = true;
    Vector3 localScale;
    [SerializeField] Vector3 defaultCameraPosition;
    bool canOpenDoor = false;
    public static bool canOpen = false;
    public static bool canOpenDoorBool = false;
    public bool canPowerUp = true;
    public static int killCount;
    public static int coinCount = 0;
    public static int howManyPowerUps = 0;
    bool canMove = true;
    bool oneTime;

    #region Variables For Power Ups
    public float timeBetweenShots = 0.5f;
    public int health = 2;
    public float jumpSpeed = 400;
    public float moveSpeed;
    public int hejterDamage = 1;
    #endregion

    #endregion
    #region Cached Referance
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    #endregion

    void Start()
    {
        currentSceneRunning = SceneManager.GetActiveScene().buildIndex;
        coinCount = 0;
        canPowerUp = true;
        healthCanvas.SetActive(false);
        healthCanvas.SetActive(true);
        isEnabledCanvasOfDeath = false;
        oneTime = true;
        moveSpeed = 4;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        localScale = transform.localScale;
        switch (currentSceneRunning)
        {
            case (5):
                canMove = false;
                break;
        }

        try
        {
            deathCanvas.SetActive(false);
        }
        catch
        {
            return;
        }
    }

    void Update()
    {
        #region ButtonsManager
        if (canPowerUp)
        {
            foreach (Animator button in powerUpButtons)
            {
                try
                {
                    button.SetBool("anim", true);
                }
                catch
                {
                    return;
                }
            }
        }
        #endregion
        #region UITextUpdate
        healthText.text = health.ToString();
        coinText.text = coinCount.ToString();
        #endregion
        #region Door Technique
        if (canOpenDoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                canOpenDoorBool = true;
            }
        }
        #endregion
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
        #region CoinCheckForPowerUp
        if (coinCount >= 5)
        {
            if (canPowerUp)
            {
                howManyPowerUps++;
            }
            else
            {
                canPowerUp = true;
            }
            coinCount = 0;
        }
        #endregion
    }

    #region Shoot
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
    #endregion

    #region DoorsOpening
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
    #endregion

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
        healthCanvas.SetActive(false);
        transform.position = deathPos;
        Camera.main.transform.position = defaultCameraPosition;
        deathCanvas.SetActive(true);
        isEnabledCanvasOfDeath = true;
        try
        {
            virtualCamera.SetActive(false);
        }
        catch
        {

        }
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
    }


}