jQuery(document).ready(function() {

    jQuery(".ajaxLoad").each(function() {
        var ajaxLink = jQuery(this);
        ajaxLink.click(function () {
            jQuery("#" + ajaxLink.attr("rel")).load(ajaxLink.attr("href"));
            return false;
        });
    });

});

function submitForm(ele) {
    var d = jQuery(ele).serialize();

    $.ajax({
        url: jQuery(ele).attr("action"),
        data: d,
        dataType: "html",
        type: "post",
        success: function (data) {
            if (data == "") {
                document.location.reload();
            } else {
                jQuery("#notice").html(data);
            }
        },
        error: function() {
            //alert("Service Error");
        }
    });

    return false;
}


function RefreshVC(ele) {

    jQuery(ele).attr("src", jQuery(ele).attr("src") + "?time=" + (new Date()).getTime());

    return true;
}

function focusNav(ele) {
    jQuery(ele).addClass("active");
    return false;
}