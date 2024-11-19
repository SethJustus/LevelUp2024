using JetBrains.Annotations;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private IWeapon[] weapons;

    [CanBeNull] public IWeapon EquippedWeapon => weapons[_equippedWeaponIndex];

    private int _equippedWeaponIndex;

    void Awake()
    {
        weapons = GetComponentsInChildren<IWeapon>();
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
        Debug.Log("Equipping Next");
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
