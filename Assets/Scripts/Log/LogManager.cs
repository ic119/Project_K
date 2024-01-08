using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro.EditorUtilities;
using UnityEngine;


public class LogManager : Singleton<LogManager>
{
    #region variable
    protected StreamWriter writer;
    #endregion

    #region LifeCycle
    public new void Awake()
    {
        writer = new StreamWriter("Assets/Logs/log.txt");

        Application.logMessageReceived += SaveLog;   // logMessageReceived �ݹ� �Լ� �߰�
    }

    public void OnDisable()
    {
        Application.logMessageReceived -= SaveLog;  // logMessageReceived �ݹ� �Լ� ����

        writer.Flush();
        writer.Close();
    }
    #endregion

    #region method
    /// <summary>
    /// �α� ���� ó��
    /// </summary>
    /// <param name="_log">����</param>
    /// <param name="_stackTrace"></param>
    /// <param name="_type"></param>
    private void SaveLog(string _log, string _stackTrace, LogType _type)
    {
        string cur_time = DateTime.Now.ToString(("HH:mm:ss"));
        writer.WriteLine($"[{cur_time}] {_log}");
    }
    #endregion
}
