using HarmonyLib;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SeparatistCrisis.Patches
{
    [HarmonyPatch]
    public class ShieldHitPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Mission), "RegisterBlow")]
        private static bool RegisterBlowPatch(Mission __instance, Agent attacker, Agent victim,
            GameEntity realHitEntity, Blow b,
            ref AttackCollisionData collisionData, in MissionWeapon attackerWeapon)
        {
            b.VictimBodyPart = collisionData.VictimHitBodyPart;
            if (collisionData.AttackBlockedWithShield || collisionData.CollidedWithShieldOnBack)
            {
                foreach (MissionBehavior missionBehavior in __instance.MissionBehaviors)
                    missionBehavior.OnRegisterBlow(attacker, victim, realHitEntity, b, ref collisionData,
                        in attackerWeapon);

                return false;
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Mission), "DecideAgentHitParticles")]
        private static bool DecideAgentHitParticlesPatch(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData, ref HitParticleResultData hprd)
        {
            if (collisionData.CollidedWithShieldOnBack)
            {
                MissionGameModels.Current.DamageParticleModel.GetMeleeAttackSweatParticles(attacker, victim, in blow, in collisionData, out hprd);
                return false;
            }

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Mission), "MissileHitCallback")]
        private static void MissileHitCallbackPatch(ref int extraHitParticleIndex,
            ref AttackCollisionData collisionData)
        {
            if (collisionData.CollidedWithShieldOnBack)
            {
                extraHitParticleIndex = -1;
            }
        }
    }
}