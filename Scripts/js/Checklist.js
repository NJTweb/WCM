"use strict";

function getZonesAndWorkCells(dept) {
    getZones(dept);
    getWorkCells(dept);
}

function getZones(dept) {
    AJAXInsert("zone", "/WCC/getOptions?ConStr=safety&Query=zones&dept=" + dept, false);
}

function getWorkCells(dept) {
    AJAXInsert("workcell", "/WCC/getOptions?ConStr=safety&Query=workcells&dept=" + dept, false);
}

function getMachines(zone) {
    AJAXInsert("mach", "/WCC/getOptions?ConStr=safety&Query=machs&zone=" + zone, false);
}

function validateDates(){
	var datesValid = true;
	for(var i = 1; i <= 31; ++i){
		var thisDateEl = document.getElementsByName("DueDate" + i)[0];
		var thisDate = new Date(thisDateEl.value);
		var dateCheck = checkDate(thisDate);
		if(!dateCheck) alert("You entered an invalid date on line " + i);
		datesValid = (datesValid && checkDate(thisDate));
	}
	return datesValid;
}

function changeImage(input, imgEl){
	var isBlank = (input.value == "");
	imgEl.src = (isBlank ? "../../res/upload.png" : "../../res/uploadgreen.png");
	console.log("src att changed to " + (isBlank ? "../../res/upload.png" : "../../res/uploadgreen.png"));
}

function openForm(){
	var num = prompt("Enter a form number.");
	var isNum = !isNaN(num);
	if(isNum){
		window.location.href = "Open?ID=" + num;
	}
	else{
		window.alert("Invalid input. Please enter a number.");
	}
	return isNum;
}