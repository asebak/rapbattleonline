package com.rapbattleonline.app;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import java.text.MessageFormat;

import Core.AndroidContext;
import Helpers.ProfileImage;


public class LoggedinActivity extends ActionBarActivity {
    private ImageView avatar;
    private TextView welcomeMessage;
    private Button messages;
    private Core.Profile user = AndroidContext.getInstance().getCurrentProfile();
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.loggedin);
        this.avatar = (ImageView)this.findViewById(R.id.profileImage);
        ProfileImage.getImageData(user.getAvatar(), this.avatar);
        this.messages = (Button)this.findViewById(R.id.messagesBtn);
        this.welcomeMessage = (TextView)this.findViewById(R.id.welcomeMsg);
        this.welcomeMessage.setText(MessageFormat.format("{0}, {1}", getResources().getString(R.string.welecomeBack), user.getUserName()));
        this.messages.setText(MessageFormat.format("{0} {1}", user.getUnreadMessages(), getResources().getString(R.string.unreadMessages)));
        this.messages.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent privateMessages = new Intent(getApplicationContext(), PrivateMessagesActivity.class);
                startActivity(privateMessages);
            }
        });
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.loggedin, menu);
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
