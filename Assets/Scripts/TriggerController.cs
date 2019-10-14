using System.Collections;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public static bool canNarrateTrigger = false;
    bool oneTime = true;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (oneTime && other.CompareTag("Player"))
        {
            StartCoroutine(TriggerMechanic());
        }
    }

    IEnumerator TriggerMechanic()
    {
        oneTime = false;
        canNarrateTrigger = true;
        yield return new WaitForSeconds(1);
        canNarrateTrigger = false;
    }
}
