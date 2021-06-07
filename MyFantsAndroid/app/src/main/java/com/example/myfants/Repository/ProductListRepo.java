package com.example.myfants.Repository;

import android.widget.ProgressBar;

import com.example.myfants.Model.ResponseClass;
import com.example.myfants.ProductListView;

import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONArray;

import java.io.IOException;
import java.net.URL;

public class ProductListRepo extends AsyncSuperClass {

    private ProductListView activity;
    private ResponseClass responseClass;

    public ProductListRepo(ProgressBar progressBar, ProductListView prepodListView) {
        super(progressBar);
        activity = prepodListView;
    }


    @Override
    protected ResponseClass doInBackground(Void... voids) {
        RepositoryAPI repositoryAPI = new RepositoryAPI();
        try {
            URL url = new URL("http://myfants.fvds.ru/api/getProductsList");
            JSONObject responseJSON = repositoryAPI.getRequest(url);

            String status = responseJSON.get("status").toString();
            JSONArray response = (JSONArray) responseJSON.get("response");

            responseClass = new ResponseClass();
            responseClass.setStatus(status);
            responseClass.setResponseArray(response);


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
