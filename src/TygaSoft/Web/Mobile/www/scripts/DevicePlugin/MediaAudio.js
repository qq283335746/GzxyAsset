var MediaAudio = {
    PlayAudio: function (src) {
        src = cordova.file.applicationDirectory + 'www/' + src;
        //src = '/android_asset/www/' + src;
        var mediaRes = new Media(src,
           function onSuccess() {
               // release the media resource once finished playing
               mediaRes.release();
           },
           function onError(e) {
               console.log("error playing sound: " + JSON.stringify(e));
           });
        mediaRes.play();
    }
}