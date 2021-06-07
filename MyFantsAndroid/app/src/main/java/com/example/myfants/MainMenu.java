package com.example.myfants;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import androidx.appcompat.app.AppCompatActivity;

public class MainMenu extends AppCompatActivity implements View.OnClickListener {

    Button ProductsList;
    Button ProductsBasket;
    Button ProductsFav;

    //private String login;
   // private String role;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_menu_activity);

        ProductsList = findViewById(R.id.products_list);
        ProductsList.setOnClickListener(this);
        //ProductsBasket = findViewById(R.id.products_basket);
        //ProductsBasket.setOnClickListener(this);
        //ProductsFav = findViewById(R.id.fav_list);
        //ProductsFav.setOnClickListener(this);

        //login = Objects.requireNonNull(getIntent().getExtras()).getString("login");
        //role = Objects.requireNonNull(getIntent().getExtras()).getString("role");

    }

    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.products_list:
                Intent prepodinfo = new Intent(this, ProductListView.class);
                startActivity(prepodinfo);
                break;
            //case R.id.products_basket:
                //Intent i2 = new Intent(this, BascetListView.class);
                //i2.putExtra("type",2);
                //i2.putExtra("role",role);
                //i2.putExtra("login", login);
                //startActivity(i2);
               // break;
            //case R.id.chat_list:
                //Intent i3 = new Intent(this, ScheduleView.class);
                //i3.putExtra("type",3);
                //i3.putExtra("role",role);
                //i3.putExtra("login", login);
                //startActivity(i3);
                //break;
            //case R.id.fav_list:
                //Intent i3 = new Intent(this, FavListView.class);
                //startActivity(i3);
                //break;
        }
    }
}
