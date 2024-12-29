using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField] private BlockSO blockType;
    [SerializeField] public Renderer renderer;
    public virtual void OnPlaced()
    {
        // Plays SFX of place block
        if(blockType.placeSFX != null)
            AudioSource.PlayClipAtPoint(blockType.placeSFX, transform.position, blockType.placeSFXVolume);
    }
    
    public virtual void DestroyBlock()
    {
        OnDestroyed();

        Destroy(gameObject);
    }
    protected virtual void OnDestroyed()
    {
        // Plays SFX of break block
        if (blockType.breakSFX != null)
            AudioSource.PlayClipAtPoint(blockType.breakSFX, transform.position, blockType.breakSFXVolume);

        BlockBreakVFX.Instance.CreateVFX(blockType, transform.position);
    }
    public BlockSO GetBlockType()
    {
        return blockType;
    }
}
