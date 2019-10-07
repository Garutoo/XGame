using UnityEngine;

public class Pogchamp : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;
    bool canLeft = true;
    bool canRight = true;
    bool still = false;

    PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        Moving();
    }

    void Moving()
    {
        if (player.faceRight && canRight || canRight && still)
        {
            canLeft = false;
            still = true;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (!player.faceRight && canLeft || canLeft && still)
        {
            canRight = false;
            still = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (tag)
        {
            case ("Player"):
                break;
            case ("Enemy"):
                break;
            case ("Tilemap"):
                Destroy(gameObject);
                break;
            default:
                break;

        }
    }
}