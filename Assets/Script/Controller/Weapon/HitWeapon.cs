using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWeapon : MonoBehaviour
{
    protected GameObject parent;
    public GameObject hitbox;
    public string name;
    protected List<HitConfig> hits = new List<HitConfig>();
    protected HitConfig chargeHit;
    public float chargeMove = 0;
    public Texture2D texture;

    public List<HitConfig> Hits { get => hits; set => hits = value; }
    public HitConfig ChargeHit { get => chargeHit; set => chargeHit = value; }

    public HitWeapon():base()
    {
        LoadAllHit();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public virtual void LoadAllHit()
    {
        name = "sword";
        Hits.Add(new HitConfig(0));
        Hits[0].next = new HitConfig(1, 0.2f, 70, -70);
        Hits[0].next.next = new HitConfig(2, 0.14f, -150, 50, new Vector3(-0.15f, 0.02f, 0), new Vector3(0, 0.16f, 0), new Vector3(0.5f, 1, 1), new Vector3(0.5f, 1, 1), new Vector3(0, 0.2f, 0), 0.1f, 0.3f);
        chargeHit = new HitConfig(0, 0.2f, -90, 90, 0, 0.3f);
    }

    public virtual void LoadCharge(HumanController controller, ChargeHitState state)
    {
        Debug.Log("Commence à charger son coup");
    }

    public virtual void ChargeHitUpdate(HumanController controller)
    {
        controller.CurrentState.UpdateDirection();
    }
    public virtual void ChargeHitEnd(HumanController controller)
    {

    }
    public void SetParent(GameObject parent)
    {
        this.parent = parent;
    }
}
