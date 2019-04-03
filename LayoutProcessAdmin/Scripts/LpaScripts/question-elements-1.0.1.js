var currentChecklistId;
var selectedType;

var Answer = ({
	addYesNo: function () {
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
	},
	addMultiple: function () {
		var div = document.createElement('div');
		div.classList = "col-10 form-group";
		div.id = "multipleContainer";

		var input = document.createElement("input");
		input.classList = "form-control";
		input.placeholder = "New Answer";

		div.appendChild(input);
		div.appendChild(document.createElement('br'));
		return div;
	},
	addInput: function () {
		var input = document.createElement('input');
		input.classList = "form-control";
		input.placeholder = "New Answer";
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

		var row = document.createElement('div');
		row.classList = "row form-group entry";

		var variableDiv = document.createElement('div');
		variableDiv.classList = "col-3";

		var answerDiv = document.createElement('div');
		answerDiv.classList = "col-9";

		var variableInput = document.createElement("input");
		variableInput.placeholder = "Variable";
		variableInput.classList = "form-control";
		variableDiv.appendChild(variableInput);

		var answerInput = document.createElement("input");
		answerInput.placeholder = "New answer";
		answerInput.classList = "form-control";
		answerDiv.appendChild(answerInput);

		row.appendChild(variableDiv);
		row.appendChild(answerDiv);

		div.appendChild(row);

		return div;
	},
	addCalculatedInput: function () {
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

		var answerInput = document.createElement("input");
		answerInput.placeholder = "New answer";
		answerInput.classList = "form-control answer";
		answerDiv.appendChild(answerInput);

		row.appendChild(variableDiv);
		row.appendChild(answerDiv);

		div.appendChild(row);
	},
	removeCalculatedInput: function () {
		$('#calculatedContainer .entry').last().remove();
	}
});

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
			$('#chSingleAnswer').remove('disabled');
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

$('#btnAddAnswer').on('click', function () {
	switch (selectedType) {
		case "mu":
			Answer.addInput();
			break;
		case "ca":
			Answer.addCalculatedInput();
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

function ShowQuestionModal() {
	$('#selectType').val(0);
	$('#addQuestion').modal('show');
}