#define TRACE
namespace SeparatistCrisis.Util
{
    using TaleWorlds.Engine;
    using TaleWorlds.Library;

    public static class DebugRender
    {
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="pos">Circle center.</param>
        /// <param name="radius">Radius.</param>
        /// <param name="numVerts">How many vertices. More verts result in a more round circle.</param>
        /// <param name="deltaTime">How long it last until it gets deleted. Best to use engine tick time since then the
        /// circle gets redrawn every frame and can be updated if any changes are made.</param>
        public static void RenderCircle(Vec3 pos, float radius, int numVerts, float deltaTime)
        {
            Vec3[] positionData = GetCircleVertices(numVerts, pos, radius);
            for (int i = 0; i < numVerts; i++)
            {
                // jump over first vert
                MBDebug.RenderDebugLine(positionData[i], positionData[(i + 1) % numVerts] - positionData[i], uint.MaxValue, false, deltaTime);
            }
        }

        private static Vec3[] GetCircleVertices(int numVerts, Vec3 startPos, float radius)
        {
            Vec3[] positionData = new Vec3[numVerts];
            float delta = 2f * MathF.PI / numVerts;

            for (int i = 0; i < numVerts; i++)
            {
                // circle math, don't worry about it
                float alpha = i * delta;
                float x = MathF.Cos(alpha);
                float y = MathF.Sin(alpha);
                Vec3 point = new Vec3(x, y);
                point = startPos + (radius * point);

                positionData[i] = point;
            }

            return positionData;
        }
    }
}