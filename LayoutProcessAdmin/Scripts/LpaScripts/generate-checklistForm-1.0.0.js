
var lpaForm = {

    Question: function (text, type, answers,) {
        var div = document.createElement('div');
        div.classList = "row";
        var paragraph = document.createElement('p');
        paragraph.textContent = "<h3>" + text + "</h3>";
        div.appendChild(paragraph);
        return div;
    },
    Answer: function (){

    }

}