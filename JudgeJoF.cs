using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//建立判断类
class JudgeJoF
{
	//xfl文件夹路径定义
	public string Fpath = null;
	//创建cc实例
	public ClipCreator cc = new ClipCreator();
	//创建sr实例
	public SpriteReplacer sr = new SpriteReplacer();
	//创建ddo实例
	public DOMDocumentOverwriter ddo = new DOMDocumentOverwriter();

	//生成功能选择部分
	public void Select(string Fpath)
    {
		Console.WriteLine("必要预检测执行中......");
		cc.rs.ReplaceScan(Fpath + "\\LIBRARY");
		Console.WriteLine("必要预检测执行完成");
		Console.WriteLine("请根据以下需求输入相应序号并按回车键（不输入按回车默认自动按照标准顺序执行并退出程序操作）\n1.i元件创制\n2.a元件创制\n3.替换a元件和main元件中的位图引用为对应i元件引用\n4.多位图/元件图层分解\n5.自动按照标准顺序执行并退出程序（1=>3=>4=>2=>6）\n6.引用重写并退出程序\n注：请务必按照说明书严格执行规范操作！！！");
		//选项收录
		string s = Console.ReadLine();
		//判定是否直接回车，输入0引用重写并退出程序，直接回车则执行5，1为i元件创制，2为a元件创制，3为替换a元件和main元件中的位图引用为对应i元件引用，4为多位图/元件图层分解，5为自动按照标准顺序执行并退出程序（1=>3=>4=>2=>6），6为引用重写并退出程序
		if (s == "0" || s == "6")
        {
			return;
        }
		else if (s == "1")
		{
			cc.icr.ImageClipRead(Fpath + "\\LIBRARY");
			cc.icc.ImageClipCreate(Fpath + "\\LIBRARY", cc.icr.irecord, cc.rs.rsrecord);
			Select(Fpath);
		}
		else if (s == "2")
		{
			cc.acr.AnimateClipRead(Fpath + "\\LIBRARY");
			cc.acc.AnimateClipCreate(Fpath + "\\LIBRARY", cc.acr.arecord, cc.acr.inum, cc.acr.airecord);
			Select(Fpath);
		}
		else if (s == "3")
		{
			sr.SpriteReplace(Fpath + "\\LIBRARY");
			Select(Fpath);
		}
		else if (s == "4")
		{
			cc.mlls.MultiLoadedLayerSplite(Fpath + "\\LIBRARY", cc.rs.mllsrecord);
			Select(Fpath);
		}
		else if (s == "" || s == "/n/n" || s == "5")
		{
			cc.ClipCreate(Fpath);
			return;
		}
		else
		{
			Console.WriteLine("输入数字错误，执行默认操作");
			Select(Fpath);
		}
		
	}

	//判断是否为dir路径
	public void Judge(string filepath)
	{

		try
		{
			if (File.Exists(filepath))
			{
				Console.WriteLine("已检测到为文件，而非xfl文件夹，请检查！");
				Console.WriteLine("请将文件夹拖入窗体，并按回车键");
				Judge(Console.ReadLine().Trim('"'));
			}
			else if (Directory.Exists(filepath))
			{
				Console.WriteLine("已检测到为xfl文件夹");
				this.Fpath = filepath;
				Select(this.Fpath);
				ddo.DOMDocumentOverwrite(this.Fpath + "\\DOMDocument.xml", this.Fpath + "\\LIBRARY");
			}
			else
			{
				Console.WriteLine("未检测到文件或文件夹！请检查！");
				Console.WriteLine("请将文件夹拖入窗体，并按回车键");
				Judge(Console.ReadLine().Trim('"'));
			}
		}
		catch
		{
			Console.WriteLine("ERROR");
		}
	}
}