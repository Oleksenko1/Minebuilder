using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMap : MonoBehaviour
{
    private const int mapHalfWidth = 14;
    private const int mapHalfHeight = 10;

    private BlockScript[ , , ] blocksMap = new BlockScript[mapHalfWidth * 2, mapHalfHeight * 2, mapHalfWidth * 2]; 

    public bool CheckForBounds(Vector3 position)
    {
        // Substracting for array index
        position -= Vector3.one;

        bool isInbounds = true;

        isInbounds = position.x < mapHalfWidth - 2 && position.x > -mapHalfWidth && isInbounds; // Check x position
        isInbounds = position.y < mapHalfHeight - 2 && position.y > -mapHalfHeight && isInbounds; // Check y position
        isInbounds = position.z < mapHalfWidth - 2 && position.z > -mapHalfWidth && isInbounds; // Check z position

        return isInbounds;
    }

    public void AddBlock(Vector3 position, BlockScript block)
    {
        // Substracting for array index
        position -= Vector3.one;

        // Adds block to the array
        blocksMap[mapHalfWidth + (int)position.x, mapHalfHeight + (int)position.y, mapHalfWidth + (int)position.z] = block;
        Debug.Log($"Saved {block.GetBlockType().blockName} at {new Vector3(mapHalfWidth + (int)position.x, mapHalfHeight + (int)position.y, mapHalfWidth + (int)position.z)}");

        block.OnPlaced();

        // Updates neighbors if its needed
        UpdateNeighborBlocks(position, block, true);
    }
    public void UpdateNeighborBlocks(Vector3 position, BlockScript block, bool updaterAlive)
    {
        // Gets position of block
        int xPos = (int)position.x + mapHalfWidth;
        int yPos = (int)position.y + mapHalfHeight;
        int zPos = (int)position.z + mapHalfWidth;

        BlockSO blockType = block.GetBlockType();

        // Updates surrounding blocks

        if (blocksMap[xPos - 1, yPos, zPos] != null) blocksMap[xPos - 1, yPos, zPos].renderer.enabled = CheckIfBlockIsVisible(xPos - 1, yPos, zPos); // Left block
        if (blocksMap[xPos + 1, yPos, zPos] != null) blocksMap[xPos + 1, yPos, zPos].renderer.enabled = CheckIfBlockIsVisible(xPos + 1, yPos, zPos); // Right block
        if (blocksMap[xPos, yPos - 1, zPos] != null) blocksMap[xPos, yPos - 1, zPos].renderer.enabled = CheckIfBlockIsVisible(xPos, yPos - 1, zPos); // Bottom block
        if (blocksMap[xPos, yPos + 1, zPos] != null) blocksMap[xPos, yPos + 1, zPos].renderer.enabled = CheckIfBlockIsVisible(xPos, yPos + 1, zPos); // Upper block
        if (blocksMap[xPos, yPos, zPos - 1] != null) blocksMap[xPos, yPos, zPos - 1].renderer.enabled = CheckIfBlockIsVisible(xPos, yPos, zPos - 1); // Front block
        if (blocksMap[xPos, yPos, zPos + 1] != null) blocksMap[xPos, yPos, zPos + 1].renderer.enabled = CheckIfBlockIsVisible(xPos, yPos, zPos + 1); // Bottom block
    }
    public void RemoveBlock(Vector3 position, BlockScript block)
    {
        // Substracting for array index
        position -= Vector3.one;

        // Removes block from array
        blocksMap[mapHalfWidth + (int)position.x, mapHalfHeight + (int)position.y, mapHalfWidth + (int)position.z] = null;
        //Debug.Log($"Removed {block.GetBlockType().blockName} at {position}");

        block.DestroyBlock();

        UpdateNeighborBlocks(position, block, false);
    }
    public int GetMapHalfHeight() { return mapHalfHeight; }
    public int GetMapHalfWidth() { return mapHalfWidth; }
    public bool CheckIfBlockIsVisible(int xPos, int yPos, int zPos)
    {
        bool isVisible = false; 
        
        // Checking if index is in bounds and if there are neighbor blocks covering main block
        // For x position
        if(xPos > 0) isVisible = blocksMap[xPos - 1, yPos, zPos] == null || isVisible; // Слева
        if(xPos < mapHalfWidth * 2 - 1) isVisible = blocksMap[xPos + 1, yPos, zPos] == null || isVisible; // Справа
        // For y position
        if (yPos > 0) isVisible = blocksMap[xPos, yPos - 1, zPos] == null || isVisible; // Снизу
        if (yPos < mapHalfHeight * 2 - 1) isVisible = blocksMap[xPos, yPos + 1, zPos] == null || isVisible; // Сверху
        // For z position
        if (zPos > 0) isVisible = blocksMap[xPos, yPos, zPos - 1] == null || isVisible; // Спереди
        if (zPos < mapHalfWidth * 2 - 1) isVisible = blocksMap[xPos, yPos, zPos + 1] == null || isVisible;   // Сзади

        return isVisible;
    }
}
