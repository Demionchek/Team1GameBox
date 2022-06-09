using UnityEngine;

public class UnlockSystemManager : MonoBehaviour
{
    [SerializeField] private int countOfItemsToUnlock;
    [SerializeField] private Saver saver;
    [SerializeField] private UnlockSystemUI unlockUi;
    [SerializeField] private TutorialNodeParser tutorialManager;
    [SerializeField] private DialogueGrapgh[] tutorialGraph = new DialogueGrapgh[4];

    private AirAtack airAtack;
    private MIghtyPunch mightyPunch;
    private int counter;

    private void Start()
    {
        TryGetComponent();
        saver.LoadCollectable();
        counter = saver.CollectToSave;
        UnlockOnLoad();
    }

    private void TryGetComponent()
    {
        try
        {
            airAtack = GetComponent<AirAtack>();
            mightyPunch = GetComponent<MIghtyPunch>();
        }
        catch
        {
            Debug.Log("One or more components doesnt exist");
        }
    }

    private void UnlockOnLoad()
    {
        if (counter >= countOfItemsToUnlock)
        {
            mightyPunch.enabled = true;
        }

        if (counter >= countOfItemsToUnlock * 2)
        {
            airAtack.enabled = true;
        }
    } 

    public void TryUnlock(int scrollNum)
    {        
        StartTutorialUi(counter);
        counter++;
        unlockUi.UpdateUi();
        saver.SaveCollectableNum(counter);
        saver.SaveScrolls(scrollNum);
        if ( IsEnoughtItems())
        {
            if (mightyPunch.enabled == false)
            {
                mightyPunch.enabled = true;
            }
            else if (airAtack.enabled == false)
            {
                airAtack.enabled = true;
            }  
                counter = 0;
        }
    }

    private bool IsEnoughtItems()
    {
        return counter >= countOfItemsToUnlock;
    }

    private void StartTutorialUi(int tutorialId)
    {
        tutorialManager.StartNode(tutorialGraph[tutorialId]);
    }
}
