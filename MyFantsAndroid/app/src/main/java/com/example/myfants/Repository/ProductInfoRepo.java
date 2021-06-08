package com.example.myfants.Repository;


import android.widget.ProgressBar;

import com.example.myfants.Model.ResponseClass;
import com.example.myfants.ProductInfoView;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.net.URL;


public class ProductInfoRepo extends AsyncSuperClass {

    private ProductInfoView activity;
    private String name;
    private ResponseClass responseClass;


    public ProductInfoRepo(ProgressBar progressBar, ProductInfoView prepodInfoView, String name){
        super(progressBar);
        activity = prepodInfoView;
        this.name = name;
    }

    @Override
    protected ResponseClass doInBackground(Void... voids) {
        RepositoryAPI repositoryAPI = new RepositoryAPI();
        try {
            URL url = new URL("http://myfants.fvds.ru/api/getProductByName" + "?name=" + name);
            JSONObject responseJSON = repositoryAPI.getRequest(url);

            String status = responseJSON.get("status").toString();
            JSONObject r = (JSONObject) responseJSON.get("response");

            responseClass = new ResponseClass();
            responseClass.setResponseObject(r);
            responseClass.setStatus(status);

        } catch (IOException e) {
            e.printStackTrace();
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return responseClass;
    }

    @Override
    protected void onPostExecute(ResponseClass responseClass) {
        super.onPostExecute(responseClass);
        activity.onAsyncTaskFinished(responseClass);
    }
}
