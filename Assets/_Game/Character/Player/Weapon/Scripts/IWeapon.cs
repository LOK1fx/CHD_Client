namespace LOK1game.Weapon
{
    public interface IWeapon
    {
        bool TryAtack(object sender, PlayerHand hand);
        void OnEquip(object sender);
        void OnDequip(object sender);
        WeaponData GetData();
    }
}
