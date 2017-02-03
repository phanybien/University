package com.example.kai.sqliteexample;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;

import com.example.kai.sqliteexample.CustomAdapter.ListAdapter;

import java.util.ArrayList;
import java.util.Arrays;

public class SelectModelActivity extends AppCompatActivity {

    String[] arr;
    ArrayList<Categories> listArr;
    ListAdapter _adapter;
    DatabaseAccess db;
    EditText searchModel;
    ListView lvModel;
    int brandID;
    static int modelID;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_select_model);

        db = DatabaseAccess.getInstance(this);
        brandID = SelectBrandActivity.BrandID;
        TextView txt_Model = (TextView) findViewById(R.id.txt_Model);
        txt_Model.setText(SelectBrandActivity.brand);

        searchModel = (EditText) findViewById(R.id.txt_SearchModel);
        lvModel = (ListView) findViewById(R.id.lv_Model);
        InitList(brandID,"");

        //Xử lý Search
        searchModel.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }
            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                InitList(brandID,"");
                if (!s.toString().equals("")) {
                    InitList(brandID,s.toString());
                }
            }
            @Override
            public void afterTextChanged(Editable s) {
            }
        });
        //Xử lý Click List View
        lvModel.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> arg0, View arg1, int arg2, long arg3) {
                Intent controllIntent = new Intent(SelectModelActivity.this, ControllActivity.class);
                //Xử lý chọn Model ???
                TextView textview2 = (TextView) arg1.findViewById(R.id.textViewCategoryID);
                modelID = Integer.parseInt(textview2.getText().toString());
                startActivity(controllIntent);
                overridePendingTransition(R.animator.activity_open_translate, R.animator.activity_close_scale);
            }
        });
    }



    // Tạo List View
    //Tạo List View
    public void InitList(int deviceID, String searchString){
        lvModel.setAdapter(null);
        listArr = db.getModelForBrandID(deviceID,searchString);
        _adapter = new ListAdapter(SelectModelActivity.this, R.layout.activity_item_row_category,listArr);
        lvModel.setAdapter(_adapter);
    }


    @Override
    public void onBackPressed()
    {
        super.onBackPressed();
        overridePendingTransition(R.animator.activity_open_scale, R.animator.activity_close_translate);
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
                startActivity(new Intent(SelectModelActivity.this, SettingsActivity.class));
                return true;
        }
        return super.onOptionsItemSelected(item);
    }
}
