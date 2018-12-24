var ckEditor;
ClassicEditor
    .create(document.querySelector('.editor'))
    .then(editor => {
        console.log('CKEditor initialised', editor);
        ckEditor = editor;
    })
    .catch(error => {
        console.error(error);
    });

function siteResourcesOnClick() {
    $("#ResourceGalleryTabContent").click(function (event) {
        var elemClass = $(event.target).attr('class');

        if (elemClass.includes("img-thumbnail")) {
            //Only apply if image was clicked
            event.preventDefault();
            $('#ResourceGalleryModal').modal('hide');

            var imgUrl = $(event.target).attr('src');
            var imgAlt = $(event.target).attr('alt');

            const content = '<img src="' + imgUrl + '" class="img-fluid" alt="' + imgAlt + '" />';
            const viewFragment = ckEditor.data.processor.toView(content);
            const modelFragment = ckEditor.data.toModel(viewFragment);

            ckEditor.model.insertContent(modelFragment, ckEditor.model.document.selection);
        }
    });
}

function siteUrlsOnClick() {
    $(".siteUrls").click(function (event) {
        event.preventDefault();

        var linkUrl = $(event.target).attr('href');
        var linkText = $(event.target).html();

        ckEditor.model.change(writer => {
            const insertPosition = ckEditor.model.document.selection.getFirstPosition();
            writer.insertText(linkText, { linkHref: linkUrl }, insertPosition);
        });
    });
}

function emojisOnClick() {
    $(".Emojis").click(function (event) {
        var emoji = $(event.target).text();

        if ($(event.target).attr('class').includes("Emoji")) {
            //Ignore clicks outside or between Emojis
            ckEditor.model.change(writer => {
                const insertPosition = ckEditor.model.document.selection.getFirstPosition();
                writer.insertText(emoji, insertPosition);
            });
        }
    });
}

$("#AddSiteResourcesForm").submit(function (event) {
    event.preventDefault();

    var uploadFolder = $("#UploadFolder").val();

    $("#FileUploadModal").modal('hide');
    $("#UploadProgressModalCloseButton").hide();
    $("#UploadProgressModal").modal('show');

    setProgressBar(0);

    var formData = new FormData();
    var fileSelect = document.getElementById('FilesToUpload');
    var files = fileSelect.files;

    // Loop through each of the selected files.
    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        var progressPercent = Math.round(100 / files.length * (i + 1));

        // Check the file type.
        if (!file.type.match('image.*')) {
            continue;
        }

        // Add the file to the request.
        formData.append('files', file, file.name);
        setProgressBar(progressPercent);
    }

    formData.append('__RequestVerificationToken', $('input[name=__RequestVerificationToken]').val());
    formData.append('subfolder', uploadFolder);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/FileUpload/UploadFiles/1', true);

    // Set up a handler for when the request finishes.
    xhr.onload = function () {
        if (xhr.status === 200) {
            // File(s) uploaded.
            console.log(xhr.responseText);
            reloadSiteResourcesGallery();
            $("#UploadProgressModalCloseButton").show();
        } else {
            console.error(xhr.responseText);
        }
    };
    xhr.send(formData);
});

function reloadSiteResourcesGallery() {
    var uploadFolder = $("#UploadFolder").val();

    $("#ItemResources").load("/FileUpload/Files?subfolder=" + uploadFolder, function (event) {
        console.log('Item File Gallery Loaded', event);
    });
    $("#GlobalResources").load("/FileUpload/Files/?subfolder=SiteAssets", function (event) {
        console.log('Global File Gallery Loaded', event);
    });
}

function reloadEmojis() {
    $("#Emojis").load("/Emoji/", function (event) {
        console.log('Emoji Gallery Loaded', event);
    });
}

function setProgressBar(progressValue) {
    $("#FileUploadProgress").attr('aria-valuenow', progressValue);
    $("#FileUploadProgress").attr('style', 'width: ' + progressValue + '%');
    $("#FileUploadProgress").html(progressValue + '%');
}

$("#ResourceGalleryModal").on("shown.bs.modal", function () {
    reloadSiteResourcesGallery();
});

$("#EmojiModal").on("shown.bs.modal", function () {
    reloadEmojis();
});

siteResourcesOnClick();
siteUrlsOnClick();
emojisOnClick();