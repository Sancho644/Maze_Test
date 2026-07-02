using System;
using Core;
using Core.Signals;
using UnityEngine;
using Zenject;

namespace Input
{
    public class CursorController : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        public CursorController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            Lock();
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.State == GameState.Playing)
                Lock();
            else
                Unlock();
        }

        private void Lock()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Unlock()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}