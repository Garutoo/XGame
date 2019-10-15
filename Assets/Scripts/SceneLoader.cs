using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;
    int currentSceneIndex;
    [SerializeField] GameObject[] buttons;
    Animator buttonAnimator;
    [HideInInspector] public static bool canTitle = false;

    void Awake()
    {
        Time.timeScale = 1;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadNextSceneRoutine());
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene("Main Menu"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadFirstLevelFromMainMenu()
    {
        foreach (GameObject button in buttons)
        {
            buttonAnimator = button.GetComponent<Animator>();
            buttonAnimator.SetTrigger("end");
        }

        StartCoroutine(LoadScene("Level 1 Cutscene"));
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(LoadScene("Level 1"));
    }


    #region Socials
    public void YouTube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCHWVi__ceLHih0wWSxDJv0g?sub_confirmation=1");
    }

    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=100020182564327");
    }
    #endregion

    #region Coroutines
    IEnumerator LoadNextSceneRoutine()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSecondsRealtime(3f);

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    IEnumerator LoadScene(string scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSecondsRealtime(3f);

        SceneManager.LoadScene(scene);
    }
    #endregion
}