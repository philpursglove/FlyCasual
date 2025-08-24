using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class CloudServices : MonoBehaviour
{
    private const string environmentIdDev = "1630a476-212c-45b9-add1-924ac00d114b";
    private const string environmentIdProd = "76391cb2-067a-4633-9c0c-938aeec9d376";

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

    private async void Awake()
    {
        await InitializeUnityServices();
        await AuthenticateUnityServices();
        await InitializeRemoteConfigAsync();
    }

    async Task InitializeUnityServices()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            InitializationOptions options = new InitializationOptions().SetEnvironmentName(DebugManager.FullDebug ? "development" : "production");
            await UnityServices.InitializeAsync(options);
        }
    }

    async Task InitializeRemoteConfigAsync()
    {
        if (RemoteConfigService.Instance.requestStatus == ConfigRequestStatus.None)
        {
            RemoteConfigService.Instance.SetEnvironmentID(DebugManager.FullDebug ? environmentIdDev : environmentIdProd);
            await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());
        }
    }

    async Task AuthenticateUnityServices()
    {
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