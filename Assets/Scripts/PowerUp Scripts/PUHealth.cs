using System.Collections;
using UnityEngine;

public class PUHealth : MonoBehaviour
{

    [SerializeField] int healthMultiplier;
    PlayerController player;
    [SerializeField] Animator[] animators;
    void Start()
    {
        if (animators == null)
        {
            return;
        }
        player = FindObjectOfType<PlayerController>();
        healthMultiplier = 1;
        if (gameObject.activeSelf)
        {
            player.canPowerUp = true;
        }
    }

    public void Click()
    {
        if (PlayerController.howManyPowerUps > 0)
        {
            PlayerController.howManyPowerUps--;
            player.health += healthMultiplier;
        }
        else if (PlayerController.howManyPowerUps == 0)
        {
            player.health += healthMultiplier;
            StartCoroutine(AnimatorOut());
        }


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
