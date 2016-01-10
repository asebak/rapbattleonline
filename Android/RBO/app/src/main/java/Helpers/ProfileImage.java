package Helpers;
/*Rap Battle Online: Class ProfileImage*/

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.widget.ImageView;

import com.loopj.android.http.BinaryHttpResponseHandler;

import org.apache.http.Header;

import WebApi.WebApiClient;

public class ProfileImage {
    public static void getImageData(String avatarPath, final ImageView avatar){

            WebApiClient.getStream(avatarPath, new BinaryHttpResponseHandler(new String[] { "image/png", "image/jpeg", "image/gif", "text/html" }) {
                @Override
                public void onSuccess(int statusCode, Header[] headers, byte[] binaryData) {
                    Bitmap image = BitmapFactory.decodeByteArray(binaryData, 0, binaryData.length);
                    avatar.setImageBitmap(image);
                }

                @Override
                public void onFailure(int statusCode, Header[] headers, byte[] binaryData, Throwable error) {
                    Log.e("Image", "onFailure!"+ error.getMessage());
                    for (Header header : headers)
                    {
                        Log.e("Image", header.getName() + " / "+ header.getValue());
                    }
                }
            });
    }
}
