using System;
using UnityEngine;

namespace Shared
{
    public abstract class SharedTools
    {
        public const string Mark = "mark";
        public const string Send = "send";
        public const string Disable = "disable";
        public const string Enable = "enable";
        public const string Coral = "coral";
        public const string White = "blank";
        
        public static Color ChangeColor(string code)
        {
            return code switch
            {
                Enable => new Color32(0, 0, 0, 255),
                Mark => new Color32(17, 170, 0, 255),
                Coral => new Color32(238, 85, 32, 255),
                Disable => new Color32(150, 150, 150, 255),
                _ => throw new ArgumentOutOfRangeException(nameof(code), code, "No existe ese color.")
            };
        }
    }
}