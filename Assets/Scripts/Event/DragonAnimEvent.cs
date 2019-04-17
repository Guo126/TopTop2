using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimEvent : MonoBehaviour {

   // public FUntyEvent OnAttack;
    public IUnityEvent reward,update;
    [SerializeField] private int hurt = 20;
    [SerializeField] private int level = 1;

    void AttackTo()
    {

        PlayerMes.getInstance().BloodNum -= ((hurt-PlayerMes.getInstance().Defence)>0? (hurt - PlayerMes.getInstance().Defence):0);
        update.Invoke(1);
    
    }
    private void OnDestroy()
    {
        reward.Invoke(level);
    }
}
