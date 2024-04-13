using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    private SummoningManager _manager;
    private SummoningSO _datas;
    public void Init(SummoningManager manager, SummoningSO datas)
    {
        _manager = manager;
        _datas = datas;
        _sprite.sprite = datas.SummoningImage;
    }
}
