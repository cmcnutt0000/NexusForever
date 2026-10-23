using System.Numerics;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Static.Spell;
using NexusForever.GameTable.Model;
using NexusForever.Shared;
using NLog;

namespace NexusForever.Game.Spell
{
    public class Telegraph : ITelegraph
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public IUnitEntity Caster { get; }
        public Vector3 Position { get; private set; }
        public Vector3 Rotation { get; private set; }
        public TelegraphDamageEntry TelegraphDamage { get; }
        public TelegraphTargetTypeFlags TelegraphTargetTypeFlags => (TelegraphTargetTypeFlags)TelegraphDamage.TargetTypeFlags;

        public Telegraph(TelegraphDamageEntry telegraphDamageEntry, IUnitEntity caster, Vector3 position, Vector3 rotation)
        {
            TelegraphDamage = telegraphDamageEntry;
            Caster          = caster;
            Position        = position;
            Rotation        = rotation;

            ApplyPositionOffsets();
            ApplyRotationOffsets();
        }

        private void ApplyPositionOffsets()
        {
            if (TelegraphDamage.ZPositionOffset == 0u
                && TelegraphDamage.XPositionOffset == 0u
                && TelegraphDamage.YPositionOffset == 0u)
                return;

            Vector3 startingPosition = Position;
            if (TelegraphDamage.ZPositionOffset != 0u)
                startingPosition = Position.GetPointForTelegraph(Rotation.X + MathF.PI / 2, TelegraphDamage.ZPositionOffset);

            startingPosition.Y += TelegraphDamage.YPositionOffset;

            Position = startingPosition.GetPointForTelegraph(Rotation.X + MathF.PI, TelegraphDamage.XPositionOffset);
        }

        private void ApplyRotationOffsets()
        {
            if (TelegraphDamage.RotationDegrees == 0u)
                return;

            float rotationRadians = Rotation.X + TelegraphDamage.RotationDegrees.ToRadians();

            if (rotationRadians > MathF.PI)
                rotationRadians -= 2 * MathF.PI;
            else if (rotationRadians <= -MathF.PI)
                rotationRadians += 2 * MathF.PI;

            Rotation = new Vector3(rotationRadians, Rotation.Y, Rotation.Z);
        }

        /// <summary>
        /// Returns whether the supplied <see cref="Vector3"/> is inside the telegraph.
        /// </summary>
        /// <remarks>
        /// The client has no hit test of its own. It paints each telegraph with a pixel shader (TelegraphPS) that tests against the shape built by 0x14054D170:
        /// an ellipsoid of outer radii, a box of half extents and a sector around local -Z, minus a vertical cylinder of inner radius.
        /// Every parameter is grown by the viewing player's hit radius unless flag 0x200 is set (0x14054DA10), so the drawn edge is where the player's centre
        /// has to be to get hit. This tests the target's centre against the same shape grown by the target's hit radius.
        /// </remarks>
        public bool InsideTelegraph(Vector3 position, float hitRadius)
        {
            float radius = (TelegraphDamage.TelegraphDamageFlags & 0x200u) != 0u ? 0f : hitRadius;

            // the pie is drawn turned by half a circle so its missing slice is in front, see 0x14054D7A8
            float yaw = Rotation.X;
            if ((DamageShape)TelegraphDamage.DamageShapeEnum == DamageShape.Pie)
                yaw += MathF.PI;

            // world to the telegraph's frame, forward is local -Z (world -sin, -cos), see 0x14054B1B0
            Vector3 delta = position - Position;
            var local = new Vector3(
                delta.X * MathF.Cos(yaw) - delta.Z * MathF.Sin(yaw),
                delta.Y,
                delta.X * MathF.Sin(yaw) + delta.Z * MathF.Cos(yaw));

            switch ((DamageShape)TelegraphDamage.DamageShapeEnum)
            {
                case DamageShape.Cross:
                    return IsInsideShape(local, BuildShape(radius, 0)) || IsInsideShape(local, BuildShape(radius, 1));
                case DamageShape.Circle:
                case DamageShape.Ring:
                case DamageShape.Square:
                case DamageShape.Cone:
                case DamageShape.Pie:
                case DamageShape.Ellipse:
                case DamageShape.Rectangle:
                case DamageShape.LongCone:
                    return IsInsideShape(local, BuildShape(radius, 0));
                default:
                    log.Warn($"Unhandled telegraph shape {(DamageShape)TelegraphDamage.DamageShapeEnum}.");
                    return false;
            }
        }

        private readonly record struct TelegraphShape(float InnerRadius, Vector3 OuterRadius, Vector3 HalfExtents, float Arc, float OffsetZ);

