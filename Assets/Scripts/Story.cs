using UnityEngine;


[CreateAssetMenu(fileName = "Story-", menuName = "Story")]
public class Story : ScriptableObject
{
    public int level = 0;
    public StoryLine[] storyLines;
}
