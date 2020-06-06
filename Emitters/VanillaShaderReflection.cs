using Microsoft.Xna.Framework;
using System.Collections.Generic;
using HamstarHelpers.Helpers.DotNET.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace Emitters
{
	internal class ArmorShaderReflection : ShaderData
	{
		public Vector3 UColor = Vector3.One;

		public Vector3 USecondaryColor = Vector3.One;

		public float USaturation = 1f;

		public float UOpacity = 1f;

		public ArmorShaderReflection(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}
	}

	static class VanillaShaderReflection
	{
		public static List<ArmorShaderData> GetShaderList(){
			
			var field = "_shaderData";
			return !ReflectionHelpers.Get(GameShaders.Armor, field, out List<ArmorShaderData> list) ? null : list;
		}
		public static Vector3 GetPrimaryColor(ArmorShaderData data){
			var field ="_uColor";
			return !ReflectionHelpers.Get(data, field, out Vector3 primaryColor) ? Vector3.One : primaryColor;
		}
		public static Vector3 GetSecondaryColor(ArmorShaderData data){
			var field = "_uSecondaryColor";
			return !ReflectionHelpers.Get(data, field, out Vector3 secondaryColor) ? Vector3.One : secondaryColor;
		}
		public static string GetPassName(ArmorShaderData data){
			var field = "_passName";
			return !ReflectionHelpers.Get(typeof(ShaderData), data, field, out string returnInfo) ? null : returnInfo;
		}
		public static float GetSaturation(ArmorShaderData data)
		{
			var field = "_uSaturation";
			return !ReflectionHelpers.Get(typeof(ShaderData), data, field, out float returnInfo) ? 1f : returnInfo;
		}
		public static float GetOpacity(ArmorShaderData data){
			var field = "_uOpacity";
			return !ReflectionHelpers.Get(typeof(ShaderData), data, field, out float returnInfo) ? 1f : returnInfo;
		}
	}
}
