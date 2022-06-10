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
    private int tutorialId;

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
        if (counter == 1)
        {
            unlockUi.UpdateUi();
        }
        if (counter >= countOfItemsToUnlock)
        {
            mightyPunch.enabled = true;
            unlockUi.UpdateUi();
            unlockUi.UpdateUi();
        }
        if (counter == 3)
        {
            unlockUi.UpdateUi();
            unlockUi.UpdateUi();
            unlockUi.UpdateUi();
        }

        if (counter >= countOfItemsToUnlock * 2)
        {
            airAtack.enabled = true;
            unlockUi.UpdateUi();
            unlockUi.UpdateUi();
            unlockUi.UpdateUi();
            unlockUi.UpdateUi();
        }
    } 

    public void TryUnlock(int scrollNum)
    {        
        StartTutorialUi(tutorialId);
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
                counter = 4;
                saver.SaveCollectableNum(counter);
            }
            if (counter !=4)
            {
                counter = 0;
            }
        }
    }

    private bool IsEnoughtItems()
    {
        return counter >= countOfItemsToUnlock;
    }

    private void StartTutorialUi(int tutorialID)
    {
        tutorialId++;
        tutorialManager.StartNode(tutorialGraph[tutorialID]);
    }
}
