﻿using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.DotNET.Reflection;
using System;
using System.ComponentModel;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;


namespace Emitters.Terraria.ModLoader.Config {
	[TypeConverter( typeof( ToFromStringConverter<GoreDefinition> ) )]
	public class GoreDefinition : EntityDefinition {
		public static readonly Func<TagCompound, GoreDefinition> DESERIALIZER = Load;



		////////////////

		public static string GetUniqueKey( int type ) {
			if( type < GoreID.Count ) {
				return "Terraria " + NPCID.Search.GetName( type );
			}
			if( !ReflectionHelpers.Get( typeof(ModGore), null, "modGores", out ModGore[] modGores ) ) {
				throw new ModHelpersException( "Could not access modGores" );
			}
			if( type >= modGores.Length ) {   //GoreLoader.GoreCount
				throw new ArgumentOutOfRangeException( "Invalid type: " + type );
			}

			ModGore modGore = modGores[ type ]; //GoreLoader.GetGore( type );
			//return $"{modGore.mod.Name} {modGore.Name}";
			return modGore.GetType().Assembly.FullName+" "+modGore.GetType().Name;
		}


		////////////////

		public static GoreDefinition FromString( string s ) => new GoreDefinition( s );

		public static GoreDefinition Load( TagCompound tag ) => new GoreDefinition(
			tag.GetString( "mod" ),
			tag.GetString( "name" )
		);



		////////////////

		public override int Type => NPCID.TypeFromUniqueKey( mod, name );



		////////////////

		public GoreDefinition() : base() { }

		public GoreDefinition( int type ) : base( GoreDefinition.GetUniqueKey(type) ) { }   //GoreID.GetUniqueKey( type )

		public GoreDefinition( string key ) : base( key ) { }

		public GoreDefinition( string mod, string name ) : base( mod, name ) { }
	}
}
