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

    public int i = 0;
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
        
    }
    public void TakeSprite(int pos)
    {
        TakeSprite(pos % (texture.width / width), Mathf.Min(pos / (texture.width / width)));
    }
    public void TakeSprite(int x, int y)
    {
        
        sprite = Sprite.Create(texture, new Rect(x * width, ((texture.height / height) - (y + 1)) * height, width, height), new Vector2(0.49f, 0.35f),50f);
        spriteRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        character.CurrentState.AnimProgress();
        Debug.Log(character.CurrentState.GetName() + " " + character.CurrentState.percentProgress);
        time -= Time.deltaTime;
        if(time < 0)
        {
            time = 0.5f;
            ++i;
            TakeSprite(i);
        }
        
    }
    public void LoadNude()
    {
       
        int mipCount = Mathf.Min(3, character.spritebase.mipmapCount);

        for (int mip = 0; mip < mipCount; ++mip)
        {
            Color[] baseSprite = character.spritebase.GetPixels(mip);
            texture.SetPixels(baseSprite, mip);
        }
        texture.Apply(false);
        
        if (character.pants != null) LoadSprite(character.pants.texture);
        if (character.torso != null) LoadSprite(character.torso.texture);
        if (character.helmet != null) LoadSprite(character.helmet.texture);
        if (character.mitt != null) LoadSprite(character.mitt.texture);
        //HitWeapon hw = character.weapon.GetComponent(typeof(HitWeapon)) as HitWeapon;
        //if (hw.texture != null) LoadSprite(hw.texture);
        
    }

    public void LoadSprite(Texture2D textureAdd)
    {
        int mipCount = Mathf.Min(3, texture.mipmapCount);

        for (int mip = 0; mip < mipCount; ++mip)
        {
            Color[] finalSprite = texture.GetPixels(mip);
            Color[] textureSprite = textureAdd.GetPixels(mip);
            for (int i = 0; i < finalSprite.Length; ++i)
            {
                if (textureSprite[i].a > 0)
                    finalSprite[i] = textureSprite[i];
                else
                    finalSprite[i] = finalSprite[i];
            }
            texture.SetPixels(finalSprite, mip);
        }
        texture.Apply(false);
    }
}
