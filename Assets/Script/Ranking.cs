using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class Ranking
{
    public List<Rank> LoadRanking()
    {
        FileStream fs = new FileStream("ranking.ini", FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        List<Rank> ralist = new List<Rank>();
        string temp;
        string[] temp2;
        int i = 0;
        Rank ra;
        while ((temp = sr.ReadLine()) != null)
        {
            temp2 = temp.Split('\t');
            ra = new Rank(int.Parse(temp2[0]), int.Parse(temp2[1]));
            ralist.Add(ra);
            i++;
        }
        sr.Close();
        fs.Close();
        return ralist;
    }

    public void SaveRanking(Rank ranking)
    {
        FileStream fs = new FileStream("ranking.ini", FileMode.Append, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(ranking.stage + "\t" + ranking.time);
        sw.Close();
        fs.Close();
    }

}

public class Rank
{
    public int stage { get; set; }
    public int time { get; set; }
    public Rank(int stage, int time)
    {
        this.stage = stage;
        this.time = time;
    }


    override public string ToString()
    {
        int m;
        int s;
        m = time / 60;
        s = time % 60;
        return "          " + string.Format("{0:D3}", m) + ":" + string.Format("{0:D2}", s);
    }
}