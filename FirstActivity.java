
package com.example.activitytest;

import android.app.Activity;
import android.os.Bundle;

public class FirstActivity extends Activity {
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		
		//调用父类的onCreate方法
		super.onCreate(savedInstanceState);
		setContentView(R.layout.first_layout);
		
	}
	
	
	protected void onDestroy(){
		super.onDestroy();
	}
	
	
	protected void onStart(){
		super.onStart();
	}
	
	
	//在feature2分支中添加的方法
	public static string p2p(){
		return "";
	}

}