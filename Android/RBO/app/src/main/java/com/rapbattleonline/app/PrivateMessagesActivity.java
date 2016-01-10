package com.rapbattleonline.app;

import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.ProgressBar;

import com.loopj.android.http.JsonHttpResponseHandler;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import Adapters.PrivateMessageAdapter;
import Core.AndroidContext;
import Core.PrivateMessage;
import Core.Profile;
import WebApi.WebApiClient;


public class PrivateMessagesActivity extends ActionBarActivity {
    private ProgressBar privateMsgsBar;
    private ListView pmListView;
    private ArrayList<PrivateMessage> pmList = new ArrayList<PrivateMessage>();
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.privatemessages);
        this.privateMsgsBar = (ProgressBar)this.findViewById(R.id.progressBar);
        this.pmListView = (ListView)this.findViewById(R.id.pmListView);
        this.readPrivateMessages();
        this.bindData();

    }

    private void readPrivateMessages(){
        this.privateMsgsBar.setVisibility(View.VISIBLE);
        WebApiClient.get(getBaseContext(), "PrivateMessages/", new JsonHttpResponseHandler(){
            @Override
            public void onSuccess(JSONArray pmArray) {
                if(pmArray != null){
                    for(int i = 0; i < pmArray.length(); i++){
                        PrivateMessage pm = new PrivateMessage();
                        try {
                            JSONObject o = pmArray.getJSONObject(i);
                            pm.setDetails(o.getString("Details"));
                            pm.setMessageId(o.getInt("MessageId"));
                            pm.setUserId(o.getInt("UserId"));
                            pm.setTo(o.getString("To"));
                            pm.setSentBy(o.getString("SentBy"));
                            pm.setSubject(o.getString("Subject"));
                            pm.setDateSent(o.getInt("DateSent"));
                            pm.setIsDeleted(o.getBoolean("IsDeleted"));
                            pm.setIsArchived(o.getBoolean("IsArchived"));
                            pm.setIsReply(o.getBoolean("IsReply"));
                            pm.setIsInOutbox(o.getBoolean("IsInOutBox"));
                            pm.setIsRead(o.getBoolean("IsRead"));
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                        pmList.add(pm);
                    }
                    bindData();
                }
                privateMsgsBar.setVisibility(View.GONE);
                Log.d("Web Api", "PM List Received");
            }

            @Override
            @SuppressWarnings("deprecation")
            public void onFailure(Throwable e, String response) {
                privateMsgsBar.setVisibility(View.GONE);
                Log.e("Web Api", "PM List Failed", e);
            }
        });
    }

    private void bindData(){
        PrivateMessageAdapter pmAdapter = new PrivateMessageAdapter(this, R.layout.pmitemslist, this.pmList);
        this.pmListView.setAdapter(pmAdapter);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.private_messages, menu);
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
