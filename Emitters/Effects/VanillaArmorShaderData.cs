using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using HamstarHelpers.Helpers.DotNET.Reflection;


namespace Emitters.Effects {
	internal class VanillaArmorShaderData : ShaderData {
		public static List<ArmorShaderData> GetShaderList() {
			return !ReflectionHelpers.Get( GameShaders.Armor, "_shaderData", out List<ArmorShaderData> list )
				? new List<ArmorShaderData>()
				: list;
		}

		public static Vector3 GetPrimaryColor( ArmorShaderData data ) {
			return !ReflectionHelpers.Get( data, "_uColor", out Vector3 primaryColor )
				? Vector3.One
				: primaryColor;
		}

		public static Vector3 GetSecondaryColor( ArmorShaderData data ) {
			return !ReflectionHelpers.Get( data, "_uSecondaryColor", out Vector3 secondaryColor )
				? Vector3.One
				: secondaryColor;
		}

		public static string GetPassName( ArmorShaderData data ) {
			return !ReflectionHelpers.Get( typeof( ShaderData ), data, "_passName", out string returnInfo )
				? null
				: returnInfo;
		}

		public static float GetSaturation( ArmorShaderData data ) {
			return !ReflectionHelpers.Get( typeof( ShaderData ), data, "_uSaturation", out float returnInfo )
				? 1f
				: returnInfo;
		}

		public static float GetOpacity( ArmorShaderData data ) {
			return !ReflectionHelpers.Get( typeof( ShaderData ), data, "_uOpacity", out float returnInfo )
				? 1f
				: returnInfo;
		}



		////////////////

		public Vector3 UColor = Vector3.One;

		public Vector3 USecondaryColor = Vector3.One;

		public float USaturation = 1f;

		public float UOpacity = 1f;



		////////////////

		public VanillaArmorShaderData( Ref<Effect> shader, string passName ) : base( shader, passName ) { }
	}
}
