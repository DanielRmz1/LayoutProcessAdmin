
var lpaForm = {
    Question: function (text, type, answers, single, formula) {
        var questionContainer = document.createElement('div');
        questionContainer.classList = "row";

        var row = document.createElement('div');
        row.classList = "col-12";

        var description = document.createElement('div');
        description.classList = "row";

        if (type != "ca")
            description.textContent = text;
        else
            description.textContent = text + " - Formula: " + formula;
        
        var answersRow = document.createElement('div');
        answersRow.classList = "row";

        var answersContainer = document.createElement('div');
        answersContainer.classList = "col-12";

        answers.forEach(function (e) {
            answersContainer.appendChild(AddAnswer(e, type, single, text));
        });

        answersRow.appendChild(answersContainer);
        
        row.appendChild(description);
        row.appendChild(answersRow);
        questionContainer.appendChild(row);

        return questionContainer;
    }

}

function AddAnswer(answers, types, single, text) {
    switch (types) {
        case "mu":
            if (single)
                return AddMultipleRadio(answers.chr_Description, text);
            else
                return AddMultiple(answers.chr_Description);
            break;
        case "op":
            return AddOpen(answers.chr_Description);
            break;
        case "yn":
            return AddYesNo(answers.chr_Description, text);
            break;
        case "ca":
            return AddCalculated(answers.chr_Description, answers.chr_Variable);
            break;
        case "in":
            return AddNumerical(answers.chr_Description);
            break;
    }

    var div = document.createElement('div');
    return div;
}

function AddNumerical(description) {

    var newAnswer = document.createElement('div');
    newAnswer.classList = "row form-group col-6";

    var checkbox = document.createElement('input');
    checkbox.setAttribute('type', 'number');
    checkbox.id = 'id-' + description;
    checkbox.classList = "form-control form-control-sm";

    newAnswer.appendChild(checkbox);

    return newAnswer;
}

function AddCalculated(description, variable) {

    var newAnswer = document.createElement('div');
    newAnswer.classList = "row form-group col-6";

    var input = document.createElement('input');
    input.setAttribute('type', 'number');
    input.id = 'id-' + description;
    input.classList = "form-control form-control-sm";

    var label = document.createElement('label');
    label.setAttribute('for', 'id-' + description);
    label.textContent = description + " (" + variable + ")";

    newAnswer.appendChild(label);
    newAnswer.appendChild(input);

    return newAnswer;
}

function AddMultiple(description) {

    var newAnswer = document.createElement('div');
    newAnswer.classList = "row custom-control custom-checkbox";

    var checkbox = document.createElement('input');
    checkbox.setAttribute('type', 'checkbox');
    checkbox.id = 'id-' + description;
    checkbox.classList = "custom-control-input";

    var label = document.createElement('label');
    label.textContent = description;
    label.setAttribute('for', 'id-' + description);
    label.classList = "custom-control-label";
    
    newAnswer.appendChild(checkbox);
    newAnswer.appendChild(label);

    return newAnswer;
}

function AddMultipleRadio(description, text) {

    var newAnswer = document.createElement('div');
    newAnswer.classList = "row custom-control custom-radio";

    var radio = document.createElement('input');
    radio.setAttribute('type', 'radio');
    radio.id = 'id-' + description;
    radio.classList = "custom-control-input";
    radio.name = text + "-radios"; 

    var label = document.createElement('label');
    label.textContent = description;
    label.setAttribute('for', 'id-' + description);
    label.classList = "custom-control-label";
    
    newAnswer.appendChild(radio);
    newAnswer.appendChild(label);

    return newAnswer;
}

function AddYesNo(description, text) {

    var newAnswer = document.createElement('div');
    newAnswer.classList = "row custom-control custom-radio";

    var radio = document.createElement('input');
    radio.setAttribute('type', 'radio');
    radio.id = 'id-' + description;
    radio.classList = "custom-control-input";
    radio.name = text + "-radios"; 

    var label = document.createElement('label');
    label.textContent = description;
    label.setAttribute('for', 'id-' + description);
    label.classList = "custom-control-label";
    
    newAnswer.appendChild(radio);
    newAnswer.appendChild(label);

    return newAnswer;
}

function AddOpen(description) {

    var newAnswer = document.createElement('div');
    newAnswer.classList = "row form-group";

    var textarea = document.createElement('textarea');
    textarea.classList = "form-control custom-textarea";

    newAnswer.appendChild(textarea);

    return newAnswer;
}
