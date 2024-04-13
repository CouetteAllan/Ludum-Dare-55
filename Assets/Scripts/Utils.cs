using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    public  static Camera MainCamera
    {
        get
        {
            if(_cam == null)
                _cam = Camera.main;
            return _cam;
        }
    }
    private static Camera _cam;
}
