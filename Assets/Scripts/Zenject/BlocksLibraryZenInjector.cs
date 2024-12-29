using UnityEngine;
using Zenject;

public class BlocksLibraryZenInjector : MonoInstaller
{
    [SerializeField] private BlocksLibrary blocksLibrary;

    public override void InstallBindings()
    {
        Container.Bind<BlocksLibrary>().FromInstance(blocksLibrary);
    }
}
