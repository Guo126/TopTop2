using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimEvent : MonoBehaviour {

    public FUntyEvent OnAttack;
    public IUnityEvent reward;
    [SerializeField] private int hurt = 20;
    [SerializeField] private int level = 1;

    void AttackTo()
    {

        PlayerMes.getInstance().BloodNum -= hurt;
        OnAttack.Invoke((float)PlayerMes.getInstance().BloodNum / PlayerMes.getInstance().BloodMax);
    
    }
    private void OnDestroy()
    {
        reward.Invoke(level);
    }
}
