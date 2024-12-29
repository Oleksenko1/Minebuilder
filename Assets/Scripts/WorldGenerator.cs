using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


// Generates flat world
public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private int dirtLevelHeight = 3;
    [Inject] private GlobalMap blocksMap;
    [Inject] private BlocksLibrary blocksLibrary;

    private int mapHalfHeight;
    private int mapHalfWidth;
    private void Awake()
    {
        mapHalfHeight = blocksMap.GetMapHalfHeight() - 1;
        mapHalfWidth = blocksMap.GetMapHalfWidth() - 1;

        GenerateStoneLevel();

        GenerateDirtLevel();
    }

    private void GenerateStoneLevel()
    {
        BlockSO block = blocksLibrary.stoneBlock;
        for (int y = -mapHalfHeight + 1; y <= -dirtLevelHeight; y++)
        {
            for (int x = -mapHalfWidth + 1; x < mapHalfWidth; x++)
            {
                for (int z = -mapHalfWidth + 1; z < mapHalfWidth; z++)
                {
                    Vector3 pos = new Vector3(x, y, z);

                    // Spawns block (withour rotation)
                    var newBlock = Instantiate(block.prefab, pos, Quaternion.identity);

                    // Adds block to globalMap
                    blocksMap.AddBlock(pos, newBlock.GetComponent<BlockScript>());
                }
            }
        }
    }
    private void GenerateDirtLevel()
    {
        BlockSO block = blocksLibrary.dirtBlock;
        for (int y = -dirtLevelHeight + 1; y <= 0; y++)
        {
            for (int x = -mapHalfWidth + 1; x < mapHalfWidth; x++)
            {
                for (int z = -mapHalfWidth + 1; z < mapHalfWidth; z++)
                {
                    Vector3 pos = new Vector3(x, y, z);

                    // If block is out of bounds - skip it
                    if (blocksMap.CheckForBounds(pos) == false)
                    {
                        Debug.LogError("Error in generating dirt!");
                        break;
                    }

                    // Spawns block (withour rotation)
                    var newBlock = Instantiate(block.prefab, pos, Quaternion.identity);

                    // Adds block to globalMap
                    blocksMap.AddBlock(pos, newBlock.GetComponent<BlockScript>());
                }
            }
        }
    }
    private void GenerateTrees()
    {

    }
}
