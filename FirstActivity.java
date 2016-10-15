
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

}