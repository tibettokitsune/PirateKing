using UniRx;
using UnityEngine;

namespace Game.Units
{
    public enum BodyPart
    {
        Head,
        LeftHand,
        RightHand,
        Body,
        LeftLeg,
        RightLeg
    }

    public enum DamageState
    {
        NoDamage = 0,
        MiddleDamage = 1,
        HighDamage = 2,
        FullDamage = 3
    }

    public interface IDamaged
    {
        public void GetDamage();
        public int TeamID { get; }
    }
    
    public class DamageTarget : MonoBehaviour, IDamaged
    {
        public ReactiveCommand OnDamage { get; } = new ReactiveCommand();
        public ReactiveCommand OnPartDestroy { get; } = new ReactiveCommand();

        public CompositeDisposable PartDisposable { get; } = new CompositeDisposable();
        
        [SerializeField] private BodyPart bodyPart;
        [SerializeField] private DamageState damageState;
        private int _teamID;
        public void GetDamage()
        {
            // var points = (int)damageState;
            // points++;
            // damageState =(DamageState) points;
            OnDamage.Execute();
            if (damageState == DamageState.FullDamage)
            {
                OnPartDestroy.Execute();
                PartDisposable.Dispose();
            }
        }

        public int TeamID => _teamID;

        public void Setup(int teamID)
        {
            _teamID = teamID;
        }
    }
}