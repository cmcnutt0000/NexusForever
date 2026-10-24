using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Spell.Effect.Data;

namespace NexusForever.Game.Spell.Effect
{
    public static class ServiceCollectionExtensions
    {
        public static void AddGameSpellEffect(this IServiceCollection sc)
        {
            sc.AddSingleton<IGlobalSpellEffectManager, GlobalSpellEffectManager>();

            sc.AddTransient<ISpellEffectAbsorptionData, SpellEffectAbsorptionData>();
            sc.AddTransient<ISpellEffectAchievementAdvanceData, SpellEffectAchievementAdvanceData>();
            sc.AddTransient<ISpellEffectActivateSpellCooldownData, SpellEffectActivateSpellCooldownData>();
            sc.AddTransient<ISpellEffectAddSpellData, SpellEffectAddSpellData>();
            sc.AddTransient<ISpellEffectCCStateBreakData, SpellEffectCCStateBreakData>();
            sc.AddTransient<ISpellEffectCCStateSetData, SpellEffectCCStateSetData>();
            sc.AddTransient<ISpellEffectChangePhaseData, SpellEffectChangePhaseData>();
            sc.AddTransient<ISpellEffectClampVitalData, SpellEffectClampVitalData>();
            sc.AddTransient<ISpellEffectDamageData, SpellEffectDamageData>();
            sc.AddTransient<ISpellEffectDefaultData, SpellEffectDefaultData>();
            sc.AddTransient<ISpellEffectDelayDeathData, SpellEffectDelayDeathData>();
            sc.AddTransient<ISpellEffectDisembarkData, SpellEffectDisembarkData>();
            sc.AddTransient<ISpellEffectDisguiseData, SpellEffectDisguiseData>();
            sc.AddTransient<ISpellEffectFactionSetData, SpellEffectFactionSetData>();
            sc.AddTransient<ISpellEffectForcedActionData, SpellEffectForcedActionData>();
            sc.AddTransient<ISpellEffectForcedMoveData, SpellEffectForcedMoveData>();
            sc.AddTransient<ISpellEffectGiveAbilityPointsToPlayerData, SpellEffectGiveAbilityPointsToPlayerData>();
            sc.AddTransient<ISpellEffectGiveAugmentPowerToPlayerData, SpellEffectGiveAugmentPowerToPlayerData>();
            sc.AddTransient<ISpellEffectGiveItemToPlayerData, SpellEffectGiveItemToPlayerData>();
            sc.AddTransient<ISpellEffectGiveSchematicData, SpellEffectGiveSchematicData>();
            sc.AddTransient<ISpellEffectGrantLevelScaledData, SpellEffectGrantLevelScaledData>();
            sc.AddTransient<ISpellEffectGrantXpData, SpellEffectGrantXpData>();
            sc.AddTransient<ISpellEffectLearnDyeColourData, SpellEffectLearnDyeColourData>();
            sc.AddTransient<ISpellEffectLootTableData, SpellEffectLootTableData>();
            sc.AddTransient<ISpellEffectModifyAbilityChargesData, SpellEffectModifyAbilityChargesData>();
            sc.AddTransient<ISpellEffectModifyInterruptArmourData, SpellEffectModifyInterruptArmourData>();
            sc.AddTransient<ISpellEffectModifyRestedXpData, SpellEffectModifyRestedXpData>();
            sc.AddTransient<ISpellEffectModifySpellCooldownData, SpellEffectModifySpellCooldownData>();
            sc.AddTransient<ISpellEffectNpcForceFacingData, SpellEffectNpcForceFacingData>();
            sc.AddTransient<ISpellEffectPathMissionIncrementData, SpellEffectPathMissionIncrementData>();
            sc.AddTransient<ISpellEffectPathXpModifyData, SpellEffectPathXpModifyData>();
            sc.AddTransient<ISpellEffectPersonalModifierData, SpellEffectPersonalModifierData>();
            sc.AddTransient<ISpellEffectPetCastSpellData, SpellEffectPetCastSpellData>();
            sc.AddTransient<ISpellEffectProcData, SpellEffectProcData>();
            sc.AddTransient<ISpellEffectProxyData, SpellEffectProxyData>();
            sc.AddTransient<ISpellEffectQuestAdvanceObjectiveData, SpellEffectQuestAdvanceObjectiveData>();
            sc.AddTransient<ISpellEffectRavelSignalData, SpellEffectRavelSignalData>();
            sc.AddTransient<ISpellEffectReputationModifyData, SpellEffectReputationModifyData>();
            sc.AddTransient<ISpellEffectRestedXpDecorBonusData, SpellEffectRestedXpDecorBonusData>();
            sc.AddTransient<ISpellEffectRewardBuffModifierData, SpellEffectRewardBuffModifierData>();
            sc.AddTransient<ISpellEffectRewardPropertyModifierData, SpellEffectRewardPropertyModifierData>();
            sc.AddTransient<ISpellEffectSapVitalData, SpellEffectSapVitalData>();
            sc.AddTransient<ISpellEffectSpellDispelData, SpellEffectSpellDispelData>();
            sc.AddTransient<ISpellEffectSpellEffectImmunityData, SpellEffectSpellEffectImmunityData>();
            sc.AddTransient<ISpellEffectSpellForceRemoveData, SpellEffectSpellForceRemoveData>();
            sc.AddTransient<ISpellEffectSpellImmunityData, SpellEffectSpellImmunityData>();
            sc.AddTransient<ISpellEffectSummonCreatureData, SpellEffectSummonCreatureData>();
            sc.AddTransient<ISpellEffectSummonMountData, SpellEffectSummonMountData>();
            sc.AddTransient<ISpellEffectSummonPetData, SpellEffectSummonPetData>();
            sc.AddTransient<ISpellEffectSummonTrapData, SpellEffectSummonTrapData>();
            sc.AddTransient<ISpellEffectSummonVanityPetData, SpellEffectSummonVanityPetData>();
            sc.AddTransient<ISpellEffectSummonVehicleData, SpellEffectSummonVehicleData>();
            sc.AddTransient<ISpellEffectTeleportData, SpellEffectTeleportData>();
            sc.AddTransient<ISpellEffectThreatModificationData, SpellEffectThreatModificationData>();
            sc.AddTransient<ISpellEffectTitleGrantData, SpellEffectTitleGrantData>();
            sc.AddTransient<ISpellEffectTitleRevokeData, SpellEffectTitleRevokeData>();
            sc.AddTransient<ISpellEffectTradeSkillProfessionData, SpellEffectTradeSkillProfessionData>();
            sc.AddTransient<ISpellEffectTransferenceData, SpellEffectTransferenceData>();
            sc.AddTransient<ISpellEffectUnitPropertyModifierData, SpellEffectUnitPropertyModifierData>();
            sc.AddTransient<ISpellEffectUnitStateSetData, SpellEffectUnitStateSetData>();
            sc.AddTransient<ISpellEffectUnlockInlaidAugmentData, SpellEffectUnlockInlaidAugmentData>();
            sc.AddTransient<ISpellEffectUnlockMountData, SpellEffectUnlockMountData>();
            sc.AddTransient<ISpellEffectUnlockPetFlairData, SpellEffectUnlockPetFlairData>();
            sc.AddTransient<ISpellEffectUnlockVanityPetData, SpellEffectUnlockVanityPetData>();
            sc.AddTransient<ISpellEffectVacuumLootData, SpellEffectVacuumLootData>();
            sc.AddTransient<ISpellEffectVendorPriceModifierData, SpellEffectVendorPriceModifierData>();
            sc.AddTransient<ISpellEffectVitalModifierData, SpellEffectVitalModifierData>();

            foreach (System.Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var attribute = type.GetCustomAttribute<SpellEffectHandlerAttribute>();
                if (attribute == null)
                    continue;

                foreach (System.Type interfaceType in type.GetInterfaces()
                    .Where(i => i.IsGenericType))
                {
                    if (interfaceType.GetGenericTypeDefinition() == typeof(ISpellEffectApplyHandler<>))
                        sc.AddKeyedTransient(interfaceType, attribute.SpellEffectType, type);
                    else if (interfaceType.GetGenericTypeDefinition() == typeof(ISpellEffectRemoveHandler<>))
                        sc.AddKeyedTransient(interfaceType, attribute.SpellEffectType, type);
                }
            }
        }
    }
}
