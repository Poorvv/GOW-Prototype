using UnityEngine;

public interface IWeapon
{
    void Initialize(Animator animator, Transform handSocket);
    void LightAttack(int _comboIndex);
    void HeavyAttack(int _comboIndex);
    void SpecialAttack(bool _isHeavy, AttackContext _comtext);        // optional (charged, throw, etc.)
    void Equip();
    void Unequip();
    void InterruptAttack();      // e.g. for dodging mid-attack
    bool CanAttack();            // used to check cooldown or lock state
    AttackData CurrentAttack { get; }  // gives info about the running attack

}
