using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animator : MonoBehaviour
{
    public Texture2D texture;
    public int width;
    public int height;
    private SpriteRenderer spriteRenderer;
    private CharacterController character;
    public Sprite sprite;
    public Direction direction;
    private AnimSpritePos actualAnim;
    private CharacterAnimMap allAnim;

    private bool changeStateDir = false;
    private string lastState = "";
    private int lastId = 0;

    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent(typeof(CharacterController)) as CharacterController;
        spriteRenderer = GetComponent<SpriteRenderer>();
        texture = new Texture2D(character.spritebase.width, character.spritebase.height);
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
    }

    public void LoadNude()
    {

        Color[] baseSprite = character.spritebase.GetPixels();
        texture.SetPixels(baseSprite);

        texture.Apply(false);
        
        if (character.pants != null) LoadSprite(character.pants.texture);
        if (character.torso != null) LoadSprite(character.torso.texture);
        if (character.helmet != null) LoadSprite(character.helmet.texture);
        if (character.mitt != null) LoadSprite(character.mitt.texture);
        HitWeapon hw = character.weapon.GetComponent(typeof(HitWeapon)) as HitWeapon;
        if (hw.texture != null) LoadSprite(hw.texture);
        
    }

    public void LoadSprite(Texture2D textureAdd)
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
