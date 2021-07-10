// ReSharper disable CheckNamespace - all ScriptComponentBehaviour classes inside this namespace get picked up automatically by bannerlords engine
#define TRACE
namespace TaleWorlds.MountAndBlade
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using SeparatistCrisis.Util;
    using TaleWorlds.CampaignSystem;
    using TaleWorlds.Engine;
    using TaleWorlds.Library;
    using TaleWorlds.ObjectSystem;

    [SuppressMessage("ReSharper", "SA1401", Justification = "<Variables need to be exposed for Editor>")]
    public class PartyTeleportation : ScriptComponentBehaviour
    {
        // ReSharper disable MemberCanBePrivate.Global - needed to expose values inside the modding tools
        public float Radius = 1f;
        public bool ShowDebugInfo = true;
        public int NumVerts = 12;

        private Vec3 TargetPosition { get; set; } = Vec3.Zero;

        private Vec3 Position { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            SetScriptComponentToTick(GetTickRequirement());
            Setup();
        }

        protected override void OnEditorInit()
        {
            base.OnEditorInit();
            SetScriptComponentToTick(GetTickRequirement());
            Setup();
        }

        protected override TickRequirement GetTickRequirement()
        {
            return TickRequirement.Tick;
        }

        protected override void OnTick(float dt)
        {
            // check performance - we might need to optimize this part
            List<MobileParty> parties = new List<MobileParty>();
            MBObjectManager.Instance.GetAllInstancesOfObjectType(ref parties);

            foreach (MobileParty party in parties)
            {
                if (IsPartyInArea(party))
                {
                    party.Position2D = TargetPosition.AsVec2;
                    party.SetMoveModeHold();
                }
            }
        }

        protected override void OnEditorTick(float dt)
        {
            Setup();
            MBDebug.ClearRenderObjects();
            if (ShowDebugInfo)
            {
                Vec3 direction = TargetPosition - Position;
                MBDebug.RenderDebugLine(Position, direction, uint.MaxValue, false, dt);
                DebugRender.RenderCircle(Position, Radius, NumVerts, dt);
            }
        }

        private void Setup()
        {
            Position = GameEntity.GetFrame().origin;
            if (GameEntity.ChildCount > 0)
            {
                TargetPosition = GameEntity.GetChild(0).GlobalPosition;
            }
        }

        private bool IsPartyInArea(MobileParty party)
        {
            float distX = party.GetPosition2D.x - Position.x;
            float distY = party.GetPosition2D.y - Position.y;
            float distance = (distX * distX) + (distY * distY);

            return distance <= Radius * Radius;
        }
    }
}