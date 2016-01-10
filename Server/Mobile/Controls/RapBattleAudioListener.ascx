<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RapBattleAudioListener.ascx.cs" Inherits="FreestyleOnline.Mobile.Controls.RapBattleAudioListener" %>
<div class="row clearfix">
    <div class="col-md-6 column">
        <audio id="audioplayer1" controls runat="server">
            <source src='/api/audiobattle/audiobattlestream/<%=this.AudioPath1%>' type="audio/mpeg" />
        </audio>
        <asp:PlaceHolder ID="Recorder1Description" runat="server" />
    </div>
    <div class="col-md-6 column">
        <audio id="audioplayer2" controls runat="server">
            <source src='/api/audiobattle/audiobattlestream/<%=this.AudioPath2%>' type="audio/mpeg" />
        </audio>
        <asp:PlaceHolder ID="Recorder2Description" runat="server" />
    </div>
</div>
