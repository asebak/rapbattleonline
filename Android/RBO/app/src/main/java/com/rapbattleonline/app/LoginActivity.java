package com.rapbattleonline.app;

import android.content.Intent;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.text.TextUtils;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.loopj.android.http.JsonHttpResponseHandler;

import org.json.JSONException;
import org.json.JSONObject;
import org.w3c.dom.Text;

import java.io.UnsupportedEncodingException;
import java.text.MessageFormat;
import java.util.Calendar;

import Core.AndroidContext;
import Core.Login;
import Core.Profile;


public class LoginActivity extends ActionBarActivity {

    private ProgressBar loginBar;
    private EditText username;
    private EditText password;
    private Button loginBtn;
    private TextView register;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login);
        this.loginBtn = (Button) this.findViewById(R.id.login);
        this.username = (EditText) this.findViewById(R.id.username);
        this.password = (EditText) this.findViewById(R.id.password);
        this.loginBar = (ProgressBar) this.findViewById(R.id.loginProgressBar);
        this.register = (TextView) this.findViewById(R.id.signUp);
        this.loginBar.setVisibility(View.GONE);
        this.register.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent signUp = new Intent(getApplicationContext(), RegisterActivity.class);
                startActivity(signUp);
            }
        });
        this.loginBtn.setOnClickListener(new View.OnClickListener() {
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
                    loginBar.setVisibility(View.VISIBLE);
                    loginBtn.setEnabled(false);
                try {
                    startLoginTask();
                } catch (UnsupportedEncodingException e) {
                    e.printStackTrace();
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
        TextView copyright = (TextView) this.findViewById(R.id.copyright);
        String copyrightDetails = MessageFormat.format("{0} 2013 - {1}, {2}", getResources().getString(R.string.copyright), Calendar.getInstance().get(Calendar.YEAR), getResources().getString(R.string.app_name));
        copyright.setText(copyrightDetails);
        WifiManager wifiManager = (WifiManager) this.getBaseContext().getSystemService(getBaseContext().WIFI_SERVICE);
        wifiManager.setWifiEnabled(true);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.login, menu);
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

    private void startLoginTask() throws UnsupportedEncodingException, JSONException {
        //can be username or email
        Login loginManager = new Login(username.getText().toString(), password.getText().toString());
        //multiple onsuccess and onfailures used bug in their library.
        loginManager.Authenticate(getBaseContext(), new JsonHttpResponseHandler() {
            @Override
            public void onStart() {
                Log.d("Web Api", "Start Loggin In");
            }

            @Override
            public void onSuccess(JSONObject profileContext) {
                //Get Forms Authentication Cookie From Response
                Log.d("Web Api", "Logged In");
                Profile p = new Profile();
                try {
                    p.setHomepage(profileContext.getString("Homepage"));
                    p.setFacebookId(profileContext.getString("FacebookId"));
                    p.setAim(profileContext.getString("AIM"));
                    p.setSkype(profileContext.getString("Skype"));
                    p.setAvatar(profileContext.getString("Avatar"));
                    p.setUserName(profileContext.getString("UserName"));
                    p.setUserId(profileContext.getInt("UserId"));
                    p.setFacebook(profileContext.getString("Facebook"));
                    p.setCountry(profileContext.getString("Country"));
                    p.setCity(profileContext.getString("City"));
                    p.setOccupation(profileContext.getString("Occupation"));
                    p.setUnreadMessages(profileContext.getInt("UnreadMessages"));
                    p.setMsn(profileContext.getString("MSN"));
                    p.setInterests(profileContext.getString("Interests"));
                    p.setTwitter(profileContext.getString("Twitter"));
                    p.setTwitterId(profileContext.getString("TwitterId"));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
                //set profile context
                AndroidContext.getInstance().setCurrentProfile(p);
                goToLoggedInActivity();

            }

            @Override
            @SuppressWarnings("deprecation")
            public void onFailure(Throwable e, String response) {
                Log.e("Web Api", "Login Failed!", e);
                loginError(e);
            }

            @Override
            public void onFinish() {
                Log.d("Web Api", "Login Finished!");
            }
        });
    }

    private void goToLoggedInActivity() {
        this.loginBar.setVisibility(View.GONE);
        loginBtn.setEnabled(true);
        Intent loggedIn = new Intent(getApplicationContext(), LoggedinActivity.class);
        this.startActivity(loggedIn);
    }

    private void loginError(Throwable e) {
        loginBtn.setEnabled(true);
        this.loginBar.setVisibility(View.GONE);
        Toast.makeText(getApplicationContext(), getResources().getString(R.string.invalidCredentials), Toast.LENGTH_LONG).show();
    }
}
