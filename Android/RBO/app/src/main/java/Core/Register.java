package Core;
/*Rap Battle Online: Class Register*/

import android.content.Context;

import com.loopj.android.http.AsyncHttpResponseHandler;

import org.apache.http.entity.StringEntity;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;

import WebApi.WebApiClient;

public class Register {
    private String userName;
    private String passWord;
    private String email;

    public Register(String userName, String passWord, String email) {
        this.userName = userName;
        this.passWord = passWord;
        this.email = email;
    }

    public void Begin(Context Request, AsyncHttpResponseHandler registerHandler) throws JSONException, UnsupportedEncodingException {
        JSONObject userData = new JSONObject();
        StringEntity registerEntity;
        userData.put("Username", this.userName);
        userData.put("Password", this.passWord);
        userData.put("Email", this.email);
        registerEntity = new StringEntity(userData.toString());
        WebApiClient.post(Request, "Register/", registerEntity, registerHandler);
    }
}
