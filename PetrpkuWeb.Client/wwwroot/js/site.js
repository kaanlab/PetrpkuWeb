function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

function stickyNav() {
    // When the user scrolls the page, execute myFunction 
    window.onscroll = function () { stickyNavFunction(); };

    // Get the navbar
    var navbar = document.getElementById("sticky-nav");

    // Get the offset position of the navbar
    var sticky = navbar.offsetTop;

    // Add the sticky class to the navbar when you reach its scroll position. Remove "sticky" when you leave the scroll position
    function stickyNavFunction() {
        if (window.pageYOffset >= sticky) {
            navbar.classList.add("sticky");
        } else {
            navbar.classList.remove("sticky");
        }
    }
}

function hlsPlayer() {
    var video = document.getElementById('video');
    if (Hls.isSupported()) {
        var hls = new Hls();
        hls.loadSource('/uploadfolder/stream/index.m3u8');
        hls.attachMedia(video);
        hls.on(Hls.Events.MANIFEST_PARSED, function () {
            video.play();
        });
    }
}

(function () {
    window.QuillFunctions = {
        createQuill: function (quillElement) {
            var options = {
                debug: 'info',
                modules: {
                    toolbar: '#toolbar'
                },
                placeholder: 'Compose an epic...',
                readOnly: false,
                theme: 'snow'
            };
            // set quill as the object we can call
            // methods on later
            quill = new Quill(quillElement, options);
        }
    };
})();