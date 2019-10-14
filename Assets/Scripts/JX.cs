using System.Collections;
using UnityEngine;

public class JX : MonoBehaviour
{
    [SerializeField] float speed = 6f;
    public static bool canMove = true;
    [SerializeField] ParticleSystem boom;
    bool isRight;

    void Start()
    {
        isRight = Hejter.isRight;
        StartCoroutine(TimeToDeath());
    }

    void Update()
    {
        if (isRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else if (!isRight)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DamageColoChanger();
            try
            {
                other.GetComponent<PlayerController>().DamagePlayer(GameObject.FindGameObjectWithTag("Hejter").GetComponent<Hejter>().damageForThePlayer);

            }
            catch
            {
                Destroy(gameObject);
            }
            Debug.Log(other.GetComponent<PlayerController>().health);
            StartCoroutine(DestroyJX());
        }
        else if (other.CompareTag("Hejter"))
        {

        }
        else if (other.CompareTag("Tilemap"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyJX()
    {
        boom.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);

    }
    private void DamageColoChanger()
    {
        FindObjectOfType<PlayerController>().GetComponent<Animator>().SetTrigger("Damage");
    }

    IEnumerator TimeToDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
