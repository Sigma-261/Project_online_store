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

public class LoginRepo extends AsyncSuperClass {

    private Map <String, String> request;
    private MainActivity activity;
    private ResponseClass responseClass;


    public LoginRepo (String login, String password, ProgressBar progressBar, MainActivity activity) throws NoSuchAlgorithmException, UnsupportedEncodingException {
        super(progressBar);
        this.activity = activity;
        request = new HashMap<>();
        request.put("login", login);
        request.put("PASSWORD", password);

    }


    @Override
    protected ResponseClass doInBackground(Void... voids) {
        try {

            RepositoryAPI repositoryAPI = new RepositoryAPI();
            String s = "http://myfants.fvds.ru/api/authorizationMobile";
            s = repositoryAPI.URLBuilder(s, request);
            URL url = new URL(s);
            JSONObject responseJSON = repositoryAPI.getRequest(url);


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
