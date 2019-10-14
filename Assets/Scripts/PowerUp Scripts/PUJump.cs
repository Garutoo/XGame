using System.Collections;
using UnityEngine;

public class PUJump : MonoBehaviour
{
    [SerializeField] float jumpMultiplier;
    PlayerController player;
    [SerializeField] Animator[] animators;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        jumpMultiplier = 50f;
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
            player.jumpSpeed += jumpMultiplier;
        }
        else if (PlayerController.howManyPowerUps == 0)
        {
            player.jumpSpeed += jumpMultiplier;
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
