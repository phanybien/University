package com.example.kai.sqliteexample.CustomAdapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.example.kai.sqliteexample.Categories;
import com.example.kai.sqliteexample.R;

import java.util.List;

/**
 * Created by Kai on 5/31/2016.
 */
public class ListAdapter extends ArrayAdapter<Categories> {
    public ListAdapter(Context c, int textViewResourceID) {
        super(c,textViewResourceID);
    }
    public ListAdapter(Context c, int textViewResourceID, List<Categories> item) {
        super(c,textViewResourceID,item);
    }
    @Override
    public View getView(int position, View convertView, ViewGroup parent){
        View v = convertView;
        if(v==null){
            LayoutInflater vi = LayoutInflater.from(getContext());
            v = vi.inflate(R.layout.activity_item_row_category,null);
        }
        Categories c = getItem(position);
        if(c != null) {
            // Anh xa + gan gia tri
            TextView textView = (TextView) v.findViewById(R.id.textView_Name);
            TextView textView2 = (TextView) v.findViewById(R.id.textViewCategoryID);

            textView.setText(c.CategoryName);
            textView2.setText(String.valueOf(c.CategoryID));
        }
        return v;
    }
}
