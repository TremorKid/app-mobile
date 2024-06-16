using UnityEngine;

namespace Shared
{
    public abstract class SharedTools
    {
        public static Color ChangeColor(string code)
        {
            return code switch
            {
                "mark" => new Color32(17, 170, 0, 255),
                "send" => new Color32(10, 115, 0, 255),
                "disable" => new Color32(150, 150, 150, 255),
                _ => new Color32(255, 255, 255, 255)
            };
        }
    }
}