        /// <summary>
        /// Build the shape the client draws for the telegraph, see 0x14054D170 in the client.
        /// </summary>
        /// <remarks>
        /// Angles are full angles in degrees. Square, rectangle and cross sizes are full sizes, centred except for the rectangle which starts at the telegraph and extends forward.
        /// </remarks>
        private TelegraphShape BuildShape(float radius, int part)
        {
            float Param(int index, float scale)
            {
                float value = index switch
                {
                    0 => TelegraphDamage.Param00,
                    1 => TelegraphDamage.Param01,
                    2 => TelegraphDamage.Param02,
                    3 => TelegraphDamage.Param03,
                    4 => TelegraphDamage.Param04,
                    _ => TelegraphDamage.Param05
                };
                return value + scale * radius;
            }

            const float fullCircle = MathF.PI * 2f;

            static Vector3 Uniform(float value) => new(value, value, value);
            static TelegraphShape Box(Vector3 halfExtents, float offsetZ = 0f)
                => new(0f, Uniform(halfExtents.Length()), halfExtents, fullCircle, offsetZ);

            switch ((DamageShape)TelegraphDamage.DamageShapeEnum)
            {
                case DamageShape.Circle:
                {
                    float outer = Param(0, 1f);
                    return new(0f, Uniform(outer), Uniform(outer), fullCircle, 0f);
                }
                case DamageShape.Ring:
                {
                    float outer = Param(1, 1f);
                    return new(MathF.Max(Param(0, -1f), 0f), Uniform(outer), Uniform(outer), fullCircle, 0f);
                }
                case DamageShape.Square:
                    return Box(new Vector3(Param(0, 2f), Param(1, 2f), Param(2, 2f)) * 0.5f);
                case DamageShape.Cross:
                {
                    // two boxes, the second turned by a quarter circle
                    return part == 0
                        ? Box(new Vector3(Param(0, 2f), Param(1, 2f), Param(2, 2f)) * 0.5f)
                        : Box(new Vector3(Param(2, 2f), Param(1, 2f), Param(0, 2f)) * 0.5f);
                }
                case DamageShape.Cone:
                {
                    float arc   = Math.Clamp(TelegraphDamage.Param02.ToRadians(), 0f, fullCircle);
                    float inner = MathF.Max(Param(0, -1f), 0f);
                    float outer = MathF.Max(Param(1, 1f), inner);

                    if (radius > 0f)
                    {
                        // widen the arc so its straight edges clear the radius, at most 7 degrees a side
                        float length = MathF.Max(outer - radius, inner + radius);
                        float half   = MathF.Max(arc < MathF.PI ? arc * 0.5f : MathF.PI - arc * 0.5f, 0.01f);
                        float grow   = MathF.Atan((MathF.Tan(half) * length + 2f * radius) / MathF.Max(1f, length + radius)) - half;
                        arc += 2f * MathF.Min(grow, 7f.ToRadians());
                    }

                    return new(inner, Uniform(outer), Uniform(outer), arc, 0f);
                }
                case DamageShape.Pie:
                {
                    float arc   = Math.Clamp(fullCircle - TelegraphDamage.Param02.ToRadians(), 0f, fullCircle);
                    float outer = Param(1, 1f);
                    return new(MathF.Max(Param(0, -1f), 0f), Uniform(outer), Uniform(outer), arc, 0f);
                }
                case DamageShape.Ellipse:
                {
                    var outer = new Vector3(Param(0, 1f), Param(1, 1f), Param(2, 1f));
                    return new(0f, outer, outer, fullCircle, 0f);
                }
                case DamageShape.Rectangle:
                {
                    // the length is only grown in front, the box starts at the telegraph
                    Vector3 halfExtents = new Vector3(Param(0, 2f), Param(1, 2f), Param(2, 1f)) * 0.5f;
                    return Box(halfExtents, -halfExtents.Z);
                }
                case DamageShape.LongCone:
                {
                    // a cone clipped by a centred box of the last three parameters, each only when set
                    float arc   = Math.Clamp(TelegraphDamage.Param02.ToRadians(), 0f, fullCircle);
                    float inner = MathF.Max(Param(0, -1f), 0f);
                    float outer = MathF.Max(Param(1, 1f), inner);
                    var halfExtents = new Vector3(
                        TelegraphDamage.Param03 > 1e-5f ? Param(3, 2f) * 0.5f : outer,
                        TelegraphDamage.Param04 > 1e-5f ? Param(4, 2f) * 0.5f : outer,
                        TelegraphDamage.Param05 > 1e-5f ? Param(5, 2f) * 0.5f : outer);
                    return new(inner, Uniform(outer), halfExtents, arc, 0f);
                }
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Returns whether the point, in the telegraph's frame, passes the tests of the client's telegraph shader.
        /// </summary>
        private static bool IsInsideShape(Vector3 local, TelegraphShape shape)
        {
            const float epsilon = 1e-5f;

            Vector3 p = local with { Z = local.Z - shape.OffsetZ };
            Vector3 outer = shape.OuterRadius;
            if (outer.X <= epsilon || outer.Y <= epsilon || outer.Z <= epsilon)
                return false;

            if (p.X * p.X / (outer.X * outer.X) + p.Y * p.Y / (outer.Y * outer.Y) + p.Z * p.Z / (outer.Z * outer.Z) >= 1f)
                return false;

            // the inner radius is a vertical cylinder
            if (shape.InnerRadius > epsilon && (p.X * p.X + p.Z * p.Z) / (shape.InnerRadius * shape.InnerRadius) < 1f)
                return false;

            if (MathF.Abs(p.X) > shape.HalfExtents.X || MathF.Abs(p.Y) > shape.HalfExtents.Y || MathF.Abs(p.Z) > shape.HalfExtents.Z)
                return false;

            float horizontal = MathF.Sqrt(p.X * p.X + p.Z * p.Z);
            if (horizontal > 0f && -p.Z / horizontal < MathF.Cos(shape.Arc * 0.5f) - epsilon)
                return false;

            return true;
        }
    }
}
