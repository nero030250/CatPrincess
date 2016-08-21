using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class Board
{
    public int level { get; set; }
	public List<string> tip { get; private set; }
    public int[,] leftCsv{ get; set; }
    public int[,] rightCsv { get; set; }
    private int rowsNum = 8;
    private int column = 7;
	public Board (int lv, List<string> tip, List<string> data) {
		this.level = lv;
		this.tip = tip;
		this.leftCsv = new int[rowsNum, column];
		this.rightCsv = new int[rowsNum, column];
		string[] aryLine = null;
		for (int rowNum=0; rowNum<data.Count; rowNum++) {
			aryLine = data [rowNum].Split (',');
			for (int index = 0; index < leftCsv.GetLength (1); index ++) {
				leftCsv [rowNum, index] = int.Parse (aryLine [index]);
				rightCsv [rowNum, index] = int.Parse (aryLine [index + 8]);
			}
		}
	}
}