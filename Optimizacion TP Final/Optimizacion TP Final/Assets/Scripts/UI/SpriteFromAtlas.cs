using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class SpriteFromAtlas : MonoBehaviour
{
    [SerializeField]SpriteAtlas atlas;
    [SerializeField]string spriteName;

    void Start()
    {
        GetComponent<Image>().sprite = atlas.GetSprite(spriteName); 
    }

    
}
