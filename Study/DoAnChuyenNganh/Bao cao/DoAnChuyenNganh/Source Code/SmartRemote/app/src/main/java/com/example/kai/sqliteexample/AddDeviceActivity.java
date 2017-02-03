package com.example.kai.sqliteexample;

import android.app.ActionBar;
import android.app.AlertDialog;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.text.Editable;
import android.text.TextWatcher;
import android.widget.TextView;
import android.widget.AutoCompleteTextView;
import android.widget.ArrayAdapter;


public class AddDeviceActivity extends AppCompatActivity {

    static String device;
    static int deviceID;
    static int controll;
    boolean available;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_device);

        findViewById(R.id.btn_TV).setOnClickListener(btnOnClick);
        findViewById(R.id.btn_AC).setOnClickListener(btnOnClick);
        findViewById(R.id.btn_Fan).setOnClickListener(btnOnClick);
        findViewById(R.id.btn_Projector).setOnClickListener(btnOnClick);
        findViewById(R.id.btn_DVD).setOnClickListener(btnOnClick);
        findViewById(R.id.btn_DSLR).setOnClickListener(btnOnClick);

    }

    //Xử lý Click Button Device
    final View.OnClickListener btnOnClick = new View.OnClickListener() {
        @Override
        public void onClick(final View v) {
            Intent selectbrandIntent=new Intent(AddDeviceActivity.this, SelectBrandActivity.class);
            switch (v.getId()) {
                case R.id.btn_TV:
                    device = "TV";
                    controll = 1;
                    deviceID = 1;
                    available = true;
                    break;
                case R.id.btn_AC:
                    device = "Air Conditioner";
                    controll = 2;
                    deviceID = 4;
                    available = true;
                    break;
                case R.id.btn_Fan:
                    device = "Fan";
                    deviceID = -1;
                    available = false;
                    break;
                case R.id.btn_Projector:
                    device = "Projector";
                    deviceID = 3;
                    controll = 3;
                    available = true;
                    break;
                case R.id.btn_DVD:
                    device = "DVD";
                    deviceID = 1;
                    available = false;
                    break;
                case R.id.btn_DSLR:
                    device = "DSLR";
                    deviceID = -1;
                    available = false;
                    break;
            }
            if (available == true) {
                startActivity(selectbrandIntent);
                overridePendingTransition(R.animator.activity_open_translate, R.animator.activity_close_scale);
            }
            else {
                AlertDialog.Builder builder = new AlertDialog.Builder(AddDeviceActivity.this);
                builder.setTitle("Thông báo");
                builder.setMessage("Thiết bị này chưa được hỗ trợ !");
                builder.setPositiveButton("OK", null);
                AlertDialog alertDialog = builder.create();
                alertDialog.show();
            }
        }
    };

    //Xử lý nút Back
    @Override
    public void onBackPressed()
    {
        super.onBackPressed();
        overridePendingTransition(R.animator.activity_open_scale,R.animator.activity_close_translate);
        return;
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_settings, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                onBackPressed();
                return true;
            case R.id.action_settings:
                startActivity(new Intent(AddDeviceActivity.this, SettingsActivity.class));
                return true;
        }
        return super.onOptionsItemSelected(item);
    }
}
