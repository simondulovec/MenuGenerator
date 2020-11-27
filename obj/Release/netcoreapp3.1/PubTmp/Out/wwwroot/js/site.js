// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#ml-tb').DataTable({
        "language": {
            "decimal": "",
            "emptyTable": "V tabuľke nie sú žiadne jedlá",
            "info": "Zobrazených _START_ až _END_ z _TOTAL_ jedál",
            "infoEmpty": "Zobrazených 0 až 0 z 0 jedál",
            "infoFiltered": "(Filtrovaných _MAX_ jedál)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "_MENU_ Počet zobrazených jedál",
            "loadingRecords": "Načítavam...",
            "processing": "Spracovávam...",
            "search": "Hľadať:",
            "zeroRecords": "Žiadna zhoda",
            "paginate": {
                "first": "Prvá",
                "last": "Posledná",
                "next": "Ďalšia",
                "previous": "Predchádzajúca"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        },
        "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "Všetky"]]
    });
});

var adm_pnl_vis = false;

if (getCookie("panelState") == "visible") {
    $("#adm-pnl").css("left", "0px");
    $("#ctn").css("marginLeft", "250px");
    adm_pnl_vis = true;
}
else if (getCookie("panelState") == "hidden") {
    $("#adm-pnl").css("left", "-250px");
    $("#ctn").css("marginLeft", "0px");
    adm_pnl_vis = false;
}

//ADMIN PANEL
function adminPanelToggle() {
    if (adm_pnl_vis) {
        adm_pnl_vis = false;
        setCookie("panelState", "hidden", 1);
        $("#adm-pnl").animate({
            left: "-=250px"
        },
            {
                duration: 500,
                complete: function () {
                    contentDeactivateMargin();
                }
            });
    }
    else {
        adm_pnl_vis = true;
        setCookie("panelState", "visible", 1);
        $("#adm-pnl").animate({
            left: "+=250px"
        },
            {
                duration: 500,
                complete: function () {
                    contentActivateMargin();
                }
            });
    }
}

function contentDeactivateMargin() {
    $("#ctn").animate({
        marginLeft: "0px"
    },
        {
            queue: false,
            duration: 500
        });
}

function contentActivateMargin() {
    $("#ctn").animate({
        marginLeft: "250px"
    },
        {
            queue: false,
            duration: 500
        });
}

//COOKIES

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

//IMPORTING FILES
function show_msg(id, msg) {
    $(id).text(msg);
    $(id).fadeIn(200);
    $(id).delay(1500).fadeOut(300);
}

//HIDING MENU
function show_hide(id_show, id_hide) {
    $(id_hide).hide();
    $(id_show).fadeIn(300);
}

function confirm_change(id) {
    var value = $("#select_" + id).val().split("*");
    $("#name_" + id).text(value[1]);
    $("#id_" + id).val(value[0]);
    show_hide("#primary_" + id, "#secondary_" + id);
}

function show_save_err() {
    $("#save-err").fadeIn();
}

function show_save_scs() {
    $("#save-scs").fadeIn();
}