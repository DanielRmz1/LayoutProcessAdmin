var currentChecklistId;
var selectedType;
var isEditing = false;
var editingQuestionId = -1;
var token;

var Answer = ({
    addYesNo: function () {
        if (!isEditing) {
            var div = document.createElement('div');
            div.classList = "col-10 form-group";
            var input = document.createElement("input");
            input.classList = "form-control";
            input.value = "Yes";
            input.id = "txtYes";

            div.appendChild(input);
            div.appendChild(document.createElement('br'));

            var input2 = document.createElement("input");
            input2.classList = "form-control";
            input2.value = "No";
            input2.id = "txtNo";

            div.appendChild(input2);
            return div;
        }
    },
    addYesNoEdit: function (yesText, noText) {

        var div = document.createElement('div');
        div.classList = "col-10 form-group";
        var input = document.createElement("input");
        input.classList = "form-control";
        input.value = yesText;
        input.id = "txtYes";

        div.appendChild(input);
        div.appendChild(document.createElement('br'));

        var input2 = document.createElement("input");
        input2.classList = "form-control";
        input2.value = noText;
        input2.id = "txtNo";

        div.appendChild(input2);
        
        return div;
    },
    addMultiple: function () {
		var div = document.createElement('div');
		div.classList = "col-10 form-group";
		div.id = "multipleContainer";

        var input = document.createElement("input");
		input.classList = "form-control";
        input.placeholder = "New Answer";

        if (!isEditing) { div.appendChild(input); }
		div.appendChild(document.createElement('br'));
		return div;
	},
	addInput: function (text) {
		var input = document.createElement('input');
		input.classList = "form-control";
        input.placeholder = "New Answer";
        input.value = text;
		document.getElementById("multipleContainer").appendChild(input);
		document.getElementById("multipleContainer").appendChild(document.createElement('br'));
	},
	removeInput: function () {
		$('#multipleContainer input').last().remove();
		$('#multipleContainer br').last().remove();
	},
	addCalculated: function () {
		var div = document.createElement('div');
		div.classList = "col-12 form-group";
		div.id = "calculatedContainer";

		var newRow = document.createElement('div');
		newRow.classList = "row";

		var formulaInput = document.createElement('input');
		formulaInput.classList = "form-control";
		formulaInput.id = "formulaInput";
        formulaInput.placeholder = "Formula: Example -> (base*height)/2";

		var help = document.createElement('i');
		help.classList = "fa fa-question-circle text-primary";
		help.style = "font-size: 24px;cursor: pointer;";
		help.onclick = function () {
			$('#helpModal').modal('show');
		};

		var formulaDiv = document.createElement('div');
		formulaDiv.classList = "col-11";
		var helpDiv = document.createElement('div');
		helpDiv.classList = "col-1";

		formulaDiv.appendChild(formulaInput);
		helpDiv.appendChild(help);

		newRow.appendChild(formulaDiv);
		newRow.appendChild(helpDiv);
		div.appendChild(newRow);
        div.appendChild(document.createElement('br'));

        if (!isEditing) {
            var row = document.createElement('div');
            row.classList = "row form-group entry";

            var variableDiv = document.createElement('div');
            variableDiv.classList = "col-3";

            var answerDiv = document.createElement('div');
            answerDiv.classList = "col-9";

            var variableInput = document.createElement("input");
            variableInput.placeholder = "Variable";
            variableInput.classList = "form-control variable";
            variableDiv.appendChild(variableInput);

            var answerInput = document.createElement("input");
            answerInput.placeholder = "New answer";
            answerInput.classList = "form-control answer";
            answerDiv.appendChild(answerInput);

            row.appendChild(variableDiv);
            row.appendChild(answerDiv);

            div.appendChild(row);
        }

		return div;
	},
    addCalculatedInput: function (variable, text) {
		var div = document.getElementById("calculatedContainer");

		var row = document.createElement('div');
		row.classList = "row form-group entry";

		var variableDiv = document.createElement('div');
		variableDiv.classList = "col-3";

		var answerDiv = document.createElement('div');
		answerDiv.classList = "col-9";

		var variableInput = document.createElement("input");
		variableInput.placeholder = "Variable";
		variableInput.classList = "form-control variable";
        variableDiv.appendChild(variableInput);
        variableInput.value = variable;

		var answerInput = document.createElement("input");
		answerInput.placeholder = "New answer";
		answerInput.classList = "form-control answer";
        answerDiv.appendChild(answerInput);
        answerInput.value = text;

		row.appendChild(variableDiv);
		row.appendChild(answerDiv);

		div.appendChild(row);
	},
	removeCalculatedInput: function () {
		$('#calculatedContainer .entry').last().remove();
	}
});

