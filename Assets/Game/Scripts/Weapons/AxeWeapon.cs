using UnityEngine;
using System.Collections;
public class AxeWeapon : MonoBehaviour, IWeapon
{
    [Header("References")]
    [SerializeField] Animator animator;    // Player's Animator
    [SerializeField] Collider weaponCollider;

    [Header("AttackData")]
    [SerializeField] AttackData lightAttack;
    [SerializeField] AttackData heavyAttack;
    private bool _canAttack = true;
    public AttackData CurrentAttack { get; private set; }

    public void Initialize(Animator animator, Transform handSocket)
    {
        throw new System.NotImplementedException();
    }
    public bool CanAttack() => _canAttack;
    private void Start()
    {
        //CurrentAttack = lightAttack;
    }
    public void LightAttack(int _comboIndex)
    {
        if(_comboIndex == 1)
        {
            CurrentAttack = lightAttack;
            PlayAttack(CurrentAttack);
        }

        else
        {
            
            if (CurrentAttack.NextAttackData != null) 
            {
                PlayAttack(CurrentAttack.NextAttackData);
                CurrentAttack = CurrentAttack.NextAttackData;
            }
            //print(CurrentAttack.NextAttackData.Id);
        }

    }
    public void HeavyAttack(int _comboIndex)
    {
        if (_comboIndex == 1)
        {
            CurrentAttack = heavyAttack;
            PlayAttack(CurrentAttack);
        }

        else
        {

            if (CurrentAttack.NextAttackData != null)
            {
                PlayAttack(CurrentAttack.NextAttackData);
                CurrentAttack = CurrentAttack.NextAttackData;
            }
            //print(CurrentAttack.NextAttackData.Id);
        }

    }

    public void Equip()
    {
        throw new System.NotImplementedException();
    }


    public void SpecialAttack(bool _isHeavy, AttackContext _context)
    {
        print("Yoo");
        if (_isHeavy)
        {
            animator.CrossFade("RunHeavyAttack", 0.1f);
        }
        else
        {
            animator.CrossFade("RunLightAttack", 0.1f);
        }
    }

    public void InterruptAttack()
    {
        throw new System.NotImplementedException();
    }



    public void Unequip()
    {
        throw new System.NotImplementedException();
    }
    private void PlayAttack(AttackData attack)
    {
        animator.applyRootMotion = attack.UsesRootMotion;
        animator.CrossFade(attack.AnimationClip.name, 0.1f);
        //EnableCollider();
    }
}
