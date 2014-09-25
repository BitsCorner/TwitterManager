$(document).ready(function () {
    $('body').on('click', '.unfollowlnk', function (e) {
        var userid = $(this).data('userid');
        var screenname = $(this).data('screenname');
        unfollow(userid, screenname);
    });

    $('body').on('click', '.tweettofix', function (e) {
        var userid = $(this).data('userid');
        var screenname = $(this).data('screenname');
        MentionToFix(userid, screenname);
    });

});

function unfollow(userid, screenname)
{
    $.ajax({
        url: "/Home/Unfollow/" + userid,
        type: "GET",
        dataType: 'json',
        success: $(".userrow_" + userid).remove()
    });
}

function MentionToFix(userid, screenname) {
    $.ajax({
        url: "/Home/MentionToFix/" + screenname,
        type: "GET",
        dataType: 'json',
        success: $(".userrow_" + userid).remove()
    });
}
