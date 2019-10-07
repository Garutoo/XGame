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
        player.jumpSpeed += jumpMultiplier;
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
