namespace BL
{
	public class Util
	{
		public static string ConvertSecureToPlain(SecureString secureStr)
		{
			string ret = "";
			IntPtr pointer = IntPtr.Zero;
			try
			{
				char[] buffer = new char[secureStr.Length];
				pointer = Marshal.SecureStringToCoTaskMemUnicode(secureStr);
				Marshal.Copy(pointer, buffer, 0, buffer.Length);
				ret = new string(buffer);
			}
			finally
			{
				if(pointer != IntPtr.Zero)
				{
					// free
					Marshal.ZeroFreeCoTaskMemUnicode(pointer);
				}
			}
			return ret;
		}
	}
}
