using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    private SummoningManager _manager;
    private SummoningSO _datas;
    public void Init(SummoningManager manager, SummoningSO datas)
    {
        _manager = manager;
        _datas = datas;
    }
}
