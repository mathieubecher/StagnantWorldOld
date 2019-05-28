using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animator : MonoBehaviour
{
    // original texture
    public Texture2D SPRITEBASE;
    // main character texture
    private Texture2D texture;
    // nb sprites
    public int width;
    public int height;
    
    private SpriteRenderer spriteRenderer;
    // Controller
    private HumanController character;

    // Actual sprite
    public Sprite sprite;
    // Animation direction
    public Direction direction;
    // Actual animation
    private AnimSpritePos actualAnim;
    // All animation
    private CharacterAnimMap allAnim;

    // Direction or state change
    public bool changeStateDir = false;
    // Last state treat
    private string lastState = "";
    // Last hit id
    private int lastId = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Get controller
        character = GetComponent(typeof(HumanController)) as HumanController;
        // Get spriterenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Load Nude
        LoadNude();
        // First sprite to default
        TakeSprite(0);
        // Load all anim
        allAnim = new CharacterAnimMap();
        // Get first anim
        actualAnim = allAnim.Get("Move");
        
    }
    // Copy of original texture
    public void LoadNude()
    {
        texture = SPRITEBASE;
        /*
        texture = new Texture2D(SPRITEBASE.width, SPRITEBASE.height);
        texture.filterMode = FilterMode.Point;

        Color[] baseSprite = SPRITEBASE.GetPixels();
        texture.SetPixels(baseSprite);

        texture.Apply(false);
        */
    }

    void Update()
    {
        // Update all data
        character.CurrentState.AnimProgress();
        UpdateDirection();
        // Update current sprite
        UpdateSprite();
        
    }
    // Load new sprite
    public void TakeSprite(int pos)
    {
        TakeSprite(pos % (texture.width / width), Mathf.Min(pos / (texture.width / width)));
    }

    public void TakeSprite(int x, int y)
    {

        sprite = Sprite.Create(texture, new Rect(x * width, ((texture.height / height) - (y + 1)) * height, width, height), new Vector2(0.49f, 0.35f), 50f);
        spriteRenderer.sprite = sprite;
        if (character.pants.equip != null) LoadSprite(character.pants,x,y);
        if (character.torso.equip != null) LoadSprite(character.torso, x, y);
        if (character.helmet.equip != null) LoadSprite(character.helmet, x, y);
        if (character.mitt.equip != null) LoadSprite(character.mitt, x, y);
        if(character.leftWeapon.equip != null) LoadSprite(character.leftWeapon, x, y);
        if (character.weapon.equip != null) LoadSprite(character.weapon, x, y);
    }
    public void LoadSprite(EquipmentPlace equipPlace, int x, int y)
    {
        equipPlace.GetComponent<SpriteRenderer>().sprite = Sprite.Create(equipPlace.equip.texture, new Rect(x * width, ((texture.height / height) - (y + 1)) * height, width, height), new Vector2(0.49f, 0.35f), 50f);
    }

    // Update anim direction
    private void UpdateDirection()
    {
        Direction last = direction;
        if (character.CurrentState.Direction == Direction.LEFT ||
           character.CurrentState.Direction == Direction.RIGHT ||
           character.CurrentState.Direction == Direction.TOP ||
           character.CurrentState.Direction == Direction.BOTTOM) direction = character.CurrentState.Direction;
        if (last != direction) changeStateDir = true;
    }


    private void UpdateSprite()
    {
        bool change = false;
        if(lastState != character.CurrentState.GetName())
        {
            lastState = character.CurrentState.GetName();
            changeStateDir = true;
        }
        if (lastState.Contains("Hit"))
        {
            try{ 
                HitState state = character.CurrentState as HitState;
                if(lastId != state.HitConfig.id)
                {
                    lastId = state.HitConfig.id;
                    changeStateDir = true;
                }
            }
            catch
            {
                //Debug.Log("can't load hit");
            }
        }
        else if (lastId != 0){
            lastId = 0;
            changeStateDir = true;
        }

        if (changeStateDir)
        {
            changeStateDir = false;
            
            actualAnim = allAnim.Get(lastState, direction, lastId);
            change = true;
        }
        if(!change) change = actualAnim.Next(character.CurrentState.percentProgress);
        if(change) TakeSprite(actualAnim.ActualSprite);
    }
}
