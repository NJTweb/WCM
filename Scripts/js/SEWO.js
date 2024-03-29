﻿function drawSketch() {
    var sketchArea = $("#sketchArea");
    var sketchCtx = sketchArea[0].getContext("2d");

    var off = sketchArea.offset();

    var relX = (mouse.x - off.left);
    var relY = (mouse.y - off.top);

    sketchCtx.fillStyle = $("#colorPicker").val();
    if (mouse.left) {
        sketchCtx.fillRect(relX, relY, $("#brushSize").val(), $("#brushSize").val());
    }
    console.log($("#colorPicker").val());
}

function showSketch(sketchURL) {
    var sketchArea = $("#sketchArea");
    var sketchCtx = sketchArea[0].getContext("2d");

    var img = new Image();
    img.src = sketchURL;
    img.onload = function () {
        sketchCtx.drawImage(img, 0, 0);
    }
}

function clearSketch() {
    var sketchArea = $("#sketchArea");
    var sketchCtx = sketchArea[0].getContext("2d");
    sketchCtx.clearRect(0, 0, sketchArea.width(), sketchArea.height());
}

function saveSketch() {
    var dataURL = $("#sketchArea")[0].toDataURL();
    //$("#sketchImg")[0].src = dataURL;
    $("#sketchUrl").val(dataURL);
}

function showBodyPartPicker() {

}

function getSecondaryRootCauses(rootCause) {
    AJAXInsert("secondary", "/WCC/getOptions?ConStr=safety&Query=secondRootCauses&rootCause=" + rootCause, false);
}

function getMicroRootCauses(secondRootCause) {
    AJAXInsert("micro", "/WCC/getOptions?ConStr=safety&Query=microRootCauses&secondRootCause=" + secondRootCause, false);
}

function getActions(secondRootCause) {
    AJAXInsert("actions", "/WCC/getOptions?ConStr=safety&Query=actions&secondRootCause=" + secondRootCause, false);
}

function getActionsAndMicros(secondRootCause) {
    getMicroRootCauses(secondRootCause);
    getActions(secondRootCause);
}

var mouse = { x: 0, y: 0, left: false };

document.addEventListener('mousemove', function (e) {
    mouse.x = e.clientX || e.pageX;
    mouse.y = e.clientY || e.pageY;
}, false);

document.addEventListener('mousedown', function (e) {
    mouse.left = (e.button === 0);
}, false);

document.addEventListener('mouseup', function (e) {
    mouse.left = false;
}, false);

showSketch();