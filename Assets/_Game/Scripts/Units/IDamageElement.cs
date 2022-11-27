using UnityEngine;

namespace Game.Units
{
    public enum DamageType
    {
        Common,
        Slash,
        Stab
    }
    public enum DegreeOfDamage
    {
        NoDamage,
        SlightDamage,
        ModerateDamage,
        SevereDamage,
        CompleteDamage
    }
    public interface IDamageElement
    {
        public DegreeOfDamage DegreeOfDamage { get; }
        void GetDamage(DamageType damageType, float velocity, Vector3 sourcePoint);
    }
}