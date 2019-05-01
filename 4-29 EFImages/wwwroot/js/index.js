$(() => {

    $.get("/home/getimages", function (images) {
        console.log(images);
        $("#images").empty();
        images.forEach(l => {
            console.log(l.fileName);
            console.log(l.id);
            console.log(l.title);
            console.log(l.fileName);
            $("#images").append(`
<div class="col-md-3 col">
        <div class="thumbnail" style="width: 200px; height:275px">
            <a href="/home/viewimage?id=${l.id}" class="btn btn-link btn-lg">${l.title}</a>
            <a href="/home/viewimage?id=${l.id}">
                <img src="/Uploads/${l.fileName}" style="width: 200px; height: 150px" />
            </a>
            <br />
            <button class="btn btn-primary like" data-id="${l.id}">Like</button>
            <label>${l.likes}</label>
</div>
        </div>
    `)
        });
    });

    $("#images").on('click', '.like', function () {
        const id = $(this).data('id');

        $.post("/home/addlike", { id }, function () {

        });
        $(this).attr('disabled', true);
    });

    setInterval(() => {
        $.get("/home/getimages", function (images) {
           
            $("#images").empty();
            images.forEach(l => {
                console.log(l);
                $("#images").append(`
<div class="col-md-3 col">
        <div class="thumbnail" style="width: 200px; height:275px">
            <a href="/home/viewimage?id=${l.id}" class="btn btn-link btn-lg">${l.title}</a>
            <a href="/home/viewimage?id=${l.id}">
                <img src="/Uploads/${l.fileName}" style="width: 200px; height: 150px" />
            </a>
            <br />
            <button class="btn btn-primary like" id="like${l.id}" data-id="${ l.id }">Like</button>
            <label>${l.likes}</label>
</div>
        </div>
    `)
                if (l.disabled ) {
                    $(`#like${l.id}`).attr('disabled', true);
                    console.log('dis');
                }
            });
        });
    }, 5000)
});