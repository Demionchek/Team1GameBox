using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Health boss;
    
    private float currentHp;

    private void Start()
    {
        currentHp = boss.Hp;   
    }

    private void Update()
    {
        UpdateHpBar();  
    }

    public void UpdateHpBar()
    {
        hpBar.fillAmount = boss.Hp / boss.FullHP;
    }
}
