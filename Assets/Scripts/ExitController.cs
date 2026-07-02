using System;
using Diamonds.Signals;
using Player.Signals;
using UnityEngine;
using Zenject;

public class ExitController : MonoBehaviour, IInitializable, IDisposable
{
    private SignalBus _signalBus;

    private bool _isUnlocked;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<AllDiamondsCollectedSignal>(UnlockExit);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<AllDiamondsCollectedSignal>(UnlockExit);
    }

    private void UnlockExit()
    {
        _isUnlocked = true;

        Debug.Log("Exit unlocked");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isUnlocked)
            return;

        if (!other.CompareTag("Player"))
            return;

        _signalBus.Fire<PlayerReachedExitSignal>();
    }
}