using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreakVFX : MonoBehaviour
{
    public static BlockBreakVFX Instance;

    [SerializeField] private int emitAmount = 15;
    [SerializeField] private GameObject vfxPrefab;
    [SerializeField] private List<BlockSO> blockTypes = new List<BlockSO>();
    [SerializeField] private List<Gradient> colors = new List<Gradient>();

    private void Awake()
    {
        Instance = this;
    }

    public void CreateVFX(BlockSO block, Vector3 position)
    {
        int blockId = blockTypes.IndexOf(block);

        Gradient blockColor = colors[blockId];

        var vfx = Instantiate(vfxPrefab, position, Quaternion.identity);

        // Gets particle system and it's main module of VFX
        ParticleSystem ps = vfx.GetComponent<ParticleSystem>();
        var mainModule = ps.main;

        // Sets start color of VFX
        ParticleSystem.MinMaxGradient minMaxGradient = new ParticleSystem.MinMaxGradient(blockColor);
        minMaxGradient.mode = ParticleSystemGradientMode.RandomColor;
        mainModule.startColor = minMaxGradient;

        ps.Emit(emitAmount);
    }
}
