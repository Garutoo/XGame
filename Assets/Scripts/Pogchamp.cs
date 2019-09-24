using UnityEngine;

public class Pogchamp : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;
    bool canLeft = true;
    bool canRight = true;
    bool still = false;

    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        Moving();
        Damager();
    }

    private void Damager()
    {
        if (CompareTag("Hejter"))
            Debug.Log("Collided");
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
}
