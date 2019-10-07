using System.Collections;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    [SerializeField] float timeToDeath = 1f;


    void Start()
    {
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(CountToDeath());
    }

    IEnumerator CountToDeath()
    {
        yield return new WaitForSeconds(timeToDeath);
        Destroy(gameObject);
    }
}
