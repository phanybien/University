package com.example.kai.sqliteexample;

import android.app.ActionBar;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        findViewById(R.id.btn_AddDevice).setOnClickListener(btnOnClick);
    }

    final View.OnClickListener btnOnClick = new View.OnClickListener() {
        @Override
        public void onClick(final View v) {
            Intent adddeviceIntent=new Intent(MainActivity.this, AddDeviceActivity.class);
            startActivity(adddeviceIntent);

            //Hiệu ứng chuyển trang
            overridePendingTransition(R.animator.activity_open_translate, R.animator.activity_close_scale);
        }
    };

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_settings, menu);
        return true;
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.action_settings:
                startActivity(new Intent(MainActivity.this, SettingsActivity.class));
                return true;
        }
        return super.onOptionsItemSelected(item);
    }
}
