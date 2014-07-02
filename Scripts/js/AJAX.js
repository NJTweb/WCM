"use strict";

var loc = "scripts/php/";

function makeXmlhttp(){
	var xmlhttp = null;
	if (window.XMLHttpRequest)
	{// code for IE7+, Firefox, Chrome, Opera, Safari
		xmlhttp=new XMLHttpRequest();
	}
	else
	{// code for IE6, IE5
		xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
	}
	return xmlhttp;
}

function isReady(xmlhttp){
	return ( xmlhttp.readyState == 4 && xmlhttp.status == 200 );
}

function AJAXInsert(element, url, sync){
	try{
		var xmlhttp = makeXmlhttp();
		xmlhttp.onreadystatechange = function() { insertResult(element, xmlhttp); };
		xmlhttp.open("GET",url,sync);
		xmlhttp.send(null);
	}catch(err){
		console.log(err.message);
	}
}

function insertResult(element, xmlhttp){
	if (isReady(xmlhttp)){
		document.getElementById(element).innerHTML=xmlhttp.responseText;
	}		
}
