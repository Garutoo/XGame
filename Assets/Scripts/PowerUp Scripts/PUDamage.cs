using System.Collections;
using UnityEngine;

public class PUDamage : MonoBehaviour
{

    [SerializeField] int damageMultiplier;
    PlayerController player;
    [SerializeField] Animator[] animators;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        damageMultiplier = 1;
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
            player.hejterDamage += damageMultiplier;
        }
        else if (PlayerController.howManyPowerUps == 0)
        {
            player.hejterDamage += damageMultiplier;
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
