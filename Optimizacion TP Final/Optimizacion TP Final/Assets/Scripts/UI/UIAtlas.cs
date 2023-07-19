using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIAtlas : MonoBehaviourUI
{
    [SerializeField] private SpriteAtlas _atlas;
    [SerializeField] private Image _image;
    [SerializeField] private string _spriteName;

    private void Awake()
    {
        _image.sprite = _atlas.GetSprite(_spriteName);
    }
}
