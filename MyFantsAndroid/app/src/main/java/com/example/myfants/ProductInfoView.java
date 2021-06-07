package com.example.myfants;

import android.os.Bundle;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.example.myfants.Model.Chotots;
import com.example.myfants.Model.ResponseClass;
import com.example.myfants.Repository.AsyncInterface;
import com.example.myfants.Repository.ProductInfoRepo;

import org.json.JSONException;

import java.util.Objects;

import androidx.appcompat.app.AppCompatActivity;

public class ProductInfoView extends AppCompatActivity implements AsyncInterface <ResponseClass> {

    //private Button basketButton;
    private String name;
    private TextView ErrorText;
    private ProgressBar progressBar;
    private TextView textView;
    private Chotots chotos;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.product_info_activity);
        name = Objects.requireNonNull(getIntent().getExtras()).getString("name");
        ErrorText = findViewById(R.id.error_text_prepod_info);
        progressBar = findViewById(R.id.progressbar_prepod_info);
        textView = findViewById(R.id.prepod_info_text);
        //basketButton = findViewById(R.id.add_basket);
        //basketButton.setOnClickListener(this);
        ProductInfoRepo prepodInfoRepo = new ProductInfoRepo(progressBar, this,name);
        prepodInfoRepo.execute();
    }

    @Override
    public void onAsyncTaskFinished(ResponseClass responseClass) {


        try {
            String name = responseClass.getResponseObject().get("name").toString();
            String price = responseClass.getResponseObject().get("price").toString();
            String date_time = responseClass.getResponseObject().get("date_time").toString();
            String description = responseClass.getResponseObject().get("description").toString();
            chotos = new Chotots();
            chotos.setName(name);
            chotos.setPrice(price);
            chotos.setDate_time(date_time);
            chotos.setDescription(description);

        }

        catch (JSONException e) {
            e.printStackTrace();
        }
        try {
            if(responseClass.getStatus().equals("OK")){
                String info = "Имя:" + chotos.getName() +"\nЦена:" + chotos.getPrice() +"\nДата:" + chotos.getDate_time() +"\nОписание:" + chotos.getDescription();
                textView.setText(info);
            }
            else {
                ErrorText.setText(R.string.Server_error);
                ErrorText.setVisibility(View.VISIBLE);
            }
        }
        catch (NullPointerException e){
            ErrorText.setText(R.string.Server_error);
            ErrorText.setVisibility(View.VISIBLE);
        }
        progressBar.setVisibility(View.GONE);
    }
    
}
