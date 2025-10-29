using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] Transform weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] Transform weaponSheath;

    void Start()
    {
        //_currentWeaponInSheath = Instantiate(weapon, weaponSheath);
    }

    public void DrawWeapon()
    {
        /*_currentWeaponInHand = Instantiate(weapon,weaponHolder.transform);
        Destroy(_currentWeaponInSheath);*/
        weapon.transform.SetParent(weaponHolder);
        weapon.transform.localPosition = new Vector3(-0.02131431f, -0.0002435625f, -0.311141f);
        weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }
    public void SheathWeapon()
    {
        weapon.transform.SetParent(weaponSheath);
        weapon.transform.localPosition = new Vector3(0.1320566f, -0.2467283f, 0.07776486f);
        weapon.transform.localRotation = Quaternion.Euler(-24.322f, 18.901f, 24.716f);
    }
}
