using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Define
{
	static public class EngineErrorName
	{
		public static string AudioManager_FMODError() { return "FMOD Error"; }

		public static string AudioManager_FMODVersionError() { return "FMOD Version Error"; }

		public static string Resource_CannotChangeFilename() { return "Cannot change filename"; }
		public static string Resource_CannotGetResource() { return "Cannot get resource"; }
		public static string Resource_CannotReload() { return "Cannot reload"; }
		public static string Resource_CannotLoad() { return "Cannot load"; }
		public static string Resource_CannotUnload() { return "Cannot unload"; }
		public static string Resource_LoadError() { return "Load error"; }
		public static string AudioManager_ChannelNotEnough() { return "Too Much Audio File"; }
		public static string Resource_CannotSet() { return "Cannot Edit"; }
		public static string ThreadManager_TooManyThread() { return "Too Many Threads"; }
		public static string TextureManager_TooManyTexture() { return "Too Many Textures"; }
		public static string TextureManager_NullManagedTexture() { return "Managed Texture NULL"; }
		public static string TextureManager_RepeatTexture() { return "Already Have the same Texture"; }
		public static string TextureManager_RemoveWithoutAdd() { return "Remove an Texture which hasn't been added to Managed Texture List"; }
		public static string Resource_CannotChangeData() { return "Cannot Change"; }
	}
}
