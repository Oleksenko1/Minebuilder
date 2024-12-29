using UnityEngine;
using Zenject;

public class PlayerZenInjector : MonoInstaller
{
    [SerializeField] private PlayerInput playerInput;

    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().FromInstance(playerInput);
    }
}
