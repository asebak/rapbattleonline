package Adapters;
/*Rap Battle Online: Class PrivateMessageAdapter*/

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.rapbattleonline.app.R;

import java.text.MessageFormat;
import java.util.List;

import Core.PrivateMessage;

public class PrivateMessageAdapter extends ArrayAdapter<PrivateMessage> {
    private int resource;
    public PrivateMessageAdapter(Context context, int resource, List<PrivateMessage> objects) {
        super(context, resource, objects);
        this.resource = resource;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent){
        View pmView = convertView;
        PrivateMessage pm = getItem(position);
        String from = pm.getSentBy();
        String subject = pm.getSubject();
        int dateSent = pm.getDateSent();
        if(convertView == null){
           LayoutInflater li = LayoutInflater.from(getContext());
           pmView = li.inflate(this.resource, null);
        }
        TextView sentByView = (TextView)pmView.findViewById(R.id.from);
        TextView subjectView = (TextView)pmView.findViewById(R.id.subject);
        TextView dateSentView = (TextView)pmView.findViewById(R.id.datesent);
        sentByView.setText(MessageFormat.format("{0}: {1}", getContext().getResources().getString(R.string.from), from));
        subjectView.setText(MessageFormat.format("{0}: {1}", getContext().getResources().getString(R.string.subject), subject));
        dateSentView.setText(MessageFormat.format("{0} {1}.",dateSent, getContext().getResources().getString(R.string.daysAgo)));
        return pmView;
    }
}
