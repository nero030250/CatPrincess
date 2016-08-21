//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//
//public class CSVHelper
//{
//    /// <summary>
//    /// 读取文件夹下所有的CSV文件，按照文件名排序
//    /// </summary>
//    /// <param name="path"></param>
//    /// <returns></returns>
//    public static List<Board> readAllCsv(string path)
//    {
//        string[] csvList = Directory.GetFiles(path, "*.csv");
//        List<Board> listBorad = new List<Board>();
//        foreach (string str in csvList)
//        {
//            Board board = CSVHelper.Csv2Array(str);
//            listBorad.Add(board);
//        }
//        return listBorad;
//    }
//
//    /// </summary>
//    /// <param name="fileName">CSV文件路径</param>
//    /// <returns>返回读取了CSV数据的DataTable</returns>
//    public static Board Csv2Array(string filePath)
//    {
//        Encoding encoding = new UTF8Encoding(true); //Encoding.ASCII;//
//        FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
//
//        //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
//        StreamReader sr = new StreamReader(fs, encoding);
//		List<string> data = new List<string> ();
//		string str = string.Empty;
//		while ((str = sr.ReadLine ()) != null)
//			data.Add (str);
//		sr.Close();
//		fs.Close();
//		return new Board (int.Parse (Path.GetFileNameWithoutExtension(filePath)), data);       
//    }
//
//    public static string[] SplitArray(string[] Source, int StartIndex, int EndIndex)
//    {
//        try
//        {
//            string[] result = new string[EndIndex - StartIndex + 1];
//            for (int i = 0; i <= EndIndex - StartIndex; i++) result[i] = Source[i + StartIndex];
//            return result;
//        }
//        catch (IndexOutOfRangeException ex)
//        {
//            throw new Exception(ex.Message);
//        }
//    }
//    public static int[] CovertArray(string[] strArray) {
//        int[] result = new int[strArray.Length];
//        for (int i = 0; i < strArray.Length; i++) {
//            if (!string.IsNullOrEmpty(strArray[i])) {
//                result[i] = int.Parse(strArray[i]);
//            }
//        }
//        return result;
//    }
//}