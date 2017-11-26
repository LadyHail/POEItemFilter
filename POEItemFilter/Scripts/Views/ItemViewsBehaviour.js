//This script applies behaviour of the elements.
$(document).ready(function () {

    // Add/remove additional textarea if item is not in database.
    var dbItems = $("#databaseItems");
    var userItem = $("#userItemTB").detach();
    $("#itemNotInDb").click(function () {
        if (document.getElementById("itemNotInDb").checked) {
            dbItems.detach();
            userItem.insertAfter("#itemNotInDbCB");
        } else {
            dbItems.insertAfter("#basicBreak");
            userItem.detach();
        }
    });

    // Hide at start most part of view.
    $("#itemLvl").hide();
    $("#sockets").hide();
    $("#size").hide();
    $("#other").hide();

    // Toggle hide/show part of view.
    $("#basicToggle").click(function () {
        $("#databaseItems").slideToggle();
        $("#noDb").slideToggle();
    });

    $("#lvlToggle").click(function () {
        $("#itemLvl").slideToggle();
    });

    $("#socketsToggle").click(function () {
        $("#sockets").slideToggle();
    });

    $("#sizeToggle").click(function () {
        $("#size").slideToggle();
    });

    $("#otherToggle").click(function () {
        $("#other").slideToggle();
    });
});