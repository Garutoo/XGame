using UnityEngine;

public class LoadSceneTimeline : MonoBehaviour
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
            sceneLoader.LoadFirstLevel();
        }
    }
}