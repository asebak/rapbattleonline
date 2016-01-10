package Core;
/*Rap Battle Online: Class Login*/

import android.content.Context;

import com.loopj.android.http.AsyncHttpResponseHandler;

import org.apache.http.entity.StringEntity;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;

import WebApi.WebApiClient;

public class Login {
    private String username;
    private String password;

    public Login(String Username, String Password) {
        this.username = Username;
        this.password = Password;
    }

    public void Authenticate(Context Request, AsyncHttpResponseHandler loginHandler) throws  JSONException, UnsupportedEncodingException{
        JSONObject userData = new JSONObject();
        StringEntity loginEntity;
        userData.put("Username", this.username);
        userData.put("Password", this.password);
        loginEntity = new StringEntity(userData.toString());
        WebApiClient.post(Request, "Login/", loginEntity, loginHandler);
    }
}
