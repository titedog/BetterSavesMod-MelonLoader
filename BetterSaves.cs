using MelonLoader;
using HarmonyLib;
using Gloomwood.UI;
using Gloomwood.Saving;
using static HarmonyLib.AccessTools;
using Gloomwood;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Reflection.Emit;

[assembly: MelonInfo(typeof(BetterSavesMod.BetterSaves), "BetterSaves", "1.0", "polyskull")]
namespace BetterSavesMod
{
    // Main Mod Class
    public class BetterSaves : MelonMod
    {
        public static readonly int MaxSaveSlots = 100;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("BetterSaves initialized.");
        }
    }

    [HarmonyPatch(typeof(LoadGameMenu), "UpdateSaveSlots")]
    public class LoadGameMenu_UpdateSaveSlotsPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_I4_S)
                {
                    codes[i].operand = BetterSaves.MaxSaveSlots;
                }
            }

            return codes.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(SaveGameMenu), "UpdateSaveSlots")]
    public class SaveGameMenu_UpdateSaveSlotsPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_I4_S)
                {
                    codes[i].operand = BetterSaves.MaxSaveSlots;
                }
            }

            return codes.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(SaveLoadManager), "FindSaveFiles")]
    public class SaveLoadManager_FindSaveFilesPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_I4_S)
                {
                    codes[i].operand = BetterSaves.MaxSaveSlots + 1;
                }
            }

            return codes.AsEnumerable();
        }
    }
}
