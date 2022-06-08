using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueException : System.Exception
{
    public DialogueException(string message) : base(message)
    {

    }
}

public class DialogueSequencer : MonoBehaviour
{
    public delegate void DialogueCallback(Dialogue dialogue);
    public delegate void DialogueNodeCallback(DialogueNode node);

    public DialogueCallback onDialogueStart;
    public DialogueCallback onDialogueEnd;
    public DialogueNodeCallback onDialogueNodeStart;
    public DialogueNodeCallback onDialogueNodeEnd;

    private Dialogue currentDialogue;
    private DialogueNode currentNode;

    public void StartDialogue(Dialogue dialogue)
    {
        if(currentDialogue == null)
        {
            currentDialogue = dialogue;
            onDialogueStart?.Invoke(currentDialogue);
            StartDialogueNode(dialogue.FirstNode);
        }
        else
        {
            throw new DialogueException("Can't start a dialogue when another is already running. ");
        }
    }

    public void EndDialogue(Dialogue dialogue)
    {
        if(currentDialogue == dialogue)
        {
            StopDialogueNode(currentNode);
            onDialogueEnd?.Invoke(currentDialogue);
            currentDialogue = null;
        }
        else
        {
            throw new DialogueException("Try to stop a dialogue that isn't running. ");
        }
    }

    private bool CanStartNode(DialogueNode node)
    {
        return (currentNode == null || node == null || currentNode.CanBeFollowedByNode(node));
    }

    public void StartDialogueNode(DialogueNode node)
    {
        if(CanStartNode(node))
        {
            StopDialogueNode(currentNode);

            currentNode = node;

            if (currentNode != null)
            {
                onDialogueNodeStart?.Invoke(currentNode);
            }
            else
            {
                EndDialogue(currentDialogue);
            }
        }
        else
        {
            throw new DialogueException("Failed to start dialogue node. ");
        }
    }    
    
    public void StopDialogueNode(DialogueNode node)
    {
        if (currentNode == node)
        {
            onDialogueNodeEnd?.Invoke(currentNode);
            currentNode = null;
        }
        else
        {
            throw new DialogueException("Trying to stop a dialogue node that ins't running.");
        }
    }
}
