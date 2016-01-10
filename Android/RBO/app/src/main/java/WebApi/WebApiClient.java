package WebApi;

import android.content.Context;

import com.loopj.android.http.AsyncHttpClient;
import com.loopj.android.http.AsyncHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import org.apache.http.entity.StringEntity;
/*Rap Battle Online: Class WebApiClient*/

public class WebApiClient {
    public static final String BASE_ADDRESS = "http://192.168.2.23:56314/";
    public static final int TIME_OUT = 20 * 1000;
    private static AsyncHttpClient client = new AsyncHttpClient();

    public static void get(Context Request, String controllerUrl, AsyncHttpResponseHandler responseHandler) {
        client.get(Request, getApiPath(controllerUrl), responseHandler);
    }

    public static void getStream(String url, AsyncHttpResponseHandler responseHandler){
        client.get(BASE_ADDRESS + url, responseHandler);
    }

    public static void post(Context Request, String controllerPath, StringEntity payload, AsyncHttpResponseHandler responseHandler) {
        client.post(Request, getApiPath(controllerPath), payload, "application/json", responseHandler);
    }

    public static void put(Context Request, String controllerPath, StringEntity payload, AsyncHttpResponseHandler responseHandler) {
        client.put(Request, getApiPath(controllerPath), payload, "application/json", responseHandler);
    }

    public static void delete(Context Request, String controllerPath, AsyncHttpResponseHandler responseHandler) {
        client.delete(Request, getApiPath(controllerPath), responseHandler);
    }

    private static String getApiPath(String controllerUrl) {
        return BASE_ADDRESS + "api/" + controllerUrl;
    }
}
