using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    private HitConfig hit;
    private GameObject hitbox;
    private float rotate = 0;
    private float time = 0;

    public HitState(CharacterController pc,Direction direction) : base(pc, direction)
    {
        hit = new HitConfig();
        hasHand = true;
        hitbox = MonoBehaviour.Instantiate(character.hitbox,character.transform);
        RotateWeapon();
        character.Move = Vector2.zero;
        Move();
        hitbox.SetActive(true);
        
    }
    public void RotateWeapon()
    {
        Debug.Log(direction);
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
        hitbox.transform.eulerAngles = new Vector3(0, 0, rotate - hit.beginRotation);
    }
    public override void HandUpdate()
    {
        time += Time.deltaTime;

        hitbox.transform.eulerAngles = new Vector3(0, 0, rotate + hit.beginRotation + (hit.endRotation - hit.beginRotation) * time/hit.time);
        if (time > hit.time)
        {
            MonoBehaviour.Destroy(character.transform.GetChild(0).gameObject);
            character.CurrentState = new State(character, direction);
        }
    }
}
