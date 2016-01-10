<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplaySitePost.ascx.cs" Inherits="FreestyleOnline.Controls.Generic.DisplaySitePost" %>
<%@ Register TagPrefix="RAP" TagName="ProfileLink" Src="~/Controls/ProfileLink.ascx" %>
<table class="displaycomment" width="100%"> 
    <tbody><tr class="postheader">
               <td width="140" class="postUser" colspan="2">
                   <a id="post1"></a><strong>
                   <a class="UserPopMenuLink"><%= UserMembershipHelper.GetDisplayNameFromID(this.UserId) %></a>
                    </strong>
               </td>
               <td width="80%" class="postPosted" colspan="2">
                   <div class="leftItem postedLeft">        
                       <strong><a href="#"><%= string.Format("#{0}", this.MessageId) %></a> Posted :</strong>
                       <YAF:DisplayDateTime runat="server" DateTime='<%#this.DatePosted %>'/>
                   </div>
                   <div class="rightItem postedRight" style="float: right;">
                       <YAF:ThemeButton ID="Retweet" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_RETWEET"
                                        TitleLocalizedTag="BUTTON_RETWEET_TT" OnClick="Retweet_Click" />
                       <YAF:ThemeButton ID="Delete" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_DELETE"
                                        TitleLocalizedTag="BUTTON_DELETE_TT" />
                       <YAF:ThemeButton ID="Quote" OnClick="Quote_Click" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_QUOTE"
                                        TitleLocalizedTag="BUTTON_QUOTE_TT" />
                   </div>
               </td>
           </tr>
        <tr class="post">
            <td valign="top" height="100" class="UserBox" colspan="2">
                <div class="yafUserBox">
                    <div class="section">
                        <RAP:ProfileLink UserId='<%#this.UserId %>' runat="server"/>
                    </div><br style="clear: both;" >
                    <div class="section">
                        <%--Rank: Administration<br style="clear: both;" >--%>
                    </div><br>
                    <div class="section">
                        <%-- TODO: Fix these values from behind --%>
                    <%--    Groups: Administrators                                                                                                                                                                                                                                                                   
                        <br style="clear: both;" >Joined: 5/21/2014<br>Posts: 0<br>--%>
                    </div><br>
                </div>
            </td>
            <td valign="top" class="message">
                <div class="postdiv">
                    <div style="display: block">
                        <asp:Label Text='<%#this.PostInformation %>' runat="server"/>            
                    </div> 
                </div>
            </td>
        </tr>
        <tr class="postfooter">
            <td class="small postTop" colspan="2">
                <a onclick=" ScrollToTop(); " class="postTopLink" href="javascript: void(0)">            
                    <YAF:ThemeImage LocalizedTitlePage="POSTS" LocalizedTitleTag="TOP"  runat="server" ThemeTag="TOTOPPOST" />
                </a>
                <span id="IPSpan1" class="rightItem postInfoRight" runat="server" visible="false"> 
                    &nbsp;&nbsp;
                    <b><%# this.GetText("IP") %>:</b>&nbsp;<a id="IPLink1" target="_blank" runat="server"/>			   
                </span> 	
            </td>
            <td class="postfooter postInfoBottom">
                <div class="displayPostFooter">
                    <div class="leftItem postInfoLeft">
                        <YAF:ThemeButton ID="btnTogglePost" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="TOGGLEPOST" Visible="false" />
                        <YAF:ThemeButton ID="Albums" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="ALBUM"
                                         TextLocalizedTag="ALBUMS" ImageThemeTag="ALBUMS" TitleLocalizedTag="ALBUMS_HEADER_TEXT" Visible="false" />
                        <YAF:ThemeButton ID="Pm" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="PM" ImageThemeTag="PM" TitleLocalizedTag="PM_TITLE" />
                        <YAF:ThemeButton ID="Email" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="EMAIL" ImageThemeTag="EMAIL" TitleLocalizedTag="EMAIL_TITLE" />
                        <YAF:ThemeButton ID="Home" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="HOME" ImageThemeTag="HOME" TitleLocalizedTag="HOME_TITLE" />
                        <YAF:ThemeButton ID="Blog" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="BLOG" ImageThemeTag="BLOG" TitleLocalizedTag="BLOG_TITLE" />
                        <YAF:ThemeButton ID="Msn" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="MSN" ImageThemeTag="MSN" Visible="false" TitleLocalizedTag="MSN_TITLE" />
                        <YAF:ThemeButton ID="Aim" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="AIM" ImageThemeTag="AIM" Visible="false" TitleLocalizedTag="AIM_TITLE" />
                        <YAF:ThemeButton ID="Yim" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="YIM" ImageThemeTag="YIM" Visible="false" TitleLocalizedTag="YIM_TITLE" />
                        <YAF:ThemeButton ID="Icq" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="ICQ" ImageThemeTag="ICQ" Visible="false" TitleLocalizedTag="ICQ_TITLE" />
                        <YAF:ThemeButton ID="Xmpp" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="XMPP" ImageThemeTag="XMPP" Visible="false" TitleLocalizedTag="XMPP_TITLE" />	
                        <YAF:ThemeButton ID="Skype" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="SKYPE" ImageThemeTag="SKYPE" Visible="false" TitleLocalizedTag="SKYPE_TITLE" />
                        <YAF:ThemeButton ID="Facebook" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="FACEBOOK" ImageThemeTag="Facebook2" TitleLocalizedTag="FACEBOOK_TITLE" />
                        <YAF:ThemeButton ID="Twitter" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="TWITTER" ImageThemeTag="Twitter2" TitleLocalizedTag="TWITTER_TITLE" />
                        <YAF:ThemeButton ID="Google" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="GOOGLE" ImageThemeTag="Google2" TitleLocalizedTag="GOOGLE_TITLE" TitleLocalizedPage="POSTS" />                  
                    </div>
                    <div class="rightItem postInfoRight">
                        <YAF:ThemeButton ID="ReportPost" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
                                         TextLocalizedTag="REPORTPOST" ImageThemeTag="REPORT_POST" TitleLocalizedTag="REPORTPOST_TITLE"></YAF:ThemeButton>	
                    </div>
                </div>
            </td>
        </tr>
        <tr class="postsep">
            <td colspan="3">
                <YAF:PopMenu runat="server" ID="PopMenu1" Control="UserName" />
            </td>
        </tr>
    </tbody></table>





