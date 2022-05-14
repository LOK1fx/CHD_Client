using UnityEngine;
using LOK1game.Weapon;

public class WeaponLibraryTester : BaseTester
{
    [SerializeField] private EWeaponId _requestId;

    public override void Test1()
    {
        var weapon = WeaponLibrary.GetWeaponData(_requestId);

        Debug.Log($"{weapon.name} | {weapon.AnimatorController.name}");
    }

    public override void Test2()
    {
        
    }

    public override void Test3()
    {
        
    }
}