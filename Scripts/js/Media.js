function mobileView() {
    if (window.innerWidth <= 799) {
        var tds = document.getElementsByTagName("td");
        console.log(tds.length);
        for (var i = 0; i < tds.length; ++i) {
            if (tds[i].innerHTML.indexOf("required") == -1 && tds[i].innerHTML.indexOf("h1") == -1 && tds[i].innerHTML.indexOf("h2") == -1 && tds[i].innerHTML.indexOf("submit") == -1) {
                tds[i].style.display = "none";
            }
        }
    }
}