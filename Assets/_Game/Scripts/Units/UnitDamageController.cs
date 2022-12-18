using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Units
{
    public class UnitDamageController : IDisposable
    {
        public ReactiveCommand OnDamaged { get; } = new ReactiveCommand();
        private List<DamageSource> _damageSources;
        private List<DamageTarget> _damageTargets;

        public CompositeDisposable DamageControllerDisposable { get; } = new CompositeDisposable();
        public void Setup(UnitView view, int teamID)
        {
            _damageSources = view.GetDamageSources();
            foreach (var source in _damageSources)
            {
                source.Setup(teamID);
            }
            _damageTargets = view.GetDamageTargets();
            foreach (var target in _damageTargets)
            {
                target.Setup(teamID);
                target.OnDamage.Subscribe(_ =>
                {
                    OnDamaged.Execute();
                }).AddTo(target.PartDisposable);
            }
        }

        public void AttackState(bool state)
        {
            foreach (var dmgSource in _damageSources)
            {
                dmgSource.DamageSourceState(state);
            }
        }

        public void Dispose()
        {
            OnDamaged?.Dispose();
            DamageControllerDisposable?.Dispose();
        }
    }
}