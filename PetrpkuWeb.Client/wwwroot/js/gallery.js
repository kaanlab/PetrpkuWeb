window.gallery = {
    init: function () {
        const viewerElement = document.getElementById('lightgallery');
        lightGallery(viewerElement, {
            thumbnail: true,
            animateThumb: false,
            showThumbByDefault: false
        });
    }
};