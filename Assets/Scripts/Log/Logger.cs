using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum LOG_TYPE
{
    DEBUG = 1,
    WARNING = 2,
    ERROR = 3
}

public class Logger
{
    internal static readonly string LogDefault_FileName = "log_";

    internal static void AppendLog(string _logTag, string _log, LOG_TYPE _type = LOG_TYPE.ERROR)
    {
        string[] lines = _log.Split('\n');
        string emptyTag = GetEmptyString(_logTag.Length);
    }

    internal static void ClearLog()
    {
        if (File.Exists(GetOrCreateFilePath()))
        {
            File.Delete(GetOrCreateFilePath());
        }
    }

    /// <summary>
    /// 로그 파일에 1줄씩 로그 기록 처리
    /// </summary>
    /// <param name="_logTag"></param>
    /// <param name="_log"></param>
    /// <param name="_type"></param>
    private static void AppendSingleLine(string _logTag, string _log, LOG_TYPE _type)
    {
        string filePath = GetOrCreateFilePath();
        int lineCount = File.ReadAllLines(filePath).Length;
        FileStream fileStream = new FileStream(filePath, FileMode.Append);
        StreamWriter writer = new StreamWriter(fileStream);

        writer.WriteLine($"{lineCount:D5} {_logTag} {GetCurrentTime()} :: {_type} :: {_log}");
        writer.Flush();
        writer.Dispose();
        fileStream.Dispose();
    }

    private static string GetOrCreateFilePath() 
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, "Log");
        string fileName = $"{LogDefault_FileName}{DateTime.Now.ToString("yyyy_MM_dd")}.txt";
        string path = Path.Combine(directoryPath, fileName);

        if (!Directory.Exists(directoryPath))               // 디렉토리 폴더에 해당 path가 없을 경우 생성 
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (!File.Exists(path)) 
        {
            FileStream fileStream = new FileStream(path, FileMode.Append);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.WriteLine($"Log File Created :: {GetCurrentTime()}");
            writer.Flush();
            writer.Dispose();
            fileStream.Dispose();
        }
        
        return path;
    }

    /// <summary>
    /// 현재 시간  return
    /// </summary>
    /// <returns></returns>
    private static string GetCurrentTime()
    {
        return DateTime.Now.ToString("yyyy_MM_dd_HH:mm:ss");
    }

    private static string GetEmptyString(int _len)
    {
        string result = string.Empty;
        for (int i = 0; i < _len; i++) 
        {
            result += " ";
        }
        return result;
    }
}
