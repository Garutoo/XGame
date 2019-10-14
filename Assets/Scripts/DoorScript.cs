using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    SceneLoader sceneLoader;
    Animator animator;
    string currentScene;

    void Start()
    {
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
        if (PlayerController.canOpenDoorBool)
        {
            switch (currentScene)
            {
                case ("Tutorial"):
                    sceneLoader.LoadNextScene();
                    break;
            }
        }
    }

}
