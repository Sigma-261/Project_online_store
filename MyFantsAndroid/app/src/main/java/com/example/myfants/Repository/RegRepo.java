package com.example.myfants.Repository;

import android.widget.ProgressBar;

import com.example.myfants.MainActivity;
import com.example.myfants.Model.ResponseClass;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;
import java.security.NoSuchAlgorithmException;
import java.util.HashMap;
import java.util.Map;

public class RegRepo extends AsyncSuperClass {

    private Map<String, String> request;
    private MainActivity activity;
    private ResponseClass responseClass;
    private JSONObject requestJSON;


    public RegRepo (String login, String password, ProgressBar progressBar, MainActivity activity) throws NoSuchAlgorithmException, UnsupportedEncodingException, JSONException {
        super(progressBar);
        this.activity = activity;
        request = new HashMap<>();
        requestJSON = new JSONObject();
        requestJSON.put("IS_ADMIN", "FALSE");
        requestJSON.put("LOGIN", login);
        requestJSON.put("PASSWORD", password);
    }

    public RegRepo(ProgressBar progressBar) {
        super(progressBar);
    }


    @Override
    protected ResponseClass doInBackground(Void... voids) {
        try {
            JSONObject responseJSON;
            RepositoryAPI repositoryAPI = new RepositoryAPI();
            String s = "http://myfants.fvds.ru/api/addUser";
            URL url = new URL(s);
            responseJSON = repositoryAPI.postResponse(requestJSON, url);


            String status = responseJSON.get("status").toString();
            String response = responseJSON.get("response").toString();

            responseClass = new ResponseClass();
            responseClass.setStatus(status);
            responseClass.setResponseString(response);


        }catch (ProtocolException e) {
            e.printStackTrace();
        } catch (MalformedURLException e) {
            e.printStackTrace();
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
