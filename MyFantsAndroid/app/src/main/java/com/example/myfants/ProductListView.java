package com.example.myfants;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.example.myfants.Model.ResponseClass;
import com.example.myfants.Repository.AsyncInterface;
import com.example.myfants.Repository.ProductListRepo;

import org.json.JSONArray;
import org.json.JSONException;

import java.util.ArrayList;

import androidx.appcompat.app.AppCompatActivity;

public class ProductListView extends AppCompatActivity implements AsyncInterface <ResponseClass> {

    private TextView ErrorText;
    private ProgressBar progressBar;
    private LinearLayout linearLayout;
    private ProductListView activity;
    private ArrayList <String> PrepodList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.product_list_activity);
        ErrorText = findViewById(R.id.error_text_prepod_list);
        progressBar = findViewById(R.id.progressbar_prepod_list);
        EditText searchText = findViewById(R.id.search_text);
        linearLayout = findViewById(R.id.prepod_list_layout);
        activity = this;

        ProductListRepo prepodListRepo = new ProductListRepo(progressBar, this);
        prepodListRepo.execute();

        PrepodList = new ArrayList<>();
        ProductSearch prepodSearch = new ProductSearch(this, PrepodList );
        searchText.addTextChangedListener(prepodSearch);
    }


    @Override
    public void onAsyncTaskFinished(ResponseClass responseClass) {
        try {
            if(responseClass.getStatus().equals("OK")){
                JSONArray array = responseClass.getResponseArray();
                for(int n = 0; n < array.length(); n++){
                    final String s = (String) array.get(n);
                    PrepodList.add(s);
                }
                SetPrepods(PrepodList, "");
            }
            else {
                ServerError();
            }
        }
        catch (NullPointerException e){
            ServerError();
        } catch (JSONException e) {
            e.printStackTrace();
        }
        progressBar.setVisibility(View.GONE);
    }

    private void ServerError(){
        ErrorText.setText(R.string.Server_error);
        ErrorText.setVisibility(View.VISIBLE);
    }

    public void SetPrepods(ArrayList<String> prepods, String sort){
        linearLayout.removeAllViews();
        for (String s: prepods) {
            if(s.toLowerCase().contains(sort.toLowerCase())){
                Button b = new Button(this);
                b.setText(s);
                final String put = s;
                b.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        Intent intent = new Intent(activity, ProductInfoView.class);
                        intent.putExtra("name", put);
                        startActivity(intent);
                    }
                });
                linearLayout.addView(b);
            }
        }
    }

}
