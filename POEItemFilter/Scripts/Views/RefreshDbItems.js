//This script applies to #databaseItems div element.
$(document).ready(function () {
    setOldValues();

    // Add item to filter and clear cookies.
    $("#saveItem").click(function () {
        setCookie("baseTypeSelectedId", null);
        setCookie("typeSelectedId", null);
        setCookie("attribute1SelectedId", null);
        setCookie("attribute2SelectedId", null);
        setCookie("itemSelectedId", null);
    });

    //Refresh 
    $("#databaseItems").on("change", "#baseTypes", function () {
        if ($("#baseTypes option:selected").text() == "Armour" || $("#baseTypes option:selected").text() == "All") {
            $("#attribute1").show();
            $("#attribute2").show();
        } else {
            $("#attribute1").hide();
            $("#attribute2").hide();
        }

        var baseType = $("#baseTypes").val();
        var type = null;
        var attribute1 = null;
        var attribute2 = null;
        var item = null;

        setCookie("baseTypeSelectedId", baseType);
        setCookie("typeSelectedId", type);
        setCookie("attribute1SelectedId", attribute1);
        setCookie("attribute2SelectedId", attribute2);
        setCookie("itemSelectedId", item);

        $.get("/UsersItems/Refresh/" + baseType + "/" + type + "/" + attribute1 + "/" + attribute2, function (data) {
            $("#databaseItems").html(data);
            setOldValues();
        });
    });

    $("#databaseItems").on("change", "#types", function () {
        if ($("#baseTypes option:selected").text() == "Armour" || $("#types option:selected").text() == "BodyArmour" || $("#types option:selected").text() == "Helmet" ||
            $("#types option:selected").text() == "Shield" || $("#types option:selected").text() == "Gloves" || $("#types option:selected").text() == "Boots") {
            $("#attribute1").show();
            $("#attribute2").show();
        } else {
            $("#attribute1").hide();
            $("#attribute2").hide();
        }

        if ($("#baseTypes option:selected").text() != "All") {
            var baseType = $("#baseTypes").val();
            var type = $("#types").val();
            var attribute1 = null;
            var attribute2 = null;
            var item = null;
        } else {
            var baseType = null;
            var type = $("#types").val();
            var attribute1 = null;
            var attribute2 = null;
            var item = null;
        }

        setCookie("baseTypeSelectedId", baseType);
        setCookie("typeSelectedId", type);
        setCookie("attribute1SelectedId", attribute1);
        setCookie("attribute2SelectedId", attribute2);
        setCookie("itemSelectedId", item);

        $.get("/UsersItems/Refresh/" + baseType + "/" + type + "/" + attribute1 + "/" + attribute2, function (data) {
            $("#databaseItems").html(data);
            setOldValues();
        });
    });

    $("#databaseItems").on("change", "#attribute1", function () {
        if ($("#baseTypes option:selected").text() != "All") {
            if ($("#types option:selected").text() != "All") {
                var baseType = $("#baseTypes").val();
                var type = $("#types").val();
                var attribute1 = $("#attribute1").val();
                if ($("#attribute2 option:selected").text() != "All") {
                    var attribute2 = $("#attribute2").val();
                } else {
                    var attribute2 = null;
                }
                var item = null;
            } else {
                var baseType = $("#baseTypes").val();
                var type = null;
                var attribute1 = $("#attribute1").val();
                if ($("#attribute2 option:selected").text() != "All") {
                    var attribute2 = $("#attribute2").val();
                } else {
                    var attribute2 = null;
                }
                var item = null;
            }
        } else {
            if ($("#types option:selected").text() != "All") {
                var baseType = null;
                var type = $("#types").val();
                var attribute1 = $("#attribute1").val();
                if ($("#attribute2 option:selected").text() != "All") {
                    var attribute2 = $("#attribute2").val();
                } else {
                    var attribute2 = null;
                }
                var item = null;
            } else {
                var baseType = null;
                var type = null;
                var attribute1 = $("#attribute1").val();
                if ($("#attribute2 option:selected").text() != "All") {
                    var attribute2 = $("#attribute2").val();
                } else {
                    var attribute2 = null;
                }
                var item = null;
            }
        }

        setCookie("baseTypeSelectedId", baseType);
        setCookie("typeSelectedId", type);
        setCookie("attribute1SelectedId", attribute1);
        setCookie("attribute2SelectedId", attribute2);
        setCookie("itemSelectedId", item);

        $.get("/UsersItems/Refresh/" + baseType + "/" + type + "/" + attribute1 + "/" + attribute2, function (data) {
            $("#databaseItems").html(data);
            setOldValues();
        });
    });

    $("#databaseItems").on("change", "#attribute2", function () {
        if ($("#baseTypes option:selected").text() != "All") {
            if ($("#types option:selected").text() != "All") {
                var baseType = $("#baseTypes").val();
                var type = $("#types").val();
                var attribute2 = $("#attribute2").val();
                if ($("#attribute1 option:selected").text() != "All") {
                    var attribute1 = $("#attribute1").val();
                } else {
                    var attribute1 = null;
                }
                var item = null;
            } else {
                var baseType = $("#baseTypes").val();
                var type = null;
                var attribute2 = $("#attribute2").val();
                if ($("#attribute1 option:selected").text() != "All") {
                    var attribute1 = $("#attribute1").val();
                } else {
                    var attribute1 = null;
                }
                var item = null;
            }
        } else {
            if ($("#types option:selected").text() != "All") {
                var baseType = null;
                var type = $("#types").val();
                var attribute2 = $("#attribute2").val();
                if ($("#attribute1 option:selected").text() != "All") {
                    var attribute1 = $("#attribute1").val();
                } else {
                    var attribute1 = null;
                }
                var item = null;
            } else {
                var baseType = null;
                var type = null;
                var attribute2 = $("#attribute2").val();
                if ($("#attribute1 option:selected").text() != "All") {
                    var attribute1 = $("#attribute1").val();
                } else {
                    var attribute1 = null;
                }
                var item = null;
            }
        }

        setCookie("baseTypeSelectedId", baseType);
        setCookie("typeSelectedId", type);
        setCookie("attribute1SelectedId", attribute1);
        setCookie("attribute2SelectedId", attribute2);
        setCookie("itemSelectedId", item);

        $.get("/UsersItems/Refresh/" + baseType + "/" + type + "/" + attribute1 + "/" + attribute2, function (data) {
            $("#databaseItems").html(data);
            setOldValues();
        });
    });

    //Cookies behaviour
    function setCookie(cname, cvalue) {
        document.cookie = cname + "=" + cvalue;
    }

    function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(";");
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

    function setOldValues() {
        var baseTypeSelectedId = getCookie("baseTypeSelectedId");
        var typeSelectedId = getCookie("typeSelectedId");
        var attribute1SelectedId = getCookie("attribute1SelectedId");
        var attribute2SelectedId = getCookie("attribute2SelectedId");
        var itemSelectedId = getCookie("itemSelectedId");

        if (baseTypeSelectedId != "" && baseTypeSelectedId != "null") {
            $("#baseTypes").val(baseTypeSelectedId);
        }
        else {
            $("#baseTypes option:first").val();
        }

        if (typeSelectedId != "" && typeSelectedId != "null") {
            $("#types").val(typeSelectedId);
        }
        else {
            $("#types option:first").val();
        }

        if (attribute1SelectedId != "" && attribute1SelectedId != "null") {
            $("#attribute1").val(attribute1SelectedId);
        }
        else {
            $("#attribute1 option:first").val();
        }

        if (attribute2SelectedId != "" && attribute2SelectedId != "null") {
            $("#attribute2").val(attribute2SelectedId);

        }
        else {
            $("#attribute2 option:first").val();
        }

        if (itemSelectedId != "" && itemSelectedId != "null") {
            $("#items").val(itemSelectedId);

        }
        else {
            $("#items option:first").val();
        }
    }
});