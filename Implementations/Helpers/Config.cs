using System.Text.RegularExpressions;

namespace OnlineCourses.Implementations.Helpers
{
	public class Config
	{
		public static string ConnectionString { get; set; }
		public static Regex cyrillicPattern = new Regex(@"^[А-Яа-яЁё\s0-9.,!?;:\-\(\)]*$");
		public static Regex latinNumericPattern = new Regex(@"^[a-zA-Z0-9]*$");

		public static Regex EMAIL_REGEXP = new Regex(@"/^(([^<>()[\].,;:\s@""]+(\.[^<>()[\].,;:\s@""]+)*)|("".+""))@(([^<>()[\].,;:\s@""]+\.)+[^<>()[\].,;:\s@""]{2,})$/iu");
		public static Regex PASSWORD_REGEXP = new Regex(@"/^(?=.*\d)(?=.*[a-z])(?=.*\W).{6,}$/");
	}
}
