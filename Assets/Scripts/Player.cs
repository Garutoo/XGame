using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed = 400;
    [SerializeField] GameObject gun, pogChamp;
    float dirX;
    public bool faceRight = true;
    Vector3 localScale;
    public int health = 10;
    #endregion

    #region Cached Referance
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    #endregion

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        localScale = transform.localScale;
        moveSpeed = 4f;
    }

    public void Damage(int damage)
    {
        health -= damage;
    }

    void Update()
    {
        Debug.Log(faceRight);
        #region Movement
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
        #endregion
        #region Fire
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(pogChamp, gun.transform.position, transform.rotation);

            switch (faceRight)
            {
                case true:
                    pogChamp.transform.localScale = new Vector3(1, 1, 1);
                    break;
                case false:
                    pogChamp.transform.localScale = new Vector3(-1, 1, 1);
                    break;
            }
        }
        #endregion
    }
    void FixedUpdate()
    {
        myRigidbody2D.velocity = new Vector2(dirX, myRigidbody2D.velocity.y);
    }

    void LateUpdate()
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
