package com.example.kai.sqliteexample;

import android.content.Intent;
import android.hardware.ConsumerIrManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.TabHost;
import android.widget.TableLayout;

public class ControllActivity extends AppCompatActivity {
    boolean isLayout1 = true;
    TableLayout tv_layout1, tv_layout2;
    IRHelper irHelper;
    DatabaseAccess db;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_controll);

        irHelper = new IRHelper();
        db = DatabaseAccess.getInstance(this);
        //Xử lý load layout theo thiết bị
        loadTabs(AddDeviceActivity.controll);

        //Xử lý chuyển tab TV
        tv_layout1 = (TableLayout) findViewById(R.id.tv_tablelayout1);
        tv_layout2 = (TableLayout) findViewById(R.id.tv_tablelayout2);
        tv_layout2.setVisibility(View.INVISIBLE);
        findViewById(R.id.btn_tv_ChangeLayout1).setOnClickListener(btnOnClick);
        findViewById(R.id.btn_tv_ChangeLayout2).setOnClickListener(btnOnClick);

        findViewById(R.id.btn_ac_FanSpeed).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_ac_Sleep).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_ac_TempDown).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_ac_TempUp).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_ac_Turbo).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_ac_AirSwing).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_ac_Mode).setOnClickListener(btnControlOnClick);

        findViewById(R.id.btn_pro_Input).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_KeyStone).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_Menu).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_PowerOn).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_Reset).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_UpArrow).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_VolumeDown).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_VolumeUp).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_LeftArrow).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_RightArrow).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_DownArrow).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_pro_Enter).setOnClickListener(btnControlOnClick);


        findViewById(R.id.btn_tv_0).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_1).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_2).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_3).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_4).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_5).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_6).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_7).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_8).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_9).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_ChanelDown).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_ChannelUp).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_DownArrow).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_LeftArrow).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_Menu).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_Mute).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_OK).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_RightArrow).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_UpArrow).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_VolumeDown).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_VolumeUp).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_TV).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_PowerOn).setOnClickListener(btnControlOnClick);
        findViewById(R.id.btn_tv_PowerOff).setOnClickListener(btnControlOnClick);
    }
    final View.OnClickListener btnControlOnClick = new View.OnClickListener() {
        @Override
        public void onClick(final View v) {
            irHelper.mCIR = (ConsumerIrManager)getSystemService(CONSUMER_IR_SERVICE);
            if (irHelper.checkIREmitter()) {
                int categoryID = 0;
                switch (v.getId()) {
                    // Dieu hoa
                    case R.id.btn_ac_FanSpeed: // Chua co
                        categoryID = -1;
                        break;
                    case R.id.btn_ac_AirSwing:
                        categoryID = -1;
                        break;
                    case R.id.btn_ac_Mode:
                        categoryID = -1;
                        break;
                    case R.id.btn_ac_Sleep:
                        categoryID = -1;
                        break;
                    case R.id.btn_ac_TempDown:
                        categoryID = -1;
                        break;
                    case R.id.btn_ac_TempUp:
                        categoryID = -1;
                        break;
                    case R.id.btn_ac_Turbo:
                        categoryID = -1;
                        break;

                    // Projector
                    case R.id.btn_pro_DownArrow:
                        categoryID = 38;
                        break;
                    case R.id.btn_pro_Enter:
                        categoryID = 43;
                        break;
                    case R.id.btn_pro_Input:
                        categoryID = -1;
                        break;
                    case R.id.btn_pro_KeyStone:
                        categoryID = -1;
                        break;
                    case R.id.btn_pro_LeftArrow:
                        categoryID = 39;
                        break;
                    case R.id.btn_pro_Menu:
                        categoryID = 24;
                        break;
                    case R.id.btn_pro_PowerOff:
                        categoryID = 18;
                        break;
                    case R.id.btn_pro_PowerOn:
                        categoryID = 17;
                        break;
                    case R.id.btn_pro_Reset:
                        categoryID = 41;
                        break;
                    case R.id.btn_pro_RightArrow:
                        categoryID = 40;
                        break;
                    case R.id.btn_pro_UpArrow:
                        categoryID = 37;
                        break;
                    case R.id.btn_pro_VolumeDown:
                        categoryID = 20;
                        break;
                    case R.id.btn_pro_VolumeUp:
                        categoryID = 19;
                        break;

                    // TV
                    case R.id.btn_tv_VolumeUp:
                        categoryID = 19;
                        break;
                    case R.id.btn_tv_VolumeDown:
                        categoryID = 20;
                        break;
                    case R.id.btn_tv_UpArrow:
                        categoryID = 37;
                        break;
                    case R.id.btn_tv_TV:
                        categoryID = 35;
                        break;
                    case R.id.btn_tv_RightArrow:
                        categoryID = 40;
                        break;
                    case R.id.btn_tv_PowerOn:
                        categoryID = 17;
                        break;
                    case R.id.btn_tv_PowerOff:
                        categoryID = 18;
                        break;
                    case R.id.btn_tv_OK:
                        categoryID = 36;
                        break;
                    case R.id.btn_tv_Mute:
                        categoryID = 23;
                        break;
                    case R.id.btn_tv_Menu:
                        categoryID = 24;
                        break;
                    case R.id.btn_tv_ChannelUp:
                        categoryID = 21;
                        break;
                    case R.id.btn_tv_DownArrow:
                        categoryID = 38;
                        break;
                    case R.id.btn_tv_ChanelDown:
                        categoryID = 22;
                        break;
                    case R.id.btn_tv_9:
                        categoryID = 33;
                        break;
                    case R.id.btn_tv_8:
                        categoryID = 32;
                        break;
                    case R.id.btn_tv_LeftArrow:
                        categoryID = 39;
                        break;
                    case R.id.btn_tv_7:
                        categoryID = 31;
                        break;
                    case R.id.btn_tv_6:
                        categoryID = 30;
                        break;
                    case R.id.btn_tv_5:
                        categoryID = 29;
                        break;
                    case R.id.btn_tv_4:
                        categoryID = 28;
                        break;
                    case R.id.btn_tv_3:
                        categoryID = 27;
                        break;
                    case R.id.btn_tv_2:
                        categoryID = 26;
                        break;
                    case R.id.btn_tv_1:
                        categoryID = 25;
                        break;
                    case R.id.btn_tv_0:
                        categoryID = 34;
                        break;
                    default:
                        break;
                }
                String pattern = db.getCodeDec(categoryID,AddDeviceActivity.deviceID, SelectBrandActivity.BrandID, SelectModelActivity.modelID);
                if(pattern != null && pattern != "") {
                    irHelper.runTransmitDEC(pattern);
                    Log.e("Pattern: ",pattern);
                }
            }
        }
    };
    final View.OnClickListener btnOnClick = new View.OnClickListener() {
        @Override
        public void onClick(final View v) {
            if (isLayout1 == true) {
                tv_layout1.setVisibility(View.INVISIBLE);
                tv_layout2.setVisibility(View.VISIBLE);
                isLayout1 = false;
            }
            else
            if (isLayout1 == false){
                tv_layout2.setVisibility(View.INVISIBLE);
                tv_layout1.setVisibility(View.VISIBLE);
                isLayout1 = true;
            }

        }
    };

    public void loadTabs(int inv)
    {
        TabHost.TabSpec spec;
        //TV Tab
        final TabHost tv_tab = (TabHost) findViewById(R.id.tv_tabHost);
        tv_tab.setup();

        spec = tv_tab.newTabSpec("t1");
        spec.setContent(R.id.tv_tab1);
        spec.setIndicator("TV");
        tv_tab.addTab(spec);

        spec = tv_tab.newTabSpec("t2");
        spec.setContent(R.id.tv_tab2);
        spec.setIndicator("Performance");
        tv_tab.addTab(spec);

        tv_tab.setCurrentTab(0);

        //AC Tab
        final TabHost ac_tab = (TabHost) findViewById(R.id.ac_tabHost);
        ac_tab.setup();

        spec = ac_tab.newTabSpec("t1");
        spec.setContent(R.id.ac_tab1);
        spec.setIndicator("Air Conditioner");
        ac_tab.addTab(spec);

        spec = ac_tab.newTabSpec("t2");
        spec.setContent(R.id.ac_tab2);
        spec.setIndicator("Performance");
        ac_tab.addTab(spec);

        ac_tab.setCurrentTab(0);

        //Projector Tab
        final TabHost pro_tab = (TabHost) findViewById(R.id.pro_tabHost);
        pro_tab.setup();

        spec = pro_tab.newTabSpec("t1");
        spec.setContent(R.id.pro_tab1);
        spec.setIndicator("Projector");
        pro_tab.addTab(spec);

        spec = pro_tab.newTabSpec("t2");
        spec.setContent(R.id.pro_tab2);
        spec.setIndicator("Performance");
        pro_tab.addTab(spec);

        pro_tab.setCurrentTab(0);

        //Xử lý hiển thị
        tv_tab.setVisibility(View.INVISIBLE);
        ac_tab.setVisibility(View.INVISIBLE);
        pro_tab.setVisibility(View.INVISIBLE);
        switch(inv){
            case 1:
                tv_tab.setVisibility(View.VISIBLE);
                break;
            case 2:
                ac_tab.setVisibility(View.VISIBLE);
                break;
            case 3:
                pro_tab.setVisibility(View.VISIBLE);
                break;
            default:
                break;
        }
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                onBackPressed();
                return true;
        }
        //int id = item.getItemId();
        //noinspection SimplifiableIfStatement
        //if (id == R.id.action_settings) {
           // return true;
        //}
        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onBackPressed()
    {
        super.onBackPressed();
        overridePendingTransition(R.animator.activity_open_scale,R.animator.activity_close_translate);
        return;
    }
}
