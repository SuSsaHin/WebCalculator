function AddText(select) {
    document.CalculatorForm.InputExpression.value += select.options[select.selectedIndex].value.split(' ')[0];
}

function UpdateOperators(select) {
    var index = select.selectedIndex;
    var selected = select.options[index].value;
    $.post('Home/OperatorsListPart?selected=' + selected, function (data) {
        $('#SelectedOperators').html(data);
        select.selectedIndex = index;
    });
}

function UpdatePlugins() {
    $('#pluginsList').load('Home/PluginsListPart');
}

function DeletePlugin(elementId) {
    var selected = $('#'+elementId).val();
    $.post('Home/DeletePlugin?deleted=' + selected, function () { UpdatePlugins(); });
}

$(function () {
    $('#UploadButton').ajaxUpload({
        url: '/Home/UploadDll',
        name: 'lib',
        onComplete: function (response) {
            $('#status').text(response);
            if (response.toLowerCase() === 'success')
                UpdatePlugins();
        }
    });
});