var Question = {
    addRow: function (index, description, type, single, formula, id, text, edit_id) {
        var row = document.createElement('tr');

        var cell1 = document.createElement('td');
        var span1 = document.createElement('span');
        span1.textContent = index;
        cell1.appendChild(span1);

        var cell2 = document.createElement('td');
        var span2 = document.createElement('span');
        span2.textContent = description;
        cell2.appendChild(span2);

        var cell3 = document.createElement('td');
        var span3 = document.createElement('span');
        span3.textContent = type;
        cell3.appendChild(span3);

        var cell4 = document.createElement('td');
        var span4 = document.createElement('span');
        span4.textContent = single;
        cell4.appendChild(span4);

        var cell5 = document.createElement('td');
        var span5 = document.createElement('span');
        span5.textContent = formula;
        cell5.appendChild(span5);

        // ----------- Celda de los botones de Editar/Eliminar ---------------

        var cell6 = document.createElement('td');

        var divRow = document.createElement('div');
        divRow.classList = 'row';

        var editTag = document.createElement('i');
        editTag.classList = "fa fa-pen";

        var deleteTag = document.createElement('i');
        deleteTag.classList = "fa fa-minus-circle";

        var editBtn = document.createElement('a');
        editBtn.href = '#';
        editBtn.setAttribute("role", "button");
        editBtn.classList = "btn btn-warning btn-sm";
        editBtn.appendChild(editTag);
        editBtn.onclick = function () {
            EditQuestion(id);
        };

        var deleteBtn = document.createElement('a');
        deleteBtn.href = '#';
        deleteBtn.setAttribute("role", "button");
        deleteBtn.classList = "btn btn-danger btn-sm";
        deleteBtn.appendChild(deleteTag);
        deleteBtn.onclick = function () {
            ShowDeleteQuestionModal(id);
        };

        var editDiv = document.createElement('div');
        editDiv.classList = "col-6";
        editDiv.appendChild(editBtn);
        
        var delDiv = document.createElement('div');
        delDiv.classList = "col-6";
        delDiv.appendChild(deleteBtn);

        divRow.appendChild(editBtn);
        divRow.appendChild(delDiv);

        cell6.appendChild(divRow);

        // ---------- Celda de los botones de Editar/Eliminar ---------------
        
        row.appendChild(cell1);
        row.appendChild(cell2);
        row.appendChild(cell3);
        row.appendChild(cell4);
        row.appendChild(cell5);
        row.appendChild(cell6);

        return row;
    }
}

$('#selectType').change(function () {
	$('#answersTextBoxes').html("");
	selectedType = $('#selectType').val();

	$('#answersContainer').show();

	switch (selectedType) {
		case "none":
			$('#answersContainer').hide();
			break;
		case "yn":
			$('#btnAddAnswer').hide();
            $('#btnRemoveAnswer').hide();

            if (isEditing)
                $('#answersTextBoxes').append(Answer.addYesNoEdit("Yes", "No"));
            else
                $('#answersTextBoxes').append(Answer.addYesNo());

			$('#chSingleAnswer').prop("checked", true);
			$('#chSingleAnswer').attr('disabled', 'disabled');
			break;
		case "op":
			$('#btnAddAnswer').hide();
			$('#btnRemoveAnswer').hide();
			$('#chSingleAnswer').prop("checked", true);
			$('#chSingleAnswer').attr('disabled', 'disabled');
			break;
		case "mu":
			$('#btnAddAnswer').show();
            $('#btnRemoveAnswer').show();
			$('#answersTextBoxes').append(Answer.addMultiple());
            $('#chSingleAnswer').prop("checked", false);
            $('#chSingleAnswer').removeAttr('disabled');
			break;
		case "ca":
			$('#btnAddAnswer').show();
            $('#btnRemoveAnswer').show();
			$('#answersTextBoxes').append(Answer.addCalculated());
			$('#chSingleAnswer').prop("checked", true);
			$('#chSingleAnswer').attr('disabled', 'disabled');
			break;
		default:
			$('#answersContainer').hide();
			break;

	}
});

