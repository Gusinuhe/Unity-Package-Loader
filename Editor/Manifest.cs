using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace Angus.Editor
{
    /// <summary>
    /// A model that represents the project manifest.
    /// For more information, see https://docs.unity3d.com/Manual/upm-dependencies.html
    /// </summary>
    /// <remarks>
    /// Removing a dependency often leads to changing installed packages, but only after the Package Manager constructs a dependency graph to solve any version conflicts.
    /// For more information, see https://docs.unity3d.com/Manual/upm-manifestPrj.html.
    /// </remarks>
    [DataContract]
    public class Manifest
    {
        [DataMember]
        private ScopedRegistry[] scopedRegistries;

        [DataMember]
        private Dictionary<string, string> dependencies;

        /// <summary>
        /// Array of scopes that you can map to a package name, either as an exact match on the package name, or as a namespace.
        /// Wildcards and other glob patterns are not supported.
        /// </summary>
        /// <remarks>
        /// This type of configuration assumes that packages follow the Reverse domain name notation (https://en.wikipedia.org/wiki/Reverse_domain_name_notation).
        /// This ensures that com.unity is equivalent to any package name that matches the com.unity namespace, such as com.unity.timeline or com.unity.2d.animation.
        /// </remarks>
        public ScopedRegistry[] ScopedRegistries => scopedRegistries;
        
        /// <summary>
        /// Collection of packages required for your project. This includes only direct dependencies (indirect dependencies are listed in package manifests).
        /// Each entry maps the package name to the minimum version required for the project.
        /// </summary>
        public Dictionary<string, string> Dependencies => dependencies;

        /// <summary>
        /// Adds a dependency to the project.
        /// </summary>
        /// <remarks>
        /// Requesting a new or different dependency often leads to changing installed packages, but only after the Package Manager constructs a dependency graph to solve any version conflicts.
        /// </remarks>
        /// <param name="unityPackage">The <see cref="UnityPackage"/> containing the dependency to be removed from the project.</param>
        public void AddPackage(UnityPackage unityPackage)
        {
            if (!dependencies.ContainsKey(unityPackage.Name))
                dependencies.Add(unityPackage.Name, unityPackage.Version);
        }
        
        /// <summary>
        /// Removes a dependency from the project.
        /// </summary>
        /// <param name="unityPackage">The <see cref="UnityPackage"/> containing the dependency to be removed from the project.</param>
        public void RemovePackage(UnityPackage unityPackage)
        {
            if (dependencies.ContainsKey(unityPackage.Name))
                dependencies.Remove(unityPackage.Name);
        }

        /// <summary>
        /// Returns a lists of <see cref="UnityPackage"/> loaded in the Package Manager.
        /// </summary>
        public IEnumerable<UnityPackage> ListInstalledPackages()
        {
            return dependencies.Select(dependency => new UnityPackage($"{dependency.Key}@{dependency.Value}"));
        }

        /// <summary>
        /// Saves the content of this <see cref="Manifest"/> to disk.
        /// </summary>
        public void Save()
        {
            string packagesFolder = Path.Combine(Application.dataPath, "..", "Packages");

            if (!Directory.Exists(packagesFolder))
                Directory.CreateDirectory(packagesFolder);
            
            string manifestPath = Path.Combine(packagesFolder, "manifest.json");
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            
            File.WriteAllText(manifestPath, json);
        }
        
        /// <summary>
        /// Returns a <see cref="Manifest"/> object filled with the current dependencies loaded in the Package Manager.
        /// </summary>
        public static Manifest Load()
        {
            string manifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");
            
            if (!File.Exists(manifestPath))
                throw new FileLoadException($"Manifest not found at: {manifestPath}");
            
            string json = File.ReadAllText(manifestPath);
            Manifest manifest = JsonConvert.DeserializeObject<Manifest>(json);
            
            return manifest;
        }
    }
}