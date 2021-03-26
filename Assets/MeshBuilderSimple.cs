using UnityEngine;
using UnityEngine.Rendering;
using Unity.Collections;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Danmaku
{
    //
    // Danmaku mesh builder with the simple mesh API
    //
    static class MeshBuilderSimple
    {
        const int BULLETS_PER_ROW = 4;

        static bool _allocated;
        static List<Vector3> _vertices;
        static List<Vector2> _uvs;
        static List<int> _indices;

        public static void Build(NativeSlice<Bullet> bullets, float size, Mesh mesh)
        {
            if (!_allocated)
            {
                // Buffer allocation
                _vertices = new List<Vector3>();
                _uvs = new List<Vector2>();
                _indices = new List<int>();
                _allocated = true;
            }

            // Position
            _vertices.Clear();

            for (var i = 0; i < bullets.Length; i++)
            {
                var position = bullets[i].Position;
                _vertices.Add(new Vector3(position.x - size, position.y - size, 0));
                _vertices.Add(new Vector3(position.x - size, position.y + size, 0));
                _vertices.Add(new Vector3(position.x + size, position.y + size, 0));
                _vertices.Add(new Vector3(position.x + size, position.y - size, 0));
            }

            // UV
            _uvs.Clear();

            var uv0 = new Vector2(0, 0);
            var uv1 = new Vector2(0, 1);
            var uv2 = new Vector2(1, 1);
            var uv3 = new Vector2(1, 0);

            for (var i = 0; i < bullets.Length; i++)
            {
                var bulletTypeIndex = bullets[i].BulletTypeIndex;
                var uvOffset = new Vector2(
                    (float)(bulletTypeIndex % BULLETS_PER_ROW),
                    (BULLETS_PER_ROW - 1) - (int)(bulletTypeIndex / BULLETS_PER_ROW)
                );

                _uvs.Add(uv0 + uvOffset);
                _uvs.Add(uv1 + uvOffset);
                _uvs.Add(uv2 + uvOffset);
                _uvs.Add(uv3 + uvOffset);
            }

            // Index
            _indices.Clear();

            for (var i = 0; i < bullets.Length * 4; i++)
            {
                _indices.Add(i);
            }

            // Mesh construction
            mesh.Clear();
            mesh.SetVertices(_vertices);
            mesh.SetUVs(0, _uvs);
            mesh.SetIndices(_indices, MeshTopology.Quads, 0);
        }
    }

} // namespace Danmaku
