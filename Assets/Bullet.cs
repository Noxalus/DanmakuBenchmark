using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace Danmaku
{
    //
    // Simple linear motion bullet structure
    //
    struct Bullet
    {
        public float2 Position { get; private set; }
        public float2 Direction => math.float2(math.cos(Rotation), math.sin(Rotation));
        public float Speed { get; private set; }
        public float Rotation { get; private set; }
        public int BulletTypeIndex { get; private set; }

        public Bullet(float2 position, float rotation, float speed, int bulletTypeIndex = 0)
        {
            Position = position;
            Rotation = rotation;
            Speed = speed;
            BulletTypeIndex = bulletTypeIndex;
        }

        public static Bullet Spawn(float2 position, int seed, int bulletTypeIndex = 0)
        {
            var hash = new Klak.Math.XXHash((uint)seed);
            var rotation = hash.Float(math.PI * 2, 0u);
            var speed = hash.Float(0.1f, 1f, 1u);

            return new Bullet(position, rotation, speed, bulletTypeIndex);
        }

        public Bullet NextFrame(float delta)
        {
            var newPosition = Position + Direction * Speed * delta;
            var newSpeed = Speed > 0 ? Speed - 1f * delta : 0f;

            Position = newPosition;
            Speed = newSpeed;
            Rotation = Rotation;

            return this;
        }
    }

    //
    // Bullet group shared information structure
    //
    readonly struct BulletGroupInfo
    {
        public int ActiveCount { get; }
        public int SpawnCount { get; }

        public BulletGroupInfo(int activeCount, int spawnCount)
        {
            ActiveCount = activeCount;
            SpawnCount = spawnCount;
        }

        public static BulletGroupInfo InitialState()
        {
            return new BulletGroupInfo(0, 0);
        }

        public static BulletGroupInfo ChangeActiveCount(in BulletGroupInfo orig, int count)
        {
            return new BulletGroupInfo(count, orig.SpawnCount);
        }

        public static BulletGroupInfo AddActiveAndSpawnCount(in BulletGroupInfo orig, int add)
        {
            return new BulletGroupInfo(orig.ActiveCount + add, orig.SpawnCount + add);
        }
    }

} // namespace Danmaku
