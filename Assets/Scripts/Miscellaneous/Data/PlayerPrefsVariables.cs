using UnityEngine;

namespace Miscellaneous.Data
{
    public static class StaticPrefs
    {
        public static float CameraSize
        {
            get { return PlayerPrefs.GetFloat("CameraSize", 12f); }
            set { PlayerPrefs.SetFloat("CameraSize", value); }
        }
    }
}