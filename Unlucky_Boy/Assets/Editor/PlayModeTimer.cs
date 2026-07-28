using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Herramienta de Editor para cronometrar el tiempo que se pasa en modo Play.
/// Acceso: Window > Tools > Play Mode Timer
/// </summary>
public class PlayModeTimer : EditorWindow
{
    private static double sessionStartTime;   // Momento en que empezó la sesión actual de Play
    private static double sessionElapsed;     // Tiempo transcurrido en la sesión actual
    private static bool isPlaying;

    private const string TotalTimeKey = "PlayModeTimer_TotalTime";
    private const string HistoryKey = "PlayModeTimer_History";
    private const int MaxHistoryEntries = 100; // Para no acumular infinitas entradas

    private Vector2 scrollPos;

    [Serializable]
    private class SessionRecord
    {
        public string date;     // Fecha y hora de inicio de la sesión
        public float duration;  // Duración en segundos
    }

    [Serializable]
    private class SessionHistory
    {
        public List<SessionRecord> sessions = new List<SessionRecord>();
    }

    [MenuItem("Window/Tools/Play Mode Timer")]
    public static void ShowWindow()
    {
        var window = GetWindow<PlayModeTimer>("Play Timer");
        window.minSize = new Vector2(280, 320);
    }

    [InitializeOnLoadMethod]
    private static void Init()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        EditorApplication.update += OnEditorUpdate;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.EnteredPlayMode:
                sessionStartTime = EditorApplication.timeSinceStartup;
                isPlaying = true;
                break;

            case PlayModeStateChange.ExitingPlayMode:
                isPlaying = false;
                double sessionTime = EditorApplication.timeSinceStartup - sessionStartTime;
                double totalTime = EditorPrefs.GetFloat(TotalTimeKey, 0f);
                totalTime += sessionTime;
                EditorPrefs.SetFloat(TotalTimeKey, (float)totalTime);

                AddHistoryEntry((float)sessionTime);

                sessionElapsed = 0;
                break;
        }
    }

    private static void OnEditorUpdate()
    {
        if (isPlaying)
        {
            sessionElapsed = EditorApplication.timeSinceStartup - sessionStartTime;
            // Repintar la ventana si está abierta, para que se actualice en tiempo real
            if (HasOpenInstances<PlayModeTimer>())
            {
                GetWindow<PlayModeTimer>().Repaint();
            }
        }
    }

    private static SessionHistory LoadHistory()
    {
        string json = EditorPrefs.GetString(HistoryKey, "");
        if (string.IsNullOrEmpty(json))
            return new SessionHistory();

        try
        {
            return JsonUtility.FromJson<SessionHistory>(json) ?? new SessionHistory();
        }
        catch
        {
            return new SessionHistory();
        }
    }

    private static void SaveHistory(SessionHistory history)
    {
        EditorPrefs.SetString(HistoryKey, JsonUtility.ToJson(history));
    }

    private static void AddHistoryEntry(float duration)
    {
        // Ignoramos sesiones casi instantáneas (por ejemplo, entrar y salir sin querer)
        if (duration < 1f) return;

        var history = LoadHistory();
        history.sessions.Add(new SessionRecord
        {
            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
            duration = duration
        });

        // Nos quedamos solo con las últimas MaxHistoryEntries entradas
        if (history.sessions.Count > MaxHistoryEntries)
        {
            history.sessions.RemoveRange(0, history.sessions.Count - MaxHistoryEntries);
        }

        SaveHistory(history);
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("Cronómetro de Play Mode", EditorStyles.boldLabel);
        GUILayout.Space(10);

        EditorGUILayout.LabelField("Estado:", isPlaying ? "▶ En reproducción" : "■ Detenido");

        GUILayout.Space(5);
        EditorGUILayout.LabelField("Sesión actual:", FormatTime(sessionElapsed));

        float totalTime = EditorPrefs.GetFloat(TotalTimeKey, 0f);
        EditorGUILayout.LabelField("Tiempo total acumulado:", FormatTime(totalTime));

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Resetear tiempo total"))
        {
            if (EditorUtility.DisplayDialog("Resetear",
                "¿Seguro que quieres poner a cero el tiempo total acumulado?", "Sí", "Cancelar"))
            {
                EditorPrefs.SetFloat(TotalTimeKey, 0f);
            }
        }
        if (GUILayout.Button("Limpiar historial"))
        {
            if (EditorUtility.DisplayDialog("Limpiar historial",
                "¿Seguro que quieres borrar el historial de sesiones?", "Sí", "Cancelar"))
            {
                SaveHistory(new SessionHistory());
            }
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(15);
        GUILayout.Label("Historial de sesiones", EditorStyles.boldLabel);

        var history = LoadHistory();
        if (history.sessions.Count == 0)
        {
            EditorGUILayout.HelpBox("Todavía no hay sesiones registradas.", MessageType.Info);
        }
        else
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));

            // Mostramos las más recientes primero
            for (int i = history.sessions.Count - 1; i >= 0; i--)
            {
                var entry = history.sessions[i];
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                EditorGUILayout.LabelField(entry.date, GUILayout.Width(130));
                EditorGUILayout.LabelField(FormatTime(entry.duration));
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }
    }

    private static string FormatTime(double seconds)
    {
        int h = (int)(seconds / 3600);
        int m = (int)((seconds % 3600) / 60);
        int s = (int)(seconds % 60);
        return $"{h:00}:{m:00}:{s:00}";
    }
}
