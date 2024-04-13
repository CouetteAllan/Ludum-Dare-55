using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pattern", menuName = "Pattern")]
public class PatternSO : ScriptableObject
{
    public string PatternName;
    public GameObject[] PatternPrefabs;

}
