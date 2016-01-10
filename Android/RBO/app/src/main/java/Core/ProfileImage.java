package Core;
/*Rap Battle Online: Class ProfileImage*/

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.widget.ImageView;

import com.loopj.android.http.BinaryHttpResponseHandler;

import WebApi.WebApiClient;

public class ProfileImage {
    public static void getImageData(String avatarPath, final ImageView avatar){
        try {
            WebApiClient.get(WebApiClient.BASE_ADDRESS + avatarPath, null, new BinaryHttpResponseHandler(new String[] { "image/png", "image/jpeg" }) {
                @Override
                public void onSuccess(byte[] responseData) {
                    Bitmap image = BitmapFactory.decodeByteArray(responseData, 0, responseData.length);
                    avatar.setImageBitmap(image);
                }
            });
        }
        catch(Throwable e){
            Log.wtf("Image", e);
        }
    }
}
