using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    SceneLoader sceneLoader;
    Animator animator;
    string currentScene;
    bool isInRange = false;

    void Start()
    {
        PlayerController.canOpen = false;
        animator = GetComponent<Animator>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        currentScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        #region Animation Check
        if (PlayerController.canOpen)
        {
            animator.SetBool("doorOpen", true);
        }
        else if (!PlayerController.canOpen)
        {
            animator.SetBool("doorOpen", false);
        }
        #endregion
        if (PlayerController.canOpenDoorBool && isInRange == true)
        {
            sceneLoader.LoadNextScene();
            PlayerController.canOpenDoorBool = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            PlayerController.canOpen = false;
        }
    }
}