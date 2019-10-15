using UnityEngine;

public class LoadMainMenuTimeline : MonoBehaviour
{
    SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Update()
    {
        if (isActiveAndEnabled)
        {
            sceneLoader.LoadMainMenu();
        }
    }
}