using UnityEngine;

public class UnlockSystemManager : MonoBehaviour
{
    [SerializeField] private int countOfItemsToUnlock;

    private AirAtack airAtack;
    private MIghtyPunch mightyPunch;
    private int counter;

    private void Start()
    {
        TryGetComponent();
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

    public void TryUnlock()
    {
        counter++;

        if ( IsEnoughtItems())
        {
            if (airAtack.enabled == false)
                airAtack.enabled = true;
            else if (mightyPunch.enabled == false)
                mightyPunch.enabled = true;
            counter = 0;
        }
    }

    private bool IsEnoughtItems()
    {
        return counter >= countOfItemsToUnlock;
    }
}
