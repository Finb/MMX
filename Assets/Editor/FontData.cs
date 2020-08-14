
using UnityEngine;
using UnityEditor;
using System.IO;

[System.Serializable]
public class FontData : ScriptableObject
{
    [SerializeField]
    public Font defaultFont;
}