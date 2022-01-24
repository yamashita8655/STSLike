using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions
{
	static public bool IsBump(RectTransform rectTrans1, RectTransform rectTrans2)
	{
		bool isBump = false;

		var rect1 = Functions.CalcurateScreenSpaceRect(rectTrans1);
		var rect2 = Functions.CalcurateScreenSpaceRect(rectTrans2);
		isBump = rect1.Overlaps(rect2);
		return isBump;
	}

	static public Rect CalcurateScreenSpaceRect(RectTransform rectTrans)
	{
		var canvas = rectTrans.GetComponentInParent<Canvas>();
		var camera = canvas.worldCamera;
		var corners = new Vector3[ 4 ];
		
		//左下、左上、右上、右下
		rectTrans.GetWorldCorners( corners );
		var screenCorner1 = RectTransformUtility.WorldToScreenPoint( camera, corners[ 1 ] );
		var screenCorner3 = RectTransformUtility.WorldToScreenPoint( camera, corners[ 3 ] );

		//左下基準
		var screenRect = new Rect();
		screenRect.x = screenCorner1.x;
		screenRect.width = screenCorner3.x - screenRect.x;
		screenRect.y = screenCorner3.y;
		screenRect.height = screenCorner1.y - screenRect.y;
		
		return screenRect;
	}
	
	static public List<string> SplitString(string src, char[] splitRule)
	{
		src = Functions.ConvertReturnCode(src);
		List<string> ReturnSplitList = new List<string>();
		ReturnSplitList.AddRange(src.Split(splitRule));

		return ReturnSplitList;
	}
	
	static public string ConvertReturnCode(string src)
	{
		string output = src.Replace("\r\n", "\n");
		output = output.Replace("\r", "\n");

		return output;
	}
	
	static public int GetDayLimit(int year, int month)
	{
		int day = 0;
		if (month == 2) {
			if (DateTime.IsLeapYear(year)) {
				day = 29;
			} else {
				day = 28;
			}
		} else if (
			(month == 4) || 
			(month == 6) || 
			(month == 9) || 
			(month == 11)
		) {
			day = 30;
		} else {
			day = 31;
		}
		return day;
	}

	// ひらがなかどうかチェック
	static public bool IsHirakana(char str)
	{
		if (str >= '\u3040' && str <= '\u309F')
		{
			return true;
        }
        else
        {
			return false;
        }
	}

	//カタカナかどうか
	static public bool IsKatakana(char str)
    {
		if (str >= '\u30A0' && str <= '\u30FF')
		{
			return true;
        }
        else
        {
			return false;
        }
	}

	//漢字かどうか
	static public bool IsKanji(char str)
    {
		if (str >= '\u4E00' && str <= '\u9FFF')
		{
			return true;
        }
        else
        {
			return false;
        }
	}

	//半角小文字英語かどうか
	static public bool IsLowerAlphabetSingleByte(char str)
    {
		if (str >= 'a' && str <= 'z')
		{
			return true;
        }
        else
        {
			return false;
        }
	}


	//半角大文字英語かどうか
	static public bool IsUpperAlphabetSingleByte(char str)
    {
		if (str >= 'A' && str <= 'Z')
		{
			return true;
        }
        else
        {
			return false;
        }
	}

	//全角小文字英語かどうか
	static public bool IsLowerAlphabetMultiByte(char str)
    {
		if (str >= 'ａ' && str <= 'ｚ')
		{
			return true;
        }
        else
        {
			return false;
        }
	}


	//全角大文字英語かどうか
	static public bool IsUpperAlphabetMultiByte(char str)
    {
		if(str >= 'Ａ' && str <= 'Ｚ')
		{
			return true;
        }
        else
        {
			return false;
        }
	}


	//半角数字かどうか
	static public bool IsNumberSingleByte(char str)
    {
		if (str >= '0' && str <= '9')
		{
			return true;
        }
        else
        {
			return false;
        }
	}

	//全角数字かどうか
	static public bool IsNumberMultiByte(char str)
    {
		if (str >= '０' && str <= '９')
		{
			return true;
        }
        else
        {
			return false;
        }
	}
}
