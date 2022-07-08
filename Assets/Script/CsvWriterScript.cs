using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class CsvWriterScript : MonoBehaviour
{
    private StreamWriter damageData;
    private StreamWriter killData;
    private string damageFileName;
    private string killFileName;    


    void Start()
    {
        DateTime now = DateTime.Now;
        damageFileName = "Data/DamageData" + now.Year.ToString() + "_" + now.Month.ToString() 
            + "_" + now.Day.ToString() + "__" + now.Hour.ToString() + "_" + now.Minute.ToString()+".csv";
        killFileName = "Data/KillData" + now.Year.ToString() + "_" + now.Month.ToString()
            + "_" + now.Day.ToString() + "__" + now.Hour.ToString() + "_" + now.Minute.ToString() + ".csv";

        damageData = new StreamWriter(damageFileName, true, Encoding.GetEncoding("Shift_JIS"));
        killData = new StreamWriter(killFileName, true, Encoding.GetEncoding("Shift_JIS"));

        string[] s1 = { "Damage", "Time" };
        string s2 = string.Join(",", s1);
        damageData.WriteLine(s2);

        string[] s3 = { "KillCounter", "KillTime" };
        string s4 = string.Join(",", s3);
        killData.WriteLine(s4);
    }

    public void GetDamageData(string txt1, string txt2)
    {
        string[] s1 = { txt1, txt2 };
        string s2 = string.Join(",", s1);
        damageData.WriteLine(s2);
    }

    public void GetKillData(string txt1, string txt2)
    {
        string[] s1 = { txt1, txt2 };
        string s2 = string.Join(",", s1);
        killData.WriteLine(s2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            damageData.Close();
            killData.Close();
        }
    }
}
