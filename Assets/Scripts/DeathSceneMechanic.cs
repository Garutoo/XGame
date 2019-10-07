using System.Collections;
using UnityEngine;
public class DeathSceneMechanic : MonoBehaviour
{
    [SerializeField] float secondsToWait;
    [SerializeField] GameObject[] lights;
    Animator animator;
    [SerializeField] Animator xayooAnim;
    [SerializeField] GameObject xayoo;
    [SerializeField] GameObject blackBG;
    Vector3 offset = new Vector3(0, 0, 10);
    void Start()
    {
        if (PlayerController.isEnabledCanvasOfDeath == true)
        {
            blackBG.SetActive(true);
            xayoo.SetActive(true);
            animator = GetComponent<Animator>();
            blackBG.transform.position = Camera.main.transform.position + offset;
            xayoo.transform.position = Camera.main.transform.position + offset;
            StartCoroutine(DeathCanvasFade());
            foreach (GameObject light in lights)
            {
                try
                {
                    light.SetActive(false);
                }
                catch
                {
                    return;
                }
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
