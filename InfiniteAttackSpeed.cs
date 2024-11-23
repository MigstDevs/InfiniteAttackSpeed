using MelonLoader;
using BTD_Mod_Helper;
using InfiniteAttackSpeed;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppSystem.Linq;

[assembly: MelonInfo(typeof(InfiniteAttackSpeed.InfiniteAttackSpeed), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace InfiniteAttackSpeed;

public class InfiniteAttackSpeed : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<InfiniteAttackSpeed>("InfiniteAttackSpeed loaded! pop those bloons now XD");
    }

    public override void OnNewGameModel(GameModel result)
    {
        foreach(var weapon in result.GetDescendants<WeaponModel>().ToList())
        {
            weapon.rate = 0;
        }
    }
}