package com.example.kai.sqliteexample;

/**
 * Created by Kai on 6/2/2016.
 */
import android.hardware.ConsumerIrManager;
import android.content.DialogInterface;
import android.hardware.ConsumerIrManager;
import android.os.Bundle;
import android.util.Log;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class IRHelper
{
    private static final String TAG = "ConsumerIrService";
    public ConsumerIrManager mCIR;
    int freq = -1;

    // Check IR Emitter
    public boolean checkIREmitter() {
        if(!mCIR.hasIrEmitter()){
            Log.e(TAG, "No IR Emitter found");
            return false;
        }
        return true;
    }
    //0000 006d 0022 0003 00a9 00a8 0015 003f 0015 003f 0015 003f 0015 0015 0015 0015 0015 0015 0015 0015 0015 0015 0015 003f 0015 003f 0015 003f 0015 0015 0015 0015 0015 0015 0015 0015 0015 0015 0015 0015 0015 003f 0015 0015 0015 0015 0015 0015 0015 0015 0015 0015 0015 0015 0015 0040 0015 0015 0015 003f 0015 003f 0015 003f 0015 003f 0015 003f 0015 003f 0015 0702 00a9 00a8 0015 0015 0015 0e6e
    // Convert to Decimal: 38028,169,168,21,63,21,63,21,63,21,21,21,21,21,21,21,21,21,21,21,63,21,63,21,63,21,21,21,21,21,21,21,21,21,21,21,21,21,63,21,21,21,21,21,21,21,21,21,21,21,21,21,64,21,21,21,63,21,63,21,63,21,63,21,63,21,63,21,1794,169,168,21,21,21,3694

    //Convert to Decimal
    public String hex2dec(String irData) {
        List<String> list = new ArrayList<String>(Arrays.asList(irData.split(" ")));
        list.remove(0); // dummy
        int frequency = Integer.parseInt(list.remove(0), 16); // frequency
        list.remove(0); // seq1
        list.remove(0); // seq2

        for (int i = 0; i < list.size(); i++) {
            list.set(i, Integer.toString(Integer.parseInt(list.get(i).trim(), 16)));
        }

        frequency = (int) (1000000 / (frequency * 0.241246));
        list.add(0, Integer.toString(frequency));

        irData = "";
        for (String s : list) {
            irData += s + ",";
        }
        return irData;
    }

    // Decimal to Duration: 4368,546,1638,546,1638,546,1638,546,546,546,546,546,546,546,546,546,546,546,1638,546,1638,546,1638,546,546,546,546,546,546,546,546,546,546,546,546,546,1638,546,546,546,546,546,546,546,546,546,546,546,546,546,1664,546,546,546,1638,546,1638,546,1638,546,1638,546,1638,546,1638,546,46644,4394,4368,546,546,546,96044
    // Convert Duration Values
    public String count2duration(String countPattern) {
        List<String> list = new ArrayList<String>(Arrays.asList(countPattern.split(",")));
        int frequency = Integer.parseInt(list.get(0).trim());
        int pulses = 1000000/frequency;
        int count;
        int duration;

        list.remove(0);

        for (int i = 0; i < list.size(); i++) {
            count = Integer.parseInt(list.get(i).trim());
            duration = count * pulses;
            list.set(i, Integer.toString(duration));
        }

        String durationPattern = "";
        for (String s : list) {
            durationPattern += s + ",";
        }

        freq = frequency;
        Log.d(TAG, "Frequency: " + frequency);
        Log.d(TAG, "Duration Pattern: " + durationPattern);

        return durationPattern;
    }
    // Convert String Pattern to int[]
    public int[] stringToArrayInt(String pattern, boolean hasFrequency){
        List<String> list = new ArrayList<String>(Arrays.asList(pattern.split(",")));
        if(hasFrequency) {
            list.remove(0); //Remove frequency
        }
        int i = 0;
        int[] arr = new int[list.size()];
        for (String s : list) {
            arr[i] = Integer.parseInt(s.trim());
            i++;
        }
        Log.d(TAG, "Convert: True");
        return arr;
    }
    public void runTransmitDEC(String pattern){
        mCIR.transmit(freq, stringToArrayInt(count2duration(pattern), false));
    }
}
