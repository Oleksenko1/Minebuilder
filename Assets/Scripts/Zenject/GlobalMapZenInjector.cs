using UnityEngine;
using Zenject;

public class GlobalMapZenInjector : MonoInstaller
{
    [SerializeField] private GlobalMap globalMap;

    public override void InstallBindings()
    {
        Container.Bind<GlobalMap>().FromInstance(globalMap);
    }
}
