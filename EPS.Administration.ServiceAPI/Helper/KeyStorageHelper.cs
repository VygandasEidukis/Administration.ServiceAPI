using Microsoft.Win32;

namespace EPS.Administration.ServiceAPI.Helper
{
    public static class KeyStorageHelper
    {
        public static string GetKey(string storage)
        {
            if (string.IsNullOrEmpty(storage))
            {
                return null;
            }

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE");
            key = key.OpenSubKey(storage);

            if (key == null)
            {
                return null;
            }

            return key.GetValue("ServiceKey").ToString();
        }
    }
}
