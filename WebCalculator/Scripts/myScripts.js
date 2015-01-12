function AddText(select) {
    document.CalculatorForm.InputExpression.value += select.options[select.selectedIndex].value.split(' ')[0];
}

function UpdateOperators(select) {
    var index = select.selectedIndex;
    var selected = select.options[index].value;
    $.post("Home/OperatorsList?selected=" + selected, function (data) {
        $('#SelectedOperators').html(data);
        select.selectedIndex = index;
    });
}

function UpdatePlugins() {
    $('#pluginsList').load('Home/PluginsList');
}

function DeletePlugin(elementId) {
    var select = document.getElementById(elementId);
    var index = select.selectedIndex;
    var selected = select.options[index].value;
    $.post("Home/DeletePlugin?deleted=" + selected, function () { UpdatePlugins(); });
}

$(function () {
    $("#UploadButton").ajaxUpload({
        url: "/Home/Upload",
        name: "file0",
        onComplete: function (response) {
            $("#status").text(response);
            if (response.toLowerCase() === "success")
                UpdatePlugins();
        }
    });
});