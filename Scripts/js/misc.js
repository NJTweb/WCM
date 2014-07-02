"use strict";

function getSelectText(selectElement) {
	try{
		var str = selectElement.options[selectElement.selectedIndex].text;
		return str;
	}catch(err){
		console.log(err.message);
	}
}

function setValue(field, value){
	try{
		var el = document.getElementsByName(field).item(0);
		el.value = value;
	}catch(err){
		console.log(err.message);
	}
}