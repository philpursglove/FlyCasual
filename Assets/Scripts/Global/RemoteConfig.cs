using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class RemoteConfig : MonoBehaviour
{
    public struct userAttributes { }

    public struct appAttributes
    {
        public string LatestVersion;
        public int LatestVersionInt;
        public string UpdateLink;
    }

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async void Awake()
    {
        // initialize Unity's authentication and core services
        await InitializeRemoteConfigAsync();

        // Fetch configuration settings from the remote service, they must be called with the attributes structs (empty or with custom attributes) to initiate the WebRequest.
        await RemoteConfigService.Instance.FetchConfigsAsync(new userAttributes(), new appAttributes());
    }
}