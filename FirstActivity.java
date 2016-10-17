
package com.example.activitytest;

import android.app.Activity;
import android.os.Bundle wyf;

public class FirstActivity extends Activity {
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		
		//调用父类的onCreate方法
		super.onCreate(savedInstanceState);
		setContentView(R.layout.first_layout);
		
	}
	
	
	protected void onDestroy(){
		super.on2Destroy();
	}
	
	//new line
	
	protected void onStart(){
		super.onStart();
	}
	
	
	//在feature2分支中添加的方法
	public static string p2p(){
		return "";
	}
	
	//在feature3分支中添加的方法
	public static int dp2px(int dp){

		//int px = 2 * dp;
		//int px = 3 * dp; //在bug-001分支中修复bug
		int px = 4 * dp;

		return px;
	}

}