<%--<tr class="postheader">		
    <%#GetIndentCell()%>
    <td width="140" id="NameCell" class="postUser" runat="server">
        <a id="post<%# DataRow["MessageID"] %>" /><strong>
            <YAF:OnlineStatusImage id="OnlineStatusImage" runat="server" Visible='<%# this.Get<YafBoardSettings>().ShowUserOnlineStatus && !UserMembershipHelper.IsGuestUser( DataRow["UserID"] )%>' Style="vertical-align: bottom" UserID='<%# (int)DataRow["UserID"] %>'  />
            <YAF:ThemeImage ID="ThemeImgSuspended" ThemePage="ICONS" ThemeTag="USER_SUSPENDED"  UseTitleForEmptyAlt="True" Enabled='<%# DataRow["Suspended"] != DBNull.Value && DataRow["Suspended"].ToType<DateTime>() > DateTime.UtcNow %>' runat="server"></YAF:ThemeImage>
            <YAF:UserLink  ID="UserProfileLink" runat="server" UserID='<%# (int)DataRow["UserID"]%>' ReplaceName='<%# this.Get<YafBoardSettings>().EnableDisplayName && (!DataRow["IsGuest"].ToType<bool>() || (DataRow["IsGuest"].ToType<bool>() && DataRow["DisplayName"].ToString() == DataRow["UserName"].ToString())) ? DataRow["DisplayName"] : DataRow["UserName"]%>' PostfixText='<%# DataRow["IP"].ToString() == "NNTP" ? this.GetText("EXTERNALUSER") : String.Empty %>' Style='<%#DataRow["Style"]%>' CssClass="UserPopMenuLink" EnableHoverCard="False" />
        </strong>
        &nbsp;<YAF:ThemeButton ID="AddReputation" CssClass='<%# "AddReputation_" + DataRow["UserID"]%>' runat="server" ImageThemeTag="VOTE_UP" Visible="false" TitleLocalizedTag="VOTE_UP_TITLE" OnClick="AddUserReputation"></YAF:ThemeButton>
        <YAF:ThemeButton ID="RemoveReputation" CssClass='<%# "RemoveReputation_" + DataRow["UserID"]%>' runat="server" ImageThemeTag="VOTE_DOWN" Visible="false" TitleLocalizedTag="VOTE_DOWN_TITLE" OnClick="RemoveUserReputation"></YAF:ThemeButton>
    </td>
    <td width="80%" class="postPosted" colspan='<%#GetIndentSpan()%>'>
        <div class="leftItem postedLeft">        
            <strong><a href='<%# YafBuildLink.GetLink(ForumPages.posts,"m={0}#post{0}",DataRow["MessageID"]) %>'>
                #<%# (CurrentPage * this.Get<YafBoardSettings>().PostsPerPage) + PostCount + 1%></a>
                <YAF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="POSTED" />
                :</strong>
            <YAF:DisplayDateTime id="DisplayDateTime" runat="server" DateTime='<%# DataRow["Posted"] %>'></YAF:DisplayDateTime>
            </div>
        <div class="rightItem postedRight">
            <YAF:ThemeButton ID="Retweet" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_RETWEET"
                TitleLocalizedTag="BUTTON_RETWEET_TT" OnClick="Retweet_Click" />
            <span id="<%# "dvThankBox" + DataRow["MessageID"] %>">
                <YAF:ThemeButton ID="Thank" runat="server" CssClass="yaflittlebutton" Visible="false" TextLocalizedTag="BUTTON_THANKS"
                    TitleLocalizedTag="BUTTON_THANKS_TT" />
            </span>        
            <YAF:ThemeButton ID="Attach" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_ATTACH"
                TitleLocalizedTag="BUTTON_ATTACH_TT" />
            <YAF:ThemeButton ID="Edit" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_EDIT"
                TitleLocalizedTag="BUTTON_EDIT_TT" />
            <YAF:ThemeButton ID="MovePost" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_MOVE"
                TitleLocalizedTag="BUTTON_MOVE_TT" />
            <YAF:ThemeButton ID="Delete" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_DELETE"
                TitleLocalizedTag="BUTTON_DELETE_TT" />
            <YAF:ThemeButton ID="UnDelete" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_UNDELETE"
                TitleLocalizedTag="BUTTON_UNDELETE_TT" />
            <YAF:ThemeButton ID="Quote" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_QUOTE"
                TitleLocalizedTag="BUTTON_QUOTE_TT" />
                <asp:CheckBox runat="server" ID="MultiQuote" CssClass="MultiQuoteButton"  />
        </div>
                
    </td>
