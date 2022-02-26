using System.Collections.Generic;

namespace Angus.Editor
{
    /// <summary>
    /// 
    /// </summary>
    public class PackageManagerUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageName"></param>
        public static void InstallPackage(string packageName)
        {
            Manifest manifest = Manifest.Load();
            UnityPackage unityPackage = new UnityPackage(packageName);
            manifest.AddPackage(unityPackage);

            manifest.Save();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityPackage"></param>
        public static void InstallPackage(UnityPackage unityPackage)
        {
            Manifest manifest = Manifest.Load();
            manifest.AddPackage(unityPackage);

            manifest.Save();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packages"></param>
        public static void InstallPackages(IEnumerable<string> packages)
        {
            Manifest manifest = Manifest.Load();

            foreach (string package in packages)
            {
                UnityPackage unityPackage = new UnityPackage(package);
                manifest.AddPackage(unityPackage);
            }

            manifest.Save();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packages"></param>
        public static void InstallPackages(IEnumerable<UnityPackage> packages)
        {
            Manifest manifest = Manifest.Load();

            foreach (UnityPackage unityPackage in packages)
            {
                manifest.AddPackage(unityPackage);
            }

            manifest.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageName"></param>
        public static void RemovePackage(string packageName)
        {
            Manifest manifest = Manifest.Load();
            UnityPackage unityPackage = new UnityPackage(packageName);
            manifest.RemovePackage(unityPackage);

            manifest.Save();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityPackage"></param>
        public static void RemovePackage(UnityPackage unityPackage)
        {
            Manifest manifest = Manifest.Load();
            manifest.RemovePackage(unityPackage);

            manifest.Save();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packages"></param>
        public static void RemovePackages(IEnumerable<string> packages)
        {
            Manifest manifest = Manifest.Load();

            foreach (string package in packages)
            {
                UnityPackage unityPackage = new UnityPackage(package);
                manifest.RemovePackage(unityPackage);
            }

            manifest.Save();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packages"></param>
        public static void RemovePackages(IEnumerable<UnityPackage> packages)
        {
            Manifest manifest = Manifest.Load();

            foreach (UnityPackage unityPackage in packages)
            {
                manifest.RemovePackage(unityPackage);
            }

            manifest.Save();
        }
    }
}