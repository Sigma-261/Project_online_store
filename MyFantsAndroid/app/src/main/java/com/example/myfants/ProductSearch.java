package com.example.myfants;

import android.text.Editable;
import android.text.TextWatcher;

import java.util.ArrayList;

public class ProductSearch implements TextWatcher {

    private ProductListView activity;
    private ArrayList <String> s;

    public ProductSearch(ProductListView prepodListView, ArrayList<String> strings){
        super();
        activity = prepodListView;
        s = strings;
    }

    @Override
    public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

    }

    @Override
    public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
        activity.SetPrepods(s, charSequence.toString());
    }

    @Override
    public void afterTextChanged(Editable editable) {

    }
}
