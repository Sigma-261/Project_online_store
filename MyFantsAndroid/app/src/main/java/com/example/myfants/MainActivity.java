package com.example.myfants;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.example.myfants.Model.ResponseClass;
import com.example.myfants.Repository.AsyncInterface;
import com.example.myfants.Repository.LoginRepo;
import com.example.myfants.Repository.RegRepo;
import com.example.myfants.R;

import java.io.UnsupportedEncodingException;
import java.security.NoSuchAlgorithmException;

import androidx.appcompat.app.AppCompatActivity;

import org.json.JSONException;

public class MainActivity extends AppCompatActivity implements View.OnClickListener, AsyncInterface <ResponseClass> {

    Button YesBtn;
    Button NoBtn;
    Button LoginButton;

    TextView LoginText;
    EditText LoginField;

    TextView PasswordText;
    EditText PasswordField;

    TextView ErrorText;
    ProgressBar loginProgressbar;

    private String login;
    private String password;
    private boolean isYesNo;

    public static final String APP_PREFERENCES = "mysettings";
    SharedPreferences mSettings;

    public static final String APP_PREFERENCES_LOGIN = "Login";
    public static final String APP_PREFERENCES_PASS = "Pass";
    SharedPreferences.Editor editor;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        LoginText = findViewById(R.id.text_enter_login);
        PasswordText = findViewById(R.id.text_enter_password);
        LoginField = findViewById(R.id.enter_login);
        PasswordField = findViewById(R.id.enter_password);

        loginProgressbar = findViewById(R.id.login_progressbar);
        ErrorText = findViewById(R.id.error_text);

        YesBtn = findViewById(R.id.choose_yes);
        YesBtn.setOnClickListener(this);
        NoBtn = findViewById(R.id.choose_no);
        NoBtn.setOnClickListener(this);
        LoginButton = findViewById(R.id.login_button);
        LoginButton.setOnClickListener(this);

        mSettings = getSharedPreferences(APP_PREFERENCES, Context.MODE_PRIVATE);

        LoadParams();

        try {
            if(!login.equals("")){
                GetServerResponse();
            }
        }
        catch (NullPointerException e){

        }
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.choose_yes:
                PasswordText.setVisibility(View.VISIBLE);
                PasswordField.setVisibility(View.VISIBLE);
                LoginField.setText("");
                LoginText.setText(R.string.Enter_Login);
                PasswordField.setText("");
                PasswordText.setText(R.string.Enter_Password);
                LoginButton.setText(R.string.Exit_button);
                isYesNo = true;
                ChangeVisibility();
                break;
            case R.id.choose_no:
                PasswordText.setVisibility(View.VISIBLE);
                PasswordField.setVisibility(View.VISIBLE);
                LoginField.setText("");
                LoginText.setText(R.string.Enter_Login);
                PasswordField.setText("");
                PasswordText.setText(R.string.Enter_Password);
                LoginButton.setText(R.string.Login_button);
                isYesNo = false;
                ChangeVisibility();
                break;
            case R.id.login_button:
                if(isYesNo){
                    login = LoginField.getText().toString();
                    password = PasswordField.getText().toString();
                    GetServerResponse();

                }
                else{
                    login = LoginField.getText().toString();
                    password = PasswordField.getText().toString();
                    GetServerResponse1();
                }
                ErrorText.setText("");
                break;

        }
    }

    private void LoadParams(){
        if(mSettings.contains(APP_PREFERENCES_LOGIN)) {
            login = mSettings.getString(APP_PREFERENCES_LOGIN, "");
        }
        if(mSettings.contains(APP_PREFERENCES_PASS)) {
            password = mSettings.getString(APP_PREFERENCES_PASS, "");
        }
    }

    private void GetServerResponse(){
        try {
            LoginRepo loginRepo = new LoginRepo(login, password, loginProgressbar, this);
            loginRepo.execute();
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
    }
    private void GetServerResponse1(){
        try {
            RegRepo regRepo = new RegRepo(login, password, loginProgressbar, this);
            regRepo.execute();
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    private void ChangeVisibility(){
        LoginText.setVisibility(View.VISIBLE );
        LoginField.setVisibility(View.VISIBLE);
        LoginButton.setVisibility(View.VISIBLE);
    }

    @Override
    public void onAsyncTaskFinished(ResponseClass responseClass) {
        try {
            if(responseClass.getStatus().equals("OK")) {
                if (responseClass.getResponseString().equals("OK")) {
                    editor = mSettings.edit();
                    editor.putString(APP_PREFERENCES_LOGIN, this.login);
                    editor.putString(APP_PREFERENCES_PASS, this.password);
                    editor.apply();
                    Intent intent = new Intent(this, MainMenu.class);
                    intent.putExtra("login", this.login);
                    startActivity(intent);
                }
            }
            else {
                switch (responseClass.getResponseString()) {
                    case "WRONG_LOGIN":
                            ErrorText.setText(R.string.Login_error);

                        ErrorText.setVisibility(View.VISIBLE);
                        break;
                    case "WRONG_PASSWORD":
                        ErrorText.setText(R.string.Password_error);
                        ErrorText.setVisibility(View.VISIBLE);
                        break;
                }
            }
        }
        catch (NullPointerException e){
            ErrorText.setText(R.string.Server_error);
            ErrorText.setVisibility(View.VISIBLE);
        }
        loginProgressbar.setVisibility(View.INVISIBLE);
    }
}
