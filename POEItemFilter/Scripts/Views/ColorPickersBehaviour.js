//This script sets behaviour of color pickers.
$(document).ready(function () {
    defaultSet();

    $(".dropdown-color-picker option").each(function () {
        if ($(this).val() !== '') {
            $(this).css('background-color', 'rgb(' + $(this).val() + ')');
        }
    });

    $("#itemInfo").on("change", ".dropdown-color-picker", function () {
        let value = $(this).val();
        value = value.split(",");
        value = rgbToHex(parseInt(value[0]), parseInt(value[1]), parseInt(value[2]));
        $(this).parent().find(".manual-color-picker").val(value);
        $(this).parent().siblings("input:hidden").val(hexToRgb(value));
    });

    $("#itemInfo").on("change", ".manual-color-picker", function () {
        $(this).parent().find(".dropdown-color-picker").val("");
        $(this).parent().siblings("input:hidden").val(hexToRgb($(this).val()));
    });

    $("#itemInfo").on("change", ".is-default-color", function () {
        let button = $(this);
        let toggle = button.parent().siblings(".color-selector");
        let model = toggle.next();
        if (button.val() == "true") {
            toggle.hide();
            model.val("");
        } else {
            toggle.show();
            if (model.val() !== "") {
                let value = model.val().split(" ");
                $(this).parent().siblings(".color-selector").find(".manual-color-picker").val(rgbToHex(parseInt(value[0]), parseInt(value[1]), parseInt(value[2])));
            } else if (toggle.find(".dropdown-color-picker").val() == "") {
                model.val(hexToRgb($(this).parent().siblings(".color-selector").find(".manual-color-picker").val()));
            } else {
                model.val($(this).parent().siblings(".color-selector").find(".dropdown-color-picker").val().replace(/,/g, " "));
            }
        }
    });

    function defaultSet() {
        let defaultSet = $("#itemInfo").find(".is-default-color");
        defaultSet.each(function () {
            if ($(this).val() == "true") {
                $(this).parent().siblings(".color-selector").hide();
            }
        });
    }

    function componentToHex(c) {
        let hex = c.toString(16);
        return hex.length == 1 ? "0" + hex : hex;
    }

    function rgbToHex(r, g, b) {
        return "#" + componentToHex(r) + componentToHex(g) + componentToHex(b);
    }

    function hexToRgb(hex) {
        var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
        return result ? parseInt(result[1], 16) + " " + parseInt(result[2], 16) + " " + parseInt(result[3], 16) : null;
    }
});