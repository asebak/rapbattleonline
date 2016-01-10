/*Invoked When user wants to download a music file*/
function onDownloadClick(musicId) {
    window.open("/api/Download/Music/" + musicId);
}