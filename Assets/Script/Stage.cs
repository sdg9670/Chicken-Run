using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class Stage
{
    public List<int> LoadStage()
    {
        if (!(new FileInfo("stage.ini").Exists))
        {
            FileStream fs2 = new FileStream("stage.ini", FileMode.Append, FileAccess.Write);
            StreamWriter sw2 = new StreamWriter(fs2);
            sw2.Write("1\t0\t0\t0\t0\t0\t0\t0\t0\t0");
            sw2.Close();
            fs2.Close();
        }
        FileStream fs = new FileStream("stage.ini", FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        List<int> stList = new List<int>();
        string temp;
        if ((temp = sr.ReadLine()) != null)
        {
            string[] temp2 = temp.Split('\t');
            for (int i = 0; i < temp2.Length; i++)
            {
                stList.Add(int.Parse(temp2[i]));
            }
        }
        sr.Close();
        fs.Close();
        return stList;
    }

    public int LoadStage(int stage)
    {
        if (!(new FileInfo("stage.ini").Exists))
        {
            FileStream fs2 = new FileStream("stage.ini", FileMode.Append, FileAccess.Write);
            StreamWriter sw2 = new StreamWriter(fs2);
            sw2.Write("1\t0\t0\t0\t0\t0\t0\t0\t0\t0");
            sw2.Close();
            fs2.Close();
        }
        FileStream fs = new FileStream("stage.ini", FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        List<int> stList = new List<int>();
        string temp;
        if ((temp = sr.ReadLine()) != null)
        {
            string[] temp2 = temp.Split('\t');
            for (int i = 0; i < temp2.Length; i++)
            {
                stList.Add(int.Parse(temp2[i]));
            }
        }
        sr.Close();
        fs.Close();
        return stList[stage];
    }

    public void SaveStage(int stage)
    {
        List<int> now_stages = LoadStage();
        now_stages[stage - 1] = 1;
        FileStream fs = new FileStream("stage.ini", FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        string temp = "";
        for(int i = 0; i < now_stages.Count; i++)
        {
            if (i == now_stages.Count - 1)
                temp += now_stages[i];
            else
                temp += now_stages[i] + "\t";
        }
        
        sw.WriteLine(temp);
        sw.Close();
        fs.Close();
    }

}