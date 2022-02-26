using System;
using System.Linq;
using UnityEngine;

namespace Angus.Editor
{
    /// <summary>
    /// A model that describes the minimum values of a Unity Package.
    /// </summary>
    [Serializable]
    public class UnityPackage
    {
        [SerializeField] 
        private string name;
        
        [SerializeField] 
        private string version;

        /// <summary>
        /// Unique name of the package.
        /// </summary>
        public string Name => name;
        
        /// <summary>
        /// Version of the package.
        /// </summary>
        public string Version => version;
        
        public UnityPackage(){}

        public UnityPackage(string package)
        {
            if (package.Contains('@'))
            {
                string[] packageData = package.Split('@');
                name = packageData.First();
                version = packageData.Last();
            }
            else
            {
                name = package;
            }
        }
        
        /// <summary>
        /// Returns a string representing a package identifier ("name@version").
        /// </summary>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Version) ? Name : $"{Name}@{Version}";
        } 
    }
}