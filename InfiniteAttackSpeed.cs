using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using InfiniteAttackSpeed;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppSystem.Linq;

[assembly: MelonInfo(typeof(InfiniteAttackSpeed.InfiniteAttackSpeed), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace InfiniteAttackSpeed;

public class InfiniteAttackSpeed : BloonsTD6Mod
{
    private static readonly ModSettingString WeaponRateSetting = new("def")
    {
        displayName = GetLocalizedText("Weapon Rate", "Taxa de Armas"),
        description = GetLocalizedText("Weapon Speed (Lower is faster, type 'def' for default)",
                                       "Velocidade das Armas (Menor é mais rápido, digite 'pdr' para padrão)")
    };

    private static readonly ModSettingEnum<Language> LanguageSetting = new(Language.English)
    {
        displayName = "Language / Idioma",
        description = "Select the language for the mod / Selecione o idioma do mod"
    };

    public override void OnApplicationStart()
    {
        ModHelper.Msg<InfiniteAttackSpeed>("InfiniteAttackSpeed loaded! Configure settings via the cog!");
    }

    public override void OnNewGameModel(GameModel result)
    {
        float weaponRate;
        string input = WeaponRateSetting;

        if (input.ToLower() == "def" || input.ToLower() == "pdr")
        {
            weaponRate = -1;
            ModHelper.Msg<InfiniteAttackSpeed>("Weapon rate set to the default game value.");
        }
        else if (float.TryParse(input, out weaponRate))
        {
            ModHelper.Msg<InfiniteAttackSpeed>($"Weapon rate set to: {weaponRate}");
        }
        else
        {
            weaponRate = -1;
            ModHelper.Warning<InfiniteAttackSpeed>($"Invalid input '{input}', resetting to default.");
        }

        foreach (var weapon in result.GetDescendants<WeaponModel>().ToList())
        {
            weapon.rate = weaponRate >= 0 ? weaponRate : weapon.rate;
        }
    }

    private static string GetLocalizedText(string english, string portuguese)
    {
        var language = LanguageSetting ?? Language.English;
        return language == Language.Portuguese ? portuguese : english;
    }

    public enum Language
    {
        English,
        Portuguese
    }
}