using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Console : MonoBehaviour {

    private static bool isAlreadyInitialized;
    private static float totalLogEntryHeight;
    private static GameObject ConsoleOutput;
    private static GameObject ConsoleWindow;
    private static readonly int LOG_ENTRY_MARGIN = 10;

    public static bool IsActive
    {
        get { return ConsoleWindow.activeSelf; }
        set { ConsoleWindow.SetActive(value); }
    }

    public class LogEntry
    {
        public string Text;
        public float CalculatedPrefferedHeight;

        public LogEntry(string text)
        {
            Text = text;
        }
    }

    private static List<LogEntry> logs;
    public static List<LogEntry> Logs
    {
        get { return logs; }
        private set { logs = value; }
    }

    private static Dictionary<string, GenericCommand> availableCommands;
    public static Dictionary<string, GenericCommand> AvailableCommands
    {
        get { return availableCommands; }
        private set { availableCommands = value; }
    }


    private void Start()
    {
        Application.logMessageReceived += ProcessUnityLog;

        InitializeCommands();
    }

    private void InitializeCommands()
    {
        AvailableCommands = new Dictionary<string, GenericCommand>();

        List<Type> typelist = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => String.Equals(t.Namespace, "CommandsList", StringComparison.Ordinal))
            .ToList();

        foreach (var type in typelist)
        {
            if (type.MemberType == MemberTypes.NestedType) continue;
            System.Activator.CreateInstance(type);
        }

        AvailableCommands = AvailableCommands.OrderBy(n => n.Key).ToDictionary(n => n.Key, n => n.Value);
    }

    private static void InitializeLogs()
    {
        Logs = new List<LogEntry>();
    }

    public static void Write(string text, bool isBold = false, string color = "")
    {
        if (Logs == null) InitializeLogs();

        string logString = text;
        if (isBold) logString = "<b>" + logString + "</b>";
        if (color != "") logString = "<color="+ color + ">" + logString + "</color>";

        LogEntry logEntry = new LogEntry(logString + "\n");
        Logs.Add(logEntry);

        ShowLogEntry(logEntry);
    }

    private void ProcessUnityLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            if (IsHiddenError(logString)) return;

            if (!DebugManager.ErrorIsAlreadyReported)
            {
                if (DebugManager.ReleaseVersion && Global.CurrentVersionInt == Global.LatestVersionInt)
                {
                    SendReport(stackTrace);
                }
            }

            if (ErrorReporter.Instance != null)
            {
                ErrorReporter.ShowError(logString + "\n\n" + stackTrace);
            }
            else
            {
                IsActive = true;
            }

            Write("\n" + logString + "\n\n" + stackTrace, true, "red");
        }
    }

    private void SendReport(string stackTrace)
    {
        DebugManager.ErrorIsAlreadyReported = true;

        AnalyticsEvent.LevelFail(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            new Dictionary<string, object>()
            {
                { "Version", Global.CurrentVersion },
                { "Pilot", (Selection.ThisShip != null) ? Selection.ThisShip.PilotInfo.PilotName : "None" },
                { "Trigger", (Triggers.CurrentTrigger != null) ? Triggers.CurrentTrigger.Name : "None" },
                { "Subphase", (Phases.CurrentSubPhase != null) ? Phases.CurrentSubPhase.GetType().ToString() : "None" }
            }
        );

        StartCoroutine(UploadCustomReport(stackTrace));
    }

    IEnumerator UploadCustomReport(string stackTrace)
    {
        JSONObject jsonData = new JSONObject();
        jsonData.AddField("rowKey", Guid.NewGuid().ToString());
        jsonData.AddField("partitionKey", "CrashReport");
        jsonData.AddField("playerName", Options.NickName);
        jsonData.AddField("description", "No description");
        jsonData.AddField("p1pilot", (Selection.ThisShip != null) ? Selection.ThisShip.PilotInfo.PilotName : "None");
        jsonData.AddField("p2pilot", (Selection.AnotherShip != null) ? Selection.AnotherShip.PilotInfo.PilotName : "None");
        jsonData.AddField("stackTrace", stackTrace.Replace("\n", "NEWLINE"));
        jsonData.AddField("trigger", (Triggers.CurrentTrigger != null) ? Triggers.CurrentTrigger.Name : "None");
        jsonData.AddField("subphase", (Phases.CurrentSubPhase != null) ? Phases.CurrentSubPhase.GetType().ToString() : "None");
        jsonData.AddField("scene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        jsonData.AddField("version", Global.CurrentVersion);

        try
        {
            jsonData.AddField("p1squad", Global.SquadBuilder.SquadLists[Players.PlayerNo.Player1].SavedConfiguration.ToString().Replace("\"", "\\\""));
            jsonData.AddField("p2squad", Global.SquadBuilder.SquadLists[Players.PlayerNo.Player2].SavedConfiguration.ToString().Replace("\"", "\\\""));
        }
        catch (Exception)
        {
            jsonData.AddField("p1squad", "None");
            jsonData.AddField("p2squad", "None");
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Battle")
        {
            jsonData.AddField("replay", ReplaysManager.GetReplayContent().Replace("\"", "\\\""));
        }
        else
        {
            jsonData.AddField("replay", "None");
        }        

        var request = new UnityWebRequest("https://flycasualdataserver.azurewebsites.net/api/crashreports/create", "POST");
        Debug.Log(jsonData.ToString());
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
    }

    private bool IsHiddenError(string text)
    {
        if ((text == "ClientDisconnected due to error: Timeout") ||
            (text == "ServerDisconnected due to error: Timeout") ||
            text.StartsWith("Screen position out of view frustum")) return true;

        if (text == "SerializedObject target has been destroyed.") return true;

        if (text == "Material doesn't have a color property '_Color'") return true;

        return false;
    }

    public static void ProcessCommand(string inputText)
    {
        if (string.IsNullOrEmpty(inputText)) return;

        List<string> blocks = inputText.Split(' ').ToList();
        string keyword = blocks.FirstOrDefault();
        blocks.RemoveAt(0);

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        foreach (var item in blocks)
        {
            string[] paramValue = item.Split(':');
            if (paramValue.Length == 2) parameters.Add(paramValue[0], paramValue[1]);
            else if (paramValue.Length == 1) parameters.Add(paramValue[0], null);
        }

        if (AvailableCommands.ContainsKey(keyword))
        {
            AvailableCommands[keyword].Execute(parameters);
        }
        else
        {
            Console.Write("Unknown command, enter \"help\" to see list of commands", color: "red");
        }
    }

    public static void AddAvailableCommand(GenericCommand command)
    {
        AvailableCommands.Add(command.Keyword, command);
    }
    public void Awake()
    {
        if (!isAlreadyInitialized)
        {
            isAlreadyInitialized = true;
            ConsoleWindow = transform.Find("ConsoleWindow").gameObject;
            ConsoleOutput = transform.Find("ConsoleWindow").Find("ScrollRect").Find("Viewport").Find("Output").gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        ProcessKeys();
    }

    private void ProcessKeys()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.BackQuote))
        {
            ToggleConsole();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsActive) Show();
        }
    }

    public static void ToggleConsole()
    {
        if (Logs == null) InitializeLogs();
        IsActive = !IsActive;
        if (IsActive)
        {
            ConsoleWindow.GetComponentInChildren<InputField>().Select();
        }
    }

    private static void ShowLogEntry(LogEntry logEntry)
    {
        GameObject logEntryPrefab = (GameObject)Resources.Load("Prefabs/UI/LogEntry", typeof(GameObject));
        GameObject newLogEntry = Instantiate(logEntryPrefab, ConsoleOutput.transform);

        newLogEntry.GetComponent<Text>().text = logEntry.Text;

        float prefferedHeight = 0;

        if (logEntry.CalculatedPrefferedHeight == 0)
        {
            newLogEntry.GetComponent<RectTransform>().sizeDelta = new Vector2(
                newLogEntry.GetComponent<RectTransform>().sizeDelta.x,
                0
            );

            prefferedHeight = newLogEntry.GetComponent<Text>().preferredHeight - 16;
            logEntry.CalculatedPrefferedHeight = prefferedHeight;
        }
        else
        {
            prefferedHeight = logEntry.CalculatedPrefferedHeight;
        }

        newLogEntry.GetComponent<RectTransform>().sizeDelta = new Vector2(
            newLogEntry.GetComponent<RectTransform>().sizeDelta.x,
            prefferedHeight * 1.25f
        );

        newLogEntry.transform.localPosition = new Vector3(newLogEntry.transform.localPosition.x, -(LOG_ENTRY_MARGIN + totalLogEntryHeight));
        totalLogEntryHeight += newLogEntry.GetComponent<RectTransform>().sizeDelta.y;

        ConsoleOutput.GetComponent<RectTransform>().sizeDelta = new Vector2(ConsoleOutput.GetComponent<RectTransform>().sizeDelta.x, LOG_ENTRY_MARGIN + totalLogEntryHeight);

        UpdateViewPosition();
    }

    private static void UpdateViewPosition()
    {
        ConsoleWindow.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    private void Show()
    {
        foreach (Transform oldRecord in ConsoleOutput.transform)
        {
            Destroy(oldRecord.gameObject);
            totalLogEntryHeight = 0;
        }

        foreach (var filteredRecord in Logs)
        {
            ShowLogEntry(filteredRecord);
        }
    }

    public void OnEndEdit(GameObject input)
    {
        InputField inputField = input.GetComponent<InputField>();
        ProcessCommand(inputField.text);
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
    }
}
