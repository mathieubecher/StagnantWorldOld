using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitShield : HitWeapon
{
    public override void LoadAllHit()
    {
        name = "shield";
        hits.Add(new HitConfig(0, 0.2f, -50, 90, 0, 0.3f));
        hits[0].beginScale = new Vector3(1, 0.5f, 1);
        hits[0].endScale = new Vector3(1, 0.5f, 1);
    }
    public override void LoadCharge(HumanController controller, ChargeHitState state)
    {
        Debug.Log("commence à parer");
        state.weapon = Instantiate(controller.GetWeapon(), controller.transform);
        state.UpdateDirection();
        state.RotateWeapon();
        state.weapon.SetActive(true);

    }

    public override void ChargeHitUpdate(HumanController controller)
    {
        ChargeHitState state = controller.CurrentState as ChargeHitState;

    }
    public override void ChargeHitEnd(HumanController controller)
    {
        int i = 0;
        while (i < controller.transform.childCount && !controller.transform.GetChild(i).gameObject.name.Contains("Shield")) i++;
        if (i < controller.transform.childCount)
            MonoBehaviour.Destroy(controller.transform.GetChild(i).gameObject);
    }
}