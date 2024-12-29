using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlocksLibrary", menuName = "ScriptableObjects/Blocks Library")]
public class BlocksLibrary : ScriptableObject
{
    [SerializeField] public BlockSO dirtBlock;
    [SerializeField] public BlockSO stoneBlock;
    [SerializeField] public BlockSO leavesBlock;
    [SerializeField] public BlockSO woodBlock;
    [SerializeField] public BlockSO planksBlock;
}
