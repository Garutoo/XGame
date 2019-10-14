using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NarratorController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI narratorsText;
    string nameOfTheScene;
    Scene currentSceneRunning;
    SceneLoader sceneLoader;
    private Story story;
    private Story triggerStory;
    private int sceneNumber;
    public static bool isNarrating = false;

    private void Start()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        currentSceneRunning = SceneManager.GetActiveScene();
        nameOfTheScene = currentSceneRunning.name;
        sceneLoader = FindObjectOfType<SceneLoader>();
        switch (sceneNumber)
        {
            case (3):
                isNarrating = true;
                break;
            case (4):
                isNarrating = true;
                break;
            case (5):
                isNarrating = true;
                break;
            case (6):
                isNarrating = true;
                break;
        }
        StorySetup();
        TriggerSetup();
        SceneCheck();
    }

    private void Update()
    {
        if (TriggerController.canNarrateTrigger)
        {
            StartCoroutine(StartNarrator(triggerStory));
        }
    }

    private void StorySetup()
    {
        story = (Story)Resources.Load("Story-" + sceneNumber);
        if (story == null)
        {
            return;
        }
    }

    private void TriggerSetup()
    {
        triggerStory = (Story)Resources.Load("Trigger/Story-" + sceneNumber);
        if (story == null)
        {
            return;
        }
    }

    private void SceneCheck() // Checks On Which Scene You Are
    {
        if (story == null) return;
        StartCoroutine(StartNarrator(story));
    }

    public IEnumerator StartNarrator(Story _story)
    {
        foreach (StoryLine storyLine in _story.storyLines)
        {
            if (isNarrating)
            {
                yield return new WaitForSeconds(storyLine.delayTime);
                ChangeText(storyLine.lineText);
            }
            else
            {
                yield break;
            }
        }
    }

    public void ChangeText(string changedText)
    {
        narratorsText.text = changedText;
    }
}