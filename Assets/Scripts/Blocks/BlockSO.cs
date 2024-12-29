using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Block SO", fileName = "NewBlockSO")]
public class BlockSO : ScriptableObject
{
    public Sprite icon;
    public string blockName;
    public GameObject prefab;
    public float height = 1f;
    public bool isRotateble;
    [Header("Sounds")]
    public AudioClip placeSFX;
    public float placeSFXVolume = 1f;
    [Space(10)]
    public AudioClip breakSFX;
    public float breakSFXVolume = 1f;
}
