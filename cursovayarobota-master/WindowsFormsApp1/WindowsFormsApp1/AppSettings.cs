using System.Configuration;

namespace WindowsFormsApp1
{
	public static class AppSettings
	{
		public static string jsonDirPath = ConfigurationSettings.AppSettings["jsonDirPath"];
		public static string jsonFileName = ConfigurationSettings.AppSettings["filename"];
		public static string cardImgFormat = ConfigurationSettings.AppSettings["cardImgFormat"];
	}
}