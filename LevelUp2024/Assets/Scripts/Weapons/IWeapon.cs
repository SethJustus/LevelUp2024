using UnityEngine;

public interface IWeapon
{
    bool IsEquipped { get; set; }

    void Attack();
}
