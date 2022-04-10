$("#subscribeForm").submit(function (e) {

    e.preventDefault(); // avoid to execute the actual submit of the form.

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(), // serializes the form's elements.
        success: function (data) {
            $("#IsSubscribed").val(data);
            toggleSubscribe(data);
        }
    });

});

$("#commentForm").submit(function (e) {

    e.preventDefault(); // avoid to execute the actual submit of the form.

    if ($('#CommentInput').val() === "") {
        $('#commentErr').text("Cannot post empty comment");
        return;
    } else {
        $('#commentErr').text("");
    }

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(), // serializes the form's elements.
        success: function () {
            window.location.reload();
        }
    });

});

function toggleSubscribe() {
    var isSubscribed = $("#IsSubscribed").val();

    const subBtn = $("#subscribeBtn");
    if (isSubscribed === 'true') {
        subBtn.addClass("btn-outline-secondary");
        subBtn.removeClass("btn-secondary");
        subBtn.text("Unsubscribe");
    } else {
        subBtn.addClass("btn-secondary");
        subBtn.removeClass("btn-outline-secondary");
        subBtn.text("Subscribe");
    }
}

toggleSubscribe();

function toggleStars(starId, rating) {
    const numRating = Math.round(parseFloat(rating));
    for (let i = 1; i < 6; i++) {
        const star = $('#' + starId + i);

        if (numRating >= i) {
            star.addClass("fas");
            star.removeClass("far");
        } else {
            star.addClass("far");
            star.removeClass("fas");
        }
    }
}

toggleStars("inputStar", userRating);
toggleStars("averageStar", averageRating);

$('#inputStarDiv').mouseleave(() => {
    toggleStars("inputStar", userRating);
});

$('#inputStar1').click(() => {
    sendRating(1);
});
$('#inputStar1').mouseenter(() => {
    toggleStars("inputStar", 1);
});


$('#inputStar2').click(() => {
    sendRating(2);
});
$('#inputStar2').mouseenter(() => {
    toggleStars("inputStar", 2);
});


$('#inputStar3').click(() => {
    sendRating(3);
});
$('#inputStar3').mouseenter(() => {
    toggleStars("inputStar", 3);
});


$('#inputStar4').click(() => {
    sendRating(4);
});
$('#inputStar4').mouseenter(() => {
    toggleStars("inputStar", 4);
});


$('#inputStar5').click(() => {
    sendRating(5);
});
$('#inputStar5').mouseenter(() => {
    toggleStars("inputStar", 5);
});


function sendRating(rating) {
    $.ajax({
        type: "POST",
        url: rateURL,
        data: { bookId: bookId, rating: rating },
        success: function (data) {
            if (data === '301') {
                window.location.href = loginRedirect;
                return;
            }
            window.location.reload();
        }
    });
}