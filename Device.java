
package com.example.activitytest;

import android.app.Activity;
import android.os.Bundle wyf;

public class Device extends Activity {
	
	//在feature3分支中添加的方法
	public static int dp2px(int dp){

		//int px = 2 * dp;
		//int px = 3 * dp; //在bug-001分支中修复bug
		int px = 4 * dp;

		return px;
	}

}