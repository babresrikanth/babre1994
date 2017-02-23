/// <reference path="ajaxJavaScript.js" />

    $(function () {
        $("select").each(function () {
            if ($(this).find("option").length <= 1) {
                $(this).attr("disabled", "disabled");
            }
        });

        $("select").change(function () {
            var value = 0;
            if ($(this).val() != "") {
                value = $(this).val();
            }
            var id = $(this).attr("id");
            $.ajax({
                type: "POST",
                url: "/Home/AjaxMethod",
                data: '{type: "' + id + '", value: ' + value + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var dropDownId;
                    var list;
                    switch (id) {
                        case "CountryId":
                            list = response.States;
                            DisableDropDown("#StateId");
                            DisableDropDown("#CityId");
                            PopulateDropDown("#StateId", list);
                            break;
                        case "StateId":
                            dropDownId = "#CityId";
                            list = response.Cities;
                            DisableDropDown("#CityId");
                            PopulateDropDown("#CityId", list);
                            break;
                    }
                        
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });
    });

function DisableDropDown(dropDownId) {
    $(dropDownId).attr("disabled", "disabled");
    $(dropDownId).empty().append('<option selected="selected" value="0">Please select</option>');
}

function PopulateDropDown(dropDownId, list) {
    if (list != null && list.length > 0) {
        $(dropDownId).removeAttr("disabled");
        $.each(list, function () {
            $(dropDownId).append($("<option></option>").val(this['Value']).html(this['Text']));
        });
    }
}

//$(function () {
//    if ($("#CountryId").val() != "" && $("#StateId").val() != "" && $("#CityId").val() != "") {
//        var message = "Country: " + $("#CountryId option:selected").text();
//        message += "\nState: " + $("#StateId option:selected").text();
//        message += "\nCity: " + $("#CityId option:selected").text();

        
//        alert(message);
//    }
//});
