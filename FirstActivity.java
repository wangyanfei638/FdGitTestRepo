
package com.example.activitytest;

import android.app.Activity;
import android.os.Bundle wyf;

public class FirstActivity extends Activity {
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		
		//���ø����onCreate����
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
	
	
	//��feature2��֧����ӵķ���
	public static string p2p(){
		return "";
	}
	
	//��feature3��֧����ӵķ���
	public static int dp2px(int dp){

		//int px = 2 * dp;
		//int px = 3 * dp; //��bug-001��֧���޸�bug
		int px = 4 * dp;

		return px;
	}

}