$('#btnAddQuestion').on('click', function () {
    $('#answersContainer').hide();
    ShowQuestionModal("");
});

$('#addQuestion').on('hide.bs.modal', function () {
    isEditing = false;
    editingQuestionId = -1;
});

$('#btnAddAnswer').on('click', function () {
	switch (selectedType) {
		case "mu":
			Answer.addInput("");
			break;
		case "ca":
			Answer.addCalculatedInput("", "");
			break;
	}
});

$('#btnRemoveAnswer').on('click', function () {
	switch (selectedType) {
		case "mu":
			Answer.removeInput();
			break;
		case "ca":
			Answer.removeCalculatedInput();
			break;
	}
});

function ShowQuestionModal(question) {
    $('#questionSumary').html('');

    if (question != "") {
        $('#answersContainer').show();
        $('#idPregunta').val(question.Quest.chr_Description);
        $('#selectType').val(question.Quest.chr_Type).trigger('change');
        $('#chSingleAnswer').prop('checked', question.Quest.bit_SingleAnswer);

        for (var i = 0; i < question.Answs.length; i++) {
            if (question.Quest.chr_Type == 'ca' && i == 0) {
                Answer.addCalculated(true);
                $('#formulaInput').val(question.Quest.chr_Formula);
            }
            else if (question.Quest.chr_Type == 'mu' && i == 0)
                Answer.addMultiple(false);
            else if (question.Quest.chr_Type == 'yn' && i == 0) {
                $('#btnAddAnswer').hide();
                $('#btnRemoveAnswer').hide();
                $('#answersTextBoxes').append(Answer.addYesNoEdit(question.Answs[0].chr_Description, question.Answs[1].chr_Description));
                $('#chSingleAnswer').prop("checked", true);
                $('#chSingleAnswer').attr('disabled', 'disabled');
            }
          
            AddEditingControls(question.Quest.chr_Type, question.Answs[i].chr_Description, question.Answs[i].chr_Variable);
        }
    } else {
        $('#idPregunta').val('');
        $('#selectType').val('none').trigger('change');
    }
    
	$('#addQuestion').modal('show');
}

function AddEditingControls(type, text, variable) {

    switch (type) {
        case "none":
            $('#answersContainer').hide();
            break;
        case "op":
            $('#btnAddAnswer').hide();
            $('#btnRemoveAnswer').hide();
            $('#chSingleAnswer').prop("checked", true);
            $('#chSingleAnswer').attr('disabled', 'disabled');
            break;
        case "mu":
            $('#btnAddAnswer').show();
            $('#btnRemoveAnswer').show();
            $('#answersTextBoxes').append(Answer.addInput(text));
            $('#chSingleAnswer').removeAttr('disabled');
            break;
        case "ca":
            $('#btnAddAnswer').show();
            $('#btnRemoveAnswer').show();
            $('#answersTextBoxes').append(Answer.addCalculatedInput(variable, text));
            $('#chSingleAnswer').prop("checked", true);
            $('#chSingleAnswer').attr('disabled', 'disabled');
            break;

    }
}

function ShowDeleteQuestionModal(id) {
    $('#deleteQuestionModal .id-question').val(id);
    $('#deleteQuestionModal').modal('show');
}
