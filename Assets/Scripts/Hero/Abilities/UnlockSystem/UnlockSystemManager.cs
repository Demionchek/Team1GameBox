using UnityEngine;

public class UnlockSystemManager : MonoBehaviour
{
    [SerializeField] private int _countOfItemsToUnlock;
    [SerializeField] private UnlockSystemUI _unlockUi;
    [SerializeField] private TutorialNodeParser _tutorialManager;
    [SerializeField] private DialogueGrapgh[] _tutorialGraph = new DialogueGrapgh[4];

    private AirAtack _airAtack;
    private MightyPunch _mightyPunch;
    private static int _tutorialId;
    private PlayerData _playerData;
    public int Counter { get; private set; }

    private void Start()
    {
        SavingSystem savingSystem = new SavingSystem();
        _playerData = new PlayerData();
        savingSystem.LoadPlayerData(ref _playerData);
        _tutorialId = _playerData.ScrollsCountData;
        TryGetComponent();
        Counter = _playerData.ScrollsCountData;
        UnlockOnLoad();
    }

    private void TryGetComponent()
    {
        try
        {
            _airAtack = GetComponent<AirAtack>();
            _mightyPunch = GetComponent<MightyPunch>();
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
            _unlockUi.UpdateUi();
        }
        if (Counter >= _countOfItemsToUnlock)
        {
            _mightyPunch.enabled = true;
            _unlockUi.UpdateUi();
            _unlockUi.UpdateUi();
        }
        if (Counter == 3)
        {
            _unlockUi.UpdateUi();
            _unlockUi.UpdateUi();
            _unlockUi.UpdateUi();
        }
        if (Counter >= _countOfItemsToUnlock * 2)
        {
            _airAtack.enabled = true;
            _unlockUi.UpdateUi();
            _unlockUi.UpdateUi();
            _unlockUi.UpdateUi();
            _unlockUi.UpdateUi();
        }
    } 

    public void TryUnlock()
    {        
        StartTutorialUi(_tutorialId);
        Counter++;
        _unlockUi.UpdateUi();
        if ( IsEnoughtItems())
        {
            if (_mightyPunch.enabled == false)
            {
                _mightyPunch.enabled = true;
            }
            else if (_airAtack.enabled == false)
            {
                _airAtack.enabled = true;
                Counter = 4;
            }
            if (Counter !=4)
            {
                Counter = 0;
            }
        }
    }

    private bool IsEnoughtItems()
    {
        return Counter >= _countOfItemsToUnlock;
    }

    private void StartTutorialUi(int tutorialID)
    {
        _tutorialId++;
        _tutorialManager.StartNode(_tutorialGraph[tutorialID]);
    }
}
