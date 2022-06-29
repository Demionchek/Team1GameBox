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
    public int Counter { get; private set; }
    private static int tutorialId;

    private void Start()
    {
        TryGetComponent();
        saver.LoadCollectable();
        Counter = saver.CollectToSave;
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
        if (Counter == 1)
        {
            unlockUi.UpdateUi();
        }
        if (Counter >= countOfItemsToUnlock)
        {
            mightyPunch.enabled = true;
            unlockUi.UpdateUi();
            unlockUi.UpdateUi();
        }
        if (Counter == 3)
        {
            unlockUi.UpdateUi();
            unlockUi.UpdateUi();
            unlockUi.UpdateUi();
        }

        if (Counter >= countOfItemsToUnlock * 2)
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
        Counter++;
        unlockUi.UpdateUi();
        saver.SaveCollectableNum(Counter);
        saver.SaveScrolls(scrollNum);
        if ( IsEnoughtItems())
        {
            if (mightyPunch.enabled == false)
            {
                mightyPunch.enabled = true;
                saver.SaveCollectableNum(Counter);
            }
            else if (airAtack.enabled == false)
            {
                airAtack.enabled = true;
                Counter = 4;
                saver.SaveCollectableNum(Counter);
            }
            saver.SaveCollectableNum(Counter);
            if (Counter !=4)
            {
                Counter = 0;
            }
        }
    }

    private bool IsEnoughtItems()
    {
        return Counter >= countOfItemsToUnlock;
    }

    private void StartTutorialUi(int tutorialID)
    {
        tutorialId++;
        tutorialManager.StartNode(tutorialGraph[tutorialID]);
    }
}
