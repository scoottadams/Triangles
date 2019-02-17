function getCoords() {
    console.log("getCoords started");

    const selectedLetter = $("#letters").val();
    const selectedNumber = $("#numbers").val();

    $.ajax({
        url: "/Home/GetTriangleFromRef",
        async: true,
        method: "post",
        data: { letter: selectedLetter, number: selectedNumber },
        beforeSend: function(xhr) {
            if (xhr.overrideMimeType) {
                xhr.overrideMimeType("application/json");
            }
        },
        success: function(data) {
            console.log(data);
            $("#stringResult").text(data.CoordsString);
            $("#imageResult").attr("src", data.Image);
        },
        error: function(xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getRef() {
    console.log("getRef started");

    try {
        const v1X = $("#v1x").val();
        const v1Y = $("#v1y").val();

        const v2X = $("#v2x").val();
        const v2Y = $("#v2y").val();

       if (v1X === "" || v1Y === "")
            throw "Right angle coords are missing";

        if (v2X === "" || v2Y === "")
            throw "Top left coords are missing";

        $.ajax({
            url: "/Home/GetTriangleFromCoords",
            async: true,
            method: "post",
            data: { v1X, v1Y, v2X, v2Y},
            beforeSend: function(xhr) {
                if (xhr.overrideMimeType) {
                    xhr.overrideMimeType("application/json");
                }
            },
            success: function(data) {
                console.log(data);
                $("#stringCoords").text(data.Ref);
                $("#imageResult").attr("src", data.Image);
            },
            error: function(xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });

    } catch (err) {
        console.log(err);
        return;
    }
}