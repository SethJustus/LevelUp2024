using JetBrains.Annotations;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private IWeapon[] weapons;

    [CanBeNull] public IWeapon EquippedWeapon;

    private int _equippedWeaponIndex;

    void Awake()
    {
        weapons = GetComponentsInChildren<IWeapon>();
        this.EquippedWeapon = weapons[0];
        Debug.Log(weapons.Length + " weapons found");
    }

    public void EquipPrevious()
    {
        if (_equippedWeaponIndex == 0)
        {
            _equippedWeaponIndex = weapons.Length - 1;
        }
        else
        {
            _equippedWeaponIndex--;
        }
    }

    public void EquipNext()
    {
        if (_equippedWeaponIndex == weapons.Length - 1)
        {
            _equippedWeaponIndex = 0;
        }
        else
        {
            _equippedWeaponIndex++;
        }
    }
}
