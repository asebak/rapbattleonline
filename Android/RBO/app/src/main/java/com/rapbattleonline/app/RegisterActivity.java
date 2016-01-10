package com.rapbattleonline.app;

import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.loopj.android.http.AsyncHttpResponseHandler;
import com.loopj.android.http.JsonHttpResponseHandler;

import org.apache.http.Header;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;

import Core.AndroidContext;
import Core.Profile;
import Core.Register;
import Helpers.Validation;


public class RegisterActivity extends ActionBarActivity {

    private Button registerBtn;
    private Button backBtn;
    private ProgressBar registerBar;
    private EditText username;
    private EditText password;
    private EditText email;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.register);
        this.registerBtn = (Button)this.findViewById(R.id.registerBtn);
        this.backBtn = (Button)this.findViewById(R.id.backBtn);
        this.registerBar = (ProgressBar)this.findViewById(R.id.registerBar);
        this.username = (EditText)this.findViewById(R.id.usernameTxt);
        this.password = (EditText)this.findViewById(R.id.passwordTxt);
        this.email = (EditText)this.findViewById(R.id.emailTxt);
        this.registerBar.setVisibility(View.GONE);
        this.backBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v){
                goBack();
            }
        });
        this.registerBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(TextUtils.isEmpty(username.getText().toString())){
                    username.setError(getResources().getString(R.string.noUsername));
                    return;
                }
                if(TextUtils.isEmpty(password.getText().toString())){
                    password.setError(getResources().getString(R.string.noPassword));
                    return;
                }
                if(TextUtils.isEmpty(email.getText().toString()) || !Validation.Email(email.getText().toString())){
                    email.setError(getResources().getString(R.string.noEmail));
                    return;
                }
                registerBar.setVisibility(View.VISIBLE);
                registerBtn.setEnabled(false);
                backBtn.setEnabled(false);
                try {
                    startRegisterTask();
                } catch (UnsupportedEncodingException e) {
                    e.printStackTrace();
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void startRegisterTask() throws UnsupportedEncodingException, JSONException {
        Register registerManager = new Register(this.username.getText().toString(), this.password.getText().toString(), this.email.getText().toString());
        registerManager.Begin(getBaseContext(),  new AsyncHttpResponseHandler() {
            @Override
            public void onSuccess(int statusCode, Header[] headers, byte[] responseBody) {
                Log.d("Web Api", "User Registered");
                registerBar.setVisibility(View.GONE);
                registerBtn.setEnabled(true);
                backBtn.setEnabled(true);
                Toast.makeText(getApplicationContext(), getResources().getString(R.string.successRegitration), Toast.LENGTH_LONG).show();
                goBack();
            }
            @Override
            public void onFailure(int statusCode, Header[] headers, byte[] responseBody, Throwable e)
            {
                registerBar.setVisibility(View.GONE);
                registerBtn.setEnabled(true);
                backBtn.setEnabled(true);
                Toast.makeText(getApplicationContext(), getResources().getString(R.string.errorRegistration), Toast.LENGTH_LONG).show();
                Log.e("Web Api", "Registration Failed!", e);
            }
        });
    }

    private void goBack(){
        super.onBackPressed();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.register, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
}
