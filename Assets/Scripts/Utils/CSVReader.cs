using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static Dictionary<int, Dialog> Read(string file)
    {
        var list = new Dictionary<int,Dialog>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;
            Dialog dialog_temp = new Dialog();
            dialog_temp.dialogNumber = int.Parse(values[0]);
            dialog_temp.Character = Regex.Split(values[1], "/");
            dialog_temp.talkName = values[2];
            dialog_temp.comment =values[3];
            dialog_temp.isChoose = bool.Parse(values[4]);
            dialog_temp.linkCondition = Regex.Split(values[5], "/");
            dialog_temp.linkDilog = values[6];
            dialog_temp.Choice1 = Regex.Split(values[7], "/");
            dialog_temp.Choice2 = Regex.Split(values[8], "/");
            dialog_temp.Choice3 = Regex.Split(values[9], "/");
            list.Add(dialog_temp.dialogNumber, dialog_temp);
        }
        return list;
    }
}
