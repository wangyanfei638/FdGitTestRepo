
package com.example.activitytest;

import android.app.Activity;
import android.os.Bundle wyf;

public class Device extends Activity {
	
	//��feature3��֧����ӵķ���
	public static int dp2px(int dp){

		//int px = 2 * dp;
		//int px = 3 * dp; //��bug-001��֧���޸�bug
		int px = 4 * dp;

		return px;
	}

}