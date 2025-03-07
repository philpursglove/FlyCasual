using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Core;
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

    async void Awake()
    {
        await InitializeUnityServices();
        await InitializeRemoteConfigAsync();
    }

    async Task InitializeUnityServices()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            await UnityServices.InitializeAsync();
        }
    }

    async Task InitializeRemoteConfigAsync()
    {
        if (RemoteConfigService.Instance.requestStatus == ConfigRequestStatus.None)
        {
            await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());
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