using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HitState : State
{
    private HitConfig hit;
    private GameObject weapon;
    private string nameWeapon;
    private float rotate = 0;
    private float time = 0;
    private bool delete = false;
    private float percentProgress = 0;
    private Vector3 endCharacterPosition;

    public HitState(SimpleController controller, Direction direction, HitConfig hit) : base(controller, direction)
    {
        this.hit = hit;
        time = hit.time + hit.beginTime;
        weapon = MonoBehaviour.Instantiate(character.GetWeapon(), character.transform);
        nameWeapon = (weapon.GetComponent(typeof(HitWeapon)) as HitWeapon).name;
        RotateWeapon();
        GetEndCharacterPosition();
        character.Move = Vector2.zero;
        Move();
        UpdateAnim(false,false,false,false);
    }
    public void GetEndCharacterPosition()
    {
        endCharacterPosition = Quaternion.Euler(0,0, rotate) * hit.endPositionCharacter;
    }
    public override void RotateWeapon()
    {
        switch (direction)
        {
            case Direction.TOP:
                rotate = 0;
                break;
            case Direction.BOTTOM:
                rotate = 180;
                break;
            case Direction.LEFT:
                rotate = 90;
                break;
            case Direction.RIGHT:
                rotate = -90;
                break;
            case Direction.TOPLEFT:
                rotate = 45;
                break;
            case Direction.TOPRIGHT:
                rotate = -45;
                break;
            case Direction.BOTTOMLEFT:
                rotate = 135;
                break;
            case Direction.BOTTOMRIGHT:
                rotate = -135;
                break;
        }
        weapon.transform.eulerAngles = new Vector3(0, 0, rotate + hit.beginRotation);
        weapon.transform.GetChild(0).localScale = hit.beginScale;
        weapon.transform.GetChild(0).localPosition = hit.beginPosition;
    }
    public override string GetName()
    {
        return "Hit";
    }

    public override void Move() {
        
        if (time < -hit.endTime) base.UpdateDirection();
        else character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    public override void Hit() {
        nextState = "hit";
        bool isSameWeapon = nameWeapon == (character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon).name;
        Debug.Log(nameWeapon + " " + (character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon).name + " " +isSameWeapon);
        if (time < -hit.endTime || (time > hit.time && !isSameWeapon))
        {

            if (!delete)
            {
                deleteWeapon();
            }
            base.UpdateDirection();
            if (isSameWeapon) NextState();
            else base.NextState();
        }

    }
    public override void Dash() {
        nextState = "dash";
        if (time < -hit.endTime || time > hit.time)
        {
            if (!delete)
            {
                deleteWeapon();
            }
            base.Move();
            NextState();
        }
    }

    public override void ChargeDash()
    {
    }

    public override void ChargeHit()
    {
        nextState = "chargehit";
        if (time < -hit.endTime || time > hit.time)
        {
            if (!delete)
            {
                deleteWeapon();
            }
            base.UpdateDirection();
            NextState();
        }
    }

    public override void Update()
    {
        time -= Time.deltaTime;
        percentProgress = (hit.time - time) / hit.time;
        if (time < -hit.endTime - 0.2f || (time < -hit.endTime && nextState != ""))
        {
           
            base.UpdateDirection();
            NextState();
        }
        if (time < -hit.endTime && !delete)
        {
            deleteWeapon();
        }
        else if (time > 0 && time < hit.time)
        {
            if(!weapon.activeSelf) weapon.SetActive(true);
            character.transform.localPosition += endCharacterPosition*Time.deltaTime/hit.time;
            weapon.transform.eulerAngles = new Vector3(0, 0, rotate + hit.beginRotation + (hit.endRotation - hit.beginRotation) * percentProgress);
            weapon.transform.GetChild(0).localPosition = hit.beginPosition + (hit.endPosition - hit.beginPosition) * percentProgress;
            weapon.transform.GetChild(0).localScale = hit.beginScale + (hit.endScale - hit.beginScale) * percentProgress;
        }
    }

    protected override void NextState()
    {
        if (nextState == "hit" && hit.next != null && nameWeapon == (character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon).name)
        {
            HitWeapon hitweapon = character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon;
            character.CurrentState = new HitState(character, direction, hit.next);
        }
        else base.NextState();
    }
    private void deleteWeapon()
    {
        int i = 0;
        while (i < character.transform.childCount && character.transform.GetChild(i).gameObject.name.Contains("life")) i++;
        if (i < character.transform.childCount)
            MonoBehaviour.Destroy(character.transform.GetChild(i).gameObject);
        delete = true;
    }
}
