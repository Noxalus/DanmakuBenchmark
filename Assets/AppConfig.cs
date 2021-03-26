using UnityEngine;

namespace Danmaku
{
    sealed class AppConfig : MonoBehaviour
    {
        void Start() => Application.targetFrameRate = 120;
    }

} // namespace Danmaku
