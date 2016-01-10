#Rap Battle Online: HipHop Just Got Serious

Contains the Following code:
- Backend Server implementation done using ASP.net and C# 4.5
- Front End implementation using various HTML5 Libraries using SAP UI5, KnockoutJS, SignalR, WebRTC
- Android Client App
- Silverlight Client App
- Windows Phone Client App

FEATURES
- Audio Rap Battles.
- Written Rap Battles.
- News Commenting/Posting.
- Ability to create and join Hoods.
- Ability to add your own music tracks.
- Rating system
- Rap Tournaments
- Leaderboards and stats.
- Real-Time notification system.

REQUIREMENTS

- Visual Studio 2012/2013
- SQL Server database (local instance can be used)
- Microsoft Code Contracts
- Silverlight SDK
- Windows Phone SDK
- Android SDK
- Android Studio

DATABASE

- Database setup is in db.config file.
- In App_data folder there is an empty db file that you can replace with so you can reinstall the application.
- To query from a database easily. hook an SqlDataSource Control from the tool box to the databound item such as a repeater, gridview etc.
- When creating a table make it a in the .SQL files Write all queries in the custom.sql file


CONTROLS

- To make a custom control, just add a web user control to the controls folder
- C# file inherits from BaseUserControl not the System.web.controls.ui one
- ASPX file is fine, no adjustments need to be made.
- All controls need a variable public int UserID { get; set;} to easily get user context
- Control must validate with this.PageContext.PageUserId


PAGES

- Add a web user control to the pages folder
- C# inherit from ForumPage or ForumPageRegistered (if inside a profile)
- Add page to the enum ForumPages, recompile the yaf solution with the new DLLs (about 8 or 9 dependant dlls)


Timer Control

- Invoked by calling writing this method on the page

```
protected override void OnPreRender(EventArgs e)
{
            ObjectID.TimeToEnd = new DateTime(Year, Month, Day); [this works perfectly]
			OR
			ObjectID.TimeToEnd = new DateTime.addHours(1.02); [issues with this]
}
```
CSS3:
Get the ID defined in the sapui5 js code
to override the library use the flag !important
you can override sap ui5 library by using .classname and your implementation
Follow UI5 LIBRARY BUTTONS from asp.net should inherit the following classes:
CssClass="sapUiBtn sapUiBtnNorm sapUiBtnS sapUiBtnStd"

URL Rewriting:
Done with ASP.NET Friendly URLS,
to enable in global.asax  RouteTable.Routes.EnableFriendlyUrls();
Response.redirect to any page without aspx extension. 
static files will be lost need to put /folder path to fix that problem

CODE REFACTORING:
- Replace all tags with english.xml file
- Make more reusable code(battlevotedisplay)
- Inject javascript code behind as minimized
- Optimize performance for HTML output rendering

Debugging Mobile Device with ASP.NET
- open folder in my documents iisexpress/config/apphost.config
- find site and add a new binding copy and paste the localhost one and put the computers ip address instead
- launch asp.net site and phone emulator, put computer ip address and port to test.
- the wcf service end point has to be the comptuer's ip address not the localhost one'
- when added a wcf service add the normal wcf service and not the silverlight enabled one, delete the interface and just leave the svc file.
- All windows phone services have to enablecookies and have to have basic bindings, they do not need to go in web.config only in client configuration file
- when updating a service, you have to delete the enable cookies string then it will work and then rewrite it in(MS bug)
- Both asp.net and windows phone emulator have to be running in debug mode simulatenously to debug them at the same time.

For V1 To Complete(Server):
- Social Feed
- Statistics
- Optimize Mobile HTML5 site with extra controls and pages to help out mobile devices( mostly done but things such as audio and written battles need to be worked on).
- Real Time Cyphers(w.i.p) with webrtc and signalR mostly done but only works for 1 on 1 cyphers and not 1 to many.

Features to Look At:
- More Customizable Hoods and profiles, moving layouts around, custom background images etc like DA has.
- Native Mobile Apps: Blackberry, W8, WP8, and Android

FEATURES FOR V2:

- Signal Processing and Syncronization for Audio Tracks
- 2 v 2 rap battles
- More customization for hoods, like images, layout etc.

Major Bugs:

- private hoods cannot be joined, private battles need to be done at all even if the memeber is invited to join.
- autopostback is broken on profile page for some 
