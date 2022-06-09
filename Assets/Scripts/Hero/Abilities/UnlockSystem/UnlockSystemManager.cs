using UnityEngine;

public class UnlockSystemManager : MonoBehaviour
{
    [SerializeField] private int countOfItemsToUnlock;
    [SerializeField] private Saver saver;
    [SerializeField] private UnlockSystemUI unlockUi;

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
            airAtack.enabled = true;
        }

        if (counter >= countOfItemsToUnlock * 2)
        {
            mightyPunch.enabled = true;
        }
    } 

    public void TryUnlock(int scrollNum)
    {
        counter++;
        unlockUi.UpdateUi();
        saver.SaveCollectableNum(counter);
        saver.SaveScrolls(scrollNum);
        if ( IsEnoughtItems())
        {
            if (airAtack.enabled == false)
            {
                airAtack.enabled = true;
            }
            else if (mightyPunch.enabled == false)
            {
                mightyPunch.enabled = true;
            }  
                counter = 0;
        }
    }

    private bool IsEnoughtItems()
    {
        return counter >= countOfItemsToUnlock;
    }
}
