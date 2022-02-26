using System.Runtime.Serialization;

namespace Angus.Editor
{
    /// <summary>
    /// Specify custom registries in addition to the default registry. This allows you to host your own packages.
    /// </summary>
    /// <remarks>
    /// For more information, see https://docs.unity3d.com/Manual/upm-scoped.html.
    /// </remarks>
    [DataContract]
    public class ScopedRegistry
    {
        [DataMember]
        private string name;
        
        [DataMember]
        private string url;
        
        [DataMember]
        private string[] scopes;

        /// <summary>
        /// The scope name as it appears in the user interface. The Package Manager window displays this name in the package details view.
        /// </summary>
        public string Name => name;
        
        /// <summary>
        /// The URL to the npm-compatible registry server.
        /// </summary>
        /// <remarks>
        /// Not all registry providers are compatible with Unity’s Package Manager.
        /// Make sure the package registry server you are trying to add implements the /-/v1/search or /-/all endpoints.
        /// </remarks>
        public string URL => url;
        
        /// <summary>
        /// Array of scopes that you can map to a package name, either as an exact match on the package name, or as a namespace.
        /// Wildcards and other glob patterns are not supported.
        /// </summary>
        /// <remarks>
        /// This type of configuration assumes that packages follow the Reverse domain name notation.
        /// This ensures that com.unity is equivalent to any package name that matches the com.unity namespace, such as com.unity.timeline or com.unity.2d.animation.
        /// </remarks>
        public string[] Scopes => scopes;
    }
}