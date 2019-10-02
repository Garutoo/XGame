using System.Collections;
using UnityEngine;
public class DeathSceneMechanic : MonoBehaviour
{
    [SerializeField] float secondsToWait;
    [SerializeField] GameObject[] lights;
    [SerializeField] GameObject lightForDeath;
    Animator animator;
    [SerializeField] Animator xayooAnim;
    [SerializeField] GameObject xayoo;
    void Start()
    {
        if (Player.isEnabledCanvasOfDeath == true)
        {
            xayoo.SetActive(true);
            animator = GetComponent<Animator>();
            lightForDeath.SetActive(true);
            StartCoroutine(DeathCanvasFade());
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }
        }

    }


    IEnumerator DeathCanvasFade()
    {
        xayooAnim.SetBool("Death", true);
        animator.SetTrigger("Canvas");
        yield return null;
    }

}
