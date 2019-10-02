using System.Collections;
using UnityEngine;

public class JX : MonoBehaviour
{
    [SerializeField] float speed = 6f;
    public static bool canMove = true;

    void Start()
    {
        StartCoroutine(TimeToDeath());
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().DamagePlayer(GameObject.FindGameObjectWithTag("Hejter").GetComponent<Hejter>().damageForThePlayer);
            Debug.Log(other.GetComponent<Player>().health);
        }
    }

    IEnumerator TimeToDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
