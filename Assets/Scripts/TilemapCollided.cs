using UnityEngine;

public class TilemapCollided : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pog"))
        {
            Destroy(other.gameObject);
        }
    }
}
