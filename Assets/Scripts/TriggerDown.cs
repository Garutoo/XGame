using UnityEngine;

public class TriggerDown : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.StartCoroutine(player.WaitToLoadDeath());
        }
    }
}
