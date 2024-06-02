using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueScriptable : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}

[System.Serializable]
public class DialogueLine
{
    public string characterNameIndonesian;
    public string lineIndonesian;
    public float duration;
    public AudioClip audioClip; // Add this line to include an audio clip
}
