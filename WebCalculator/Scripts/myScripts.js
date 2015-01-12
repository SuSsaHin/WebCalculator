function AddText(select) {
    document.CalculatorForm.InputExpression.value += select.options[select.selectedIndex].value.split(' ')[0];
}

var updateOperators = function (select) {
    var index = select.selectedIndex;
    var selected = select.options[index].value;
    $.post("Home/OperatorsList?selected=" + selected, function (data) {
        $('#SelectedOperators').html(data);
        select.selectedIndex = index;
    });
}

$(function () {
    var updatePlugins = function () {
        $('#pluginsList').load('Home/PluginsList');
    }

    $("#UploadButton").ajaxUpload({
        url: "/Home/Upload",
        name: "file0",
        onComplete: function (response) {
            $("#status").text(response);
            if (response.toLowerCase() === "success")
                updatePlugins();
        }
    });
});