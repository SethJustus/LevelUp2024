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

    public void SwapWeapons()
    {
        _equippedWeaponIndex = _equippedWeaponIndex > weapons.Length - 2 ? 0 : _equippedWeaponIndex + 1;
        this.EquippedWeapon = weapons[_equippedWeaponIndex];
    }
}
