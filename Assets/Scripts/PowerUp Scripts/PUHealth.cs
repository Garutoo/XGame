using System.Collections;
using UnityEngine;

public class PUHealth : MonoBehaviour
{

    [SerializeField] int healthMultiplier;
    PlayerController player;
    [SerializeField] Animator[] animators;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        healthMultiplier = 1;
        if (gameObject.activeSelf)
        {
            player.canPowerUp = true;
        }
    }

    public void Click()
    {
        player.health += healthMultiplier;
        StartCoroutine(AnimatorOut());
    }

    IEnumerator AnimatorOut()
    {
        foreach (Animator anim in animators)
        {
            anim.SetBool("anim", false);
        }
        player.canPowerUp = false;
        yield return null;
    }
}
