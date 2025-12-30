using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] MonoBehaviour currentWeaponScript;
    private IWeapon currentWeapon;
    void Awake()
    {
        currentWeapon = currentWeaponScript as IWeapon;
    }

    public void PerformAttack(bool _isHeavy, int _comboIndex)
    {
        if (_isHeavy)
        {
            currentWeapon?.HeavyAttack(_comboIndex);
        }
        else
        {
            currentWeapon?.LightAttack(_comboIndex);
        }
    }
    public void PerformSpecialAttack(bool _isHeavy, AttackContext _context) 
    {
        if (currentWeapon == null) return;
        currentWeapon.SpecialAttack(_isHeavy, _context);
    }
}
