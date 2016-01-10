<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedManager.ascx.cs" Inherits="FreestyleOnline.Controls.Admin.FeaturedManager" %>
<%@ Register TagPrefix="RAP" TagName="UsersComboBox" Src="~/Controls/Generic/UsersComboBox.ascx" %>
<div class="row clearfix">
    <div class="col-md-12 column">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%=this.Text("ADMIN", "FEATURE_USER") %>
                </h3>
            </div>
            <div class="panel-body">
                <RAP:UsersComboBox ID="FeaturedUsers" runat="server" />
                <br />
                <ext:DateField Cls="form-control" Width="400" AutoDataBind="true" FieldLabel='<%#this.Text("ADMIN", "FEATUREDMANAGER_UNTIL") %>' ID="DateForProfileFeature" runat="server" />
            </div>
            <div class="panel-footer">
                <ext:Button Cls="btn btn-default" AutoDataBind="true" ID="FeaturedProfile" AutoPostBack="true" Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' runat="server" OnClick="FeaturedProfile_Click" />
            </div>
        </div>
    </div>
</div>
<br />
<div class="row clearfix">
    <div class="col-md-12 column">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%=this.Text("ADMIN", "FEATURE_MUSIC") %>
                </h3>
            </div>
            <div class="panel-body">
                <ext:ComboBox AutoDataBind="true" Cls="form-control" Width="400" PageSize="20" FieldLabel='<%#this.Text("MUSIC", "MUSIC_TRACK") %>' ID="MusicTextbox" DisplayField="TrackTitle" Editable="false"
                    EmptyText='<%#this.Text("MUSIC", "MUSIC_SELECT") %>' ForceSelection="true" QueryMode="Local" runat="server" TriggerAction="All" ValueField="MusicID">
                    <Store>
                        <ext:Store PageSize="20" IsPagingStore="true" ID="FeatureMusicStore" runat="server">
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="TrackTitle" />
                                        <ext:ModelField Name="DisplayName" />
                                        <ext:ModelField Name="MusicID" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ListConfig Width="320" Height="300" ItemSelector=".x-boundlist-item">
                        <Tpl runat="server">
                            <Html>
                                <tpl for=".">
                    <tpl if="[xindex] == 1">
                        <table>
                        <tr>
                            <th>Display Name</th>
                            <th>Music Track</th>
                        </tr>
                    </tpl>
                    <tr class="x-boundlist-item">
                        <td>{DisplayName}</td>
                        <td>{TrackTitle}</td>
                    </tr>
                    <tpl if="[xcount-xindex]==0">
                    </table>
                    </tpl>
                </tpl>
                            </Html>
                        </Tpl>
                    </ListConfig>
                </ext:ComboBox>
                <br />
                <ext:DateField Cls="form-control" Width="400" AutoDataBind="true" FieldLabel='<%#this.Text("ADMIN", "FEATUREDMANAGER_UNTIL") %>' ID="DateForMusicFeature" runat="server" />
            </div>
            <div class="panel-footer">
                <ext:Button Cls="btn btn-default" AutoDataBind="true" AutoPostBack="true" ID="FeaturedTrack" Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' runat="server" OnClick="FeaturedTrack_Click" />
            </div>
        </div>
    </div>
</div>
