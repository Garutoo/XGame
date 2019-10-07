using UnityEngine;

public class Spike : MonoBehaviour
{
    PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.StartCoroutine(player.WaitToLoadDeath());
        }
    }
}
