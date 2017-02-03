package com.example.kai.sqliteexample;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.GridView;
import android.widget.ListView;
import android.widget.TextView;
import android.text.Editable;
import android.text.TextWatcher;
import android.widget.AutoCompleteTextView;

import com.example.kai.sqliteexample.CustomAdapter.ListAdapter;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class SelectBrandActivity extends AppCompatActivity{

    static String brand;
    ArrayList<Categories> listArr;
    EditText searchBrand;
    ListView lvBrand;
    DatabaseAccess db;
    ListAdapter _adapter;
    int deviceID;
    static int BrandID;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_select_brand);

        deviceID = AddDeviceActivity.deviceID;
        db = DatabaseAccess.getInstance(this);

        TextView txt_Device = (TextView) findViewById(R.id.txt_Device);
        txt_Device.setText(AddDeviceActivity.device);

        searchBrand = (EditText) findViewById(R.id.txt_SearchBrand);
        lvBrand = (ListView) findViewById(R.id.lv_Brand);
        InitList(deviceID,"");

        //Xử lý Search
        searchBrand.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }
            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

                if (!s.toString().equals("")) {
                    InitList(deviceID,s.toString());
                }
                else
                    InitList(deviceID,"");
            }
            @Override
            public void afterTextChanged(Editable s) {
            }
        });

        //Xử lý Click List View
        lvBrand.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> arg0, View v, int arg2, long arg3) {
                Intent selectmodelIntent = new Intent(SelectBrandActivity.this, SelectModelActivity.class);

                //Xử lý chọn Brand ???
                TextView textview1 = (TextView) v.findViewById(R.id.textView_Name);
                TextView textview2 = (TextView) v.findViewById(R.id.textViewCategoryID);
                BrandID = Integer.parseInt(textview2.getText().toString());
                brand = textview1.getText().toString(); // Get tieu de hang
                startActivity(selectmodelIntent);
                overridePendingTransition(R.animator.activity_open_translate, R.animator.activity_close_scale);
            }
        });
    }

    //Tạo List View
    public void InitList(int deviceID, String searchString){
        lvBrand.setAdapter(null);
        listArr = db.getBrandForDevideID(deviceID,searchString);
        _adapter = new ListAdapter(SelectBrandActivity.this, R.layout.activity_item_row_category,listArr);
        lvBrand.setAdapter(_adapter);
    }

    @Override
    public void onBackPressed()
    {
        super.onBackPressed();
        overridePendingTransition(R.animator.activity_open_scale, R.animator.activity_close_translate);
        return;
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                onBackPressed();
                return true;
        }
        return super.onOptionsItemSelected(item);
    }

}