</tr>
<tr class="<%#GetPostClass()%>">
    <td <%# GetRowSpan() %> valign="top" height="<%# GetUserBoxHeight() %>" class="UserBox" colspan='<%#GetIndentSpan()%>'>
        <YAF:UserBox id="UserBox1" runat="server" Visible="<%# !PostData.IsSponserMessage %>" PageCache="<%# PageContext.CurrentForumPage.PageCache %>" DataRow='<%# DataRow %>'></YAF:UserBox>
    </td>
    <td valign="top" class="message">
        <div class="postdiv">
            <asp:panel id="panMessage" runat="server">      
                <YAF:MessagePostData ID="MessagePost1" runat="server" DataRow="<%# DataRow %>" IsAltMessage="<%# this.IsAlt %>" ColSpan="<%#GetIndentSpan()%>" ShowEditMessage="True"></YAF:MessagePostData>
            </asp:panel> 
        </div>
    </td>
</tr>
<tr class="postfooter">
    <td class="small postTop" colspan='<%#GetIndentSpan()%>'>
        <a onclick="ScrollToTop();" class="postTopLink" href="javascript: void(0)">            
            <YAF:ThemeImage LocalizedTitlePage="POSTS" LocalizedTitleTag="TOP"  runat="server" ThemeTag="TOTOPPOST" />
        </a>
     <span id="IPSpan1" class="rightItem postInfoRight" runat="server" visible="false"> 
		&nbsp;&nbsp;
		<b><%# this.GetText("IP") %>:</b>&nbsp;<a id="IPLink1" target="_blank" runat="server"/>			   
	</span> 		
    </td>
		<td class="postfooter postInfoBottom">
			<YAF:DisplayPostFooter id="PostFooter" runat="server" DataRow="<%# DataRow %>"></YAF:DisplayPostFooter>
		</td>
</tr>
<tr class="<%#GetPostClass()%> postThanksRow">
    <td style="padding: 5px;" colspan="2" valign="top">
        <div id="<%# "dvThanksInfo" + DataRow["MessageID"] %>" class="ThanksInfo">
            <asp:Literal runat="server"  Visible="false" ID="ThanksDataLiteral"></asp:Literal></div>
    </td>
    <td class="message" style="padding: 5px;" valign="top">
        <div id="<%# "dvThanks" + DataRow["MessageID"] %>" class="ThanksList">
            <asp:Literal runat="server" Visible="false" ID="thanksDataExtendedLiteral"></asp:Literal>
        </div>
    </td>
</tr>
<tr class="postsep">
    <td colspan="3">
        <YAF:PopMenu runat="server" ID="PopMenu1" Control="UserName" />
    </td>
</tr>


</div>--%>