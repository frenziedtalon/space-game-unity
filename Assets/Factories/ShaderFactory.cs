using System;
using UnityEngine;

namespace Assets.Factories
{
    internal class ShaderFactory
    {
        public static Shader Create(ShaderType type)
        {
            switch (type)
            {
                case ShaderType.URP_Lit:
                    return Shader.Find("Universal Render Pipeline/Lit");
                default:
                    throw new ArgumentException(nameof(type));
            }
        }
    }

    public enum ShaderType
    {
        URP_Lit,
    }
}
