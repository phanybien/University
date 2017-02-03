package com.example.kai.sqliteexample;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import java.util.ArrayList;
import java.util.List;
/**
 * Created by Kai on 5/24/2016.
 */
public class DatabaseAccess {
    private SQLiteOpenHelper openHelper;
    private SQLiteDatabase database;
    private static DatabaseAccess instance;

    /**
     * Private constructor to aboid object creation from outside classes.
     *
     * @param context
     */
    private DatabaseAccess(Context context) {
        this.openHelper = new DatabaseOpenHelper(context);
    }

    /**
     * Return a singleton instance of DatabaseAccess.
     *
     * @param context the Context
     * @return the instance of DabaseAccess
     */
    public static DatabaseAccess getInstance(Context context) {
        if (instance == null) {
            instance = new DatabaseAccess(context);
        }
        return instance;
    }

    /**
     * Open the database connection.
     */
    public void open() {
        this.database = openHelper.getWritableDatabase();
    }

    /**
     * Close the database connection.
     */
    public void close() {
        if (database != null) {
            this.database.close();
        }
    }

    /**
     * Read all quotes from the database.
     *
     * @return a List of quotes
     */
    public ArrayList<String> getQuotes() {
        open();
        ArrayList<String> list = new ArrayList<String>();
        Cursor cursor = database.rawQuery("SELECT CategoryName FROM Categories", null);
        cursor.moveToFirst();
        while (!cursor.isAfterLast()) {
            list.add(cursor.getString(0));
            cursor.moveToNext();
        }
        cursor.close();
        close();
        return list;
    }
    public ArrayList<Categories> getBrandForDevideID(int ID, String searchString){
        open(); // Connect Database

        ArrayList<Categories> list = new ArrayList<Categories>();
        String query = "Select CategoryID, CategoryName from Categories JOIN (Select DISTINCT Distributions.BranchID " +
                "from Distributions, Categories where Distributions.DeviceID == " + ID + ") where Categories.CategoryID == BranchID";
        if(searchString.length() > 0) {
            query += " and Categories.CategoryName LIKE \"%" + searchString +"%\"";
        }
        Cursor cursor = database.rawQuery(query, null);
        cursor.moveToFirst();
        while (!cursor.isAfterLast()) {
            Categories c = new Categories(cursor.getInt(0),cursor.getString(1));
            list.add(c);
            cursor.moveToNext();
        }
        cursor.close();

        close();// Disconnect Database
        return list;
    }

    public ArrayList<Categories> getModelForBrandID(int ID, String searchString) {
        open(); // Connect Database

        ArrayList<Categories> list = new ArrayList<Categories>();
        String query = "Select CategoryID, CategoryName from Categories JOIN (Select DISTINCT Distributions.ModelID " +
                "from Distributions, Categories where Distributions.BranchID == " + ID + ") where Categories.CategoryID == ModelID";
        if(searchString.length() > 0) {
            query += " and Categories.CategoryName LIKE \"%" + searchString +"%\"";
        }
        Cursor cursor = database.rawQuery(query, null);
        cursor.moveToFirst();
        while (!cursor.isAfterLast()) {
            Categories c = new Categories(cursor.getInt(0),cursor.getString(1));
            list.add(c);
            cursor.moveToNext();
        }
        cursor.close();

        close();// Disconnect Database
        return list;
    }

    public String getCodeDec(int categoryID, int deviceID, int brandID, int modelID){
        open(); // Connect Database

       String list = "";
        String query = "select Controls.CodeDec from Controls where Controls.CategoryID == " +categoryID+" AND Controls.DistributionID == " +
                "(select Distributions.DistributionID from Distributions where Distributions.BranchID == " +brandID+" AND Distributions.DeviceID == " +deviceID+" AND " +
                "Distributions.ModelID == " +modelID+")";
        Cursor cursor = database.rawQuery(query, null);
        cursor.moveToFirst();
        if (!cursor.isAfterLast()) {
            list = cursor.getString(0);
        }
        cursor.close();

        close();// Disconnect Database
        return list;
    }
}
