using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animator : MonoBehaviour
{
    public Texture2D SPRITEBASE;
    private Texture2D texture;
    public int width;
    public int height;
    private SpriteRenderer spriteRenderer;
    private HumanController character;
    public Sprite sprite;
    public Direction direction;
    private AnimSpritePos actualAnim;
    private CharacterAnimMap allAnim;

    public bool changeStateDir = false;
    private string lastState = "";
    private int lastId = 0;

    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent(typeof(HumanController)) as HumanController;
        spriteRenderer = GetComponent<SpriteRenderer>();
        texture = new Texture2D(SPRITEBASE.width, SPRITEBASE.height);
        texture.filterMode = FilterMode.Point;
        LoadNude();
        TakeSprite(0);
        allAnim = new CharacterAnimMap();
        actualAnim = allAnim.Get("Move");
        
    }

    // Update is called once per frame
    void Update()
    {
        character.CurrentState.AnimProgress();
        UpdateDirection();
        UpdateSprite();
        
    }

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
    }

    public void LoadSprite(EquipmentPlace equipPlace, int x, int y)
    {
        equipPlace.GetComponent<SpriteRenderer>().sprite = Sprite.Create(equipPlace.equip.texture, new Rect(x * width, ((texture.height / height) - (y + 1)) * height, width, height), new Vector2(0.49f, 0.35f), 50f);
    }

    public void LoadNude()
    {

        Color[] baseSprite = SPRITEBASE.GetPixels();
        texture.SetPixels(baseSprite);

        texture.Apply(false);
        /*
        if (character.pants != null) LoadTexture(character.pants.texture);
        if (character.torso != null) LoadTexture(character.torso.texture);
        if (character.helmet != null) LoadTexture(character.helmet.texture);
        if (character.mitt != null) LoadTexture(character.mitt.texture);
        HitWeapon hw = character.weapon.GetComponent(typeof(HitWeapon)) as HitWeapon;
        if (hw.texture != null) LoadSprite(hw.texture);
        */
    }

    public void LoadTexture(Texture2D textureAdd)
    {
        int mipCount = Mathf.Min(textureAdd.mipmapCount, texture.mipmapCount);

        Color[] finalSprite = texture.GetPixels();
        Color[] textureSprite = textureAdd.GetPixels();
        for (int i = 0; i < finalSprite.Length; ++i)
        {
            if (textureSprite[i].a > 0)
                finalSprite[i] = textureSprite[i];
            else
                finalSprite[i] = finalSprite[i];
        }
        texture.SetPixels(finalSprite);
        texture.Apply(false);
    }


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
                Debug.Log("can't load hit");
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
