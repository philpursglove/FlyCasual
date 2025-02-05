using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class CloudServices : MonoBehaviour
{
    public struct UserAttributes { }

    public struct AppAttributes
    {
        public string LatestVersion;
        public int LatestVersionInt;
        public string UpdateLink;
    }

   private void Start()
    {
        AnalyticsService.Instance.StartDataCollection();
    }

    private void OnDestroy()
    {
        AnalyticsService.Instance.StopDataCollection();
    }

    async void Awake()
    {
        // initialize Unity's authentication and core services
        await InitializeRemoteConfigAsync();


        // Fetch configuration settings from the remote service, they must be called with the attributes structs (empty or with custom attributes) to initiate the WebRequest.
        await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());
    }


    async Task InitializeRemoteConfigAsync()
    {
        InitializationOptions options = new();

        if (Application.isEditor)
        {
            options.SetEnvironmentName("development");
        }
        else
        {
            options.SetEnvironmentName("production");
        }

        // initialize handlers for unity game services
        await UnityServices.InitializeAsync(options);

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
}

namespace Analytics
{
    public class BattleStartedEvent : Unity.Services.Analytics.Event
    {
        public string Edition { set { SetParameter("Edition", Editions.Edition.Current.Name); } }
        public string GameMode { set { SetParameter("GameMode", value); } }

        public BattleStartedEvent() : base("battleStarted") { }        
    }

    public class BattleEndedEvent : Unity.Services.Analytics.Event
    {
        public BattleEndedEvent() : base("battleEnded") { }
    }

    public class GameErrorEvent : Unity.Services.Analytics.Event
    {
        public string Scene { set { SetParameter("Scene", value); } }
        public string Pilot { set { SetParameter("Pilot", value); } }
        public string Trigger { set { SetParameter("Trigger", value); } }
        public string Subphase { set { SetParameter("Subphase", value); } }

        public GameErrorEvent() : base("gameError") { }
    }
}