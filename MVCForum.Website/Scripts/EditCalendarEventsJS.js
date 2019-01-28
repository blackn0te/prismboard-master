function setInputFilter(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
        textbox.addEventListener(event, function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            }
        });
    });
}

//var input = "!@#$^&%*()+=-[]\/{}|:<>?,.";

//for (var i = 0; i < input.length; i++) {
//    var desired = sourceString.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
//}


//var outString = sourceString.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
setInputFilter(document.getElementById("moduleIn"), function (value) {
    return /^\w*\d*\.?\d*$/.test(value); //filters anything but numbers (whitelisting)
});

setInputFilter(document.getElementById("eventNameGetter"), function (value) {
    return /^\w*\d*\.?\d*$/.test(value); //filters anything but numbers (whitelisting)
});
//function validateAddress() {
//    var TCode = document.getElementById('moduleIn').value;

//    if (/[^a-zA-Z0-9\-\/]/.test(TCode)) {
//        alert('Input is not alphanumeric');
//        return false;
//    }

//    return true;
//}
window.addEventListener('load', function () {
    //document.getElementById("EventType").setAttribute("class", "form-control");
    document.getElementById("eventTypeGetter").onchange = function () {

        //var eventTypeVal = document.getElementById("EventTypeGetter").value();
        var e = document.getElementById("eventTypeGetter");
        var sel = e.options[e.selectedIndex].value;

        if (sel == "AS" || sel == "CT" || sel == "EX") {
            document.getElementById("modulePortion").style.display = "block";
        }
        else if (sel == null) {
            document.getElementById("modulePortion").style.display = "none";
        }
        else {
            document.getElementById("modulePortion").style.display = "none";

        }
    }
})

