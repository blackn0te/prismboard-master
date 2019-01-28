var vanillaCalendar = {
    month: document.querySelectorAll('[data-calendar-area="month"]')[0],
    next: document.querySelectorAll('[data-calendar-toggle="next"]')[0],
    previous: document.querySelectorAll('[data-calendar-toggle="previous"]')[0],
    label: document.querySelectorAll('[data-calendar-label="month"]')[0],
    activeDates: null,
    date: new Date,
    todaysDate: new Date,
    init: function (t) {
        this.options = t, this.date.setDate(1), this.createMonth(), this.createListeners()
    },
    createListeners: function () {
        var t = this;
        this.next.addEventListener("click", function () {
            t.clearCalendar();
            var e = t.date.getMonth() + 1;
            t.date.setMonth(e), t.createMonth();
        }), this.previous.addEventListener("click", function () {
            t.clearCalendar();
            var e = t.date.getMonth() - 1;
            t.date.setMonth(e), t.createMonth();
        })//can add in findDate() funciton here
    },
    createDay: function (t, e, a) {
        var n = document.createElement("div"),
            s = document.createElement("span");

        s.innerHTML = t, n.className = "vcal-date", n.setAttribute("data-calendar-date", this.date), 1 === t && (n.style.marginLeft = 0 === e ? 6 * 14.28 + "%" : 14.28 * (e - 1) + "%"), this.options.disablePastDays && this.date.getTime() <= this.todaysDate.getTime() - 1 ? n.classList.add("vcal-date--disabled") : (n.classList.add("vcal-date--active"), n.setAttribute("data-calendar-status", "active")), this.date.toString() === this.todaysDate.toString() && n.classList.add("vcal-date--today"), n.appendChild(s), this.month.appendChild(n);
    },
    dateClicked: function () {
        var t = this;
        this.activeDates = document.querySelectorAll('[data-calendar-status="active"]');
        for (var e = 0; e < this.activeDates.length; e++) this.activeDates[e].addEventListener("click", function (e) { //click function to see the event

            var dateRecieved = this.dataset.calendarDate;

            dateRecieved = dateRecieved.toString().split(" ");
            alert("Day of the week: " + dateRecieved[0] + "\nMonth: " + dateRecieved[1] + "\nDay: " + dateRecieved[2] + "\nYear: " + dateRecieved[3]);// var popup = document.getElementById("popid");
            //alert("running "+dateRecieved[0]);
            document.getElementById("displayBox").style.display = "block";

            // var display = document.querySelector(".container");
            // display.style.opacity = "0.5";
            var display = document.querySelectorAll(".vcal-date.vcal-date--disabled");
            for (var i = 0; i < display.length; i++) {
                display[i].style.opacity = "0";
                display[i].style.cursor = "default";
            }
            var disBox = document.querySelector(".modal-content");
            document.getElementById("dateSetYear").innerHTML = dateRecieved[0] + " " + document.getElementsByClassName("vcal-header__label")[0].innerHTML;
            var monthNo = "00";
            if (dateRecieved[1] == "Jan") {
                monthNo = "01";
            }
            else if (dateRecieved[1] == "Feb") {
                monthNo = "02";
            }
            else if (dateRecieved[1] == "Mar") {
                monthNo = "03";
            }
            else if (dateRecieved[1] == "Apr") {
                monthNo = "04";
            }
            else if (dateRecieved[1] == "May") {
                monthNo = "05";
            }
            else if (dateRecieved[1] == "Jun") {
                monthNo = "06";
            }
            else if (dateRecieved[1] == "Jul") {
                monthNo = "07";
            }
            else if (dateRecieved[1] == "Aug") {
                monthNo = "08";
            }
            else if (dateRecieved[1] == "Sep") {
                monthNo = "09";
            }
            else if (dateRecieved[1] == "Oct") {
                monthNo = "10";
            }
            else if (dateRecieved[1] == "Nov") {
                monthNo = "11";
            }
            else if (dateRecieved[1] == "Dec") {
                monthNo = "12";
            }
            var dayComp = dateRecieved[2] + "-" + monthNo + "-" + dateRecieved[3];
            console.log("day " + dayComp);
            eventCount(dayComp);

        })
    },
    createMonth: function () {
        for (var t = this.date.getMonth(); this.date.getMonth() === t;) this.createDay(this.date.getDate(), this.date.getDay(), this.date.getFullYear()), this.date.setDate(this.date.getDate() + 1);
        this.date.setDate(1), this.date.setMonth(this.date.getMonth() - 1), this.label.innerHTML = this.monthsAsString(this.date.getMonth()) + " " + this.date.getFullYear(), this.dateClicked()
    },
    monthsAsString: function (t) {
        return ["January", "Febuary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"][t]
    },
    clearCalendar: function () {
        vanillaCalendar.month.innerHTML = "";
    },
    removeActiveClass: function () {
        for (var t = 0; t < this.activeDates.length; t++) this.activeDates[t].classList.remove("vcal-date--selected")
    }
};
//Load Function *****************************************************!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!********************************************

//for the collapsible
var coll = document.getElementsByClassName("collapsible");
for (var i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.display === "block") {
            content.style.display = "none";
        }
        else {
            content.style.width = "200px";
            content.style.height = "200px";
            content.style.display = "block";
        }
    });
}

// Get the modal
var modal = document.getElementById('displayBox');
// Anywhere outside the modal will trigger it to close
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
        document.getElementById('displayBox').style.display = 'none';
        var display = document.querySelectorAll(".vcal-date.vcal-date--disabled");
        for (var i = 0; i < display.length; i++) {
            display[i].style.opacity = "0.5";
            display[i].style.cursor = "not-allowed";
        }
        var element = document.getElementsByClassName("desRead")[0];
        if (element != null) {
            element.parentNode.removeChild(element);
        }
    }
}
// Modal close function
function closeModal() {
    document.getElementById('displayBox').style.display = 'none';
    var display = document.querySelectorAll(".vcal-date.vcal-date--disabled");
    for (var i = 0; i < display.length; i++) {
        display[i].style.opacity = "0.5";
        display[i].style.cursor = "not-allowed";
    }

    var element = document.getElementsByClassName("desRead")[0];
    if (element != null) {
        element.parentNode.removeChild(element);
    }
}
var eventObj;
function findDate(eventIn) {
    this.eventObj = eventIn;
    var name = eventIn.EventName;
    var date = eventIn.Date;

    console.log("name " + name + " date " + date);   

    var dateDivided = date.split("-");
    console.log("date: " + date + "\n date divided: " + dateDivided);

    var thisdate = new Date();
    var lastDay = thisdate.getFullYear();
    var thismonth = thisdate.getMonth();
    var thisday = thisdate.getDate();

    var pagemonth = document.getElementsByClassName("vcal-header__label")[0].innerHTML.split(" ")[0];
    var pageyear = document.getElementsByClassName("vcal-header__label")[0].innerHTML.split(" ")[1];
    if (pagemonth == "January") {
        pagemonth = 1;
    }
    else if (pagemonth == "Febuary") {
        pagemonth = 2;
    }
    else if (pagemonth == "March") {
        pagemonth = 3;
    }
    else if (pagemonth == "April") {
        pagemonth = 4;
    }
    else if (pagemonth == "May") {
        pagemonth = 5;
    }
    else if (pagemonth == "June") {
        pagemonth = 6;
    }
    else if (pagemonth == "July") {
        pagemonth = 7;
    }
    else if (pagemonth == "August") {
        pagemonth = 8;
    }
    else if (pagemonth == "September") {
        pagemonth = 9;
    }
    else if (pagemonth == "October") {
        pagemonth = 10;
    }
    else if (pagemonth == "November") {
        pagemonth = 11;
    }
    else if (pagemonth == "December") {
        pagemonth = 12;
    }
    console.log(pagemonth);
    if (dateDivided[0] == pageyear) {
        //it is this year
        console.log("Same year");
        if (dateDivided[1] == pagemonth) {
            //this year and same month as page
            console.log("Same month");

            var diff = 0;

            var dayNum = parseInt(dateDivided[2]);
            var dayOfEvent = document.querySelectorAll(".vcal-date");
            console.log(typeof dayNum + " day " + dayNum);
            dayOfEvent[dayNum - 1].style.color = "white";
            //compare and cal the days apart
            thisday = parseInt(thisday);
            if (thismonth + 1 == pagemonth) {
                //the month is same as te current month
                // proceed as normal
                diff = dayNum - thisday;
            }
            else if (thismonth + 1 < pagemonth) {
                //the month is later than current month
                //need to include the difference in date
                var lastD = new Date(lastDay, thismonth + 1, 0);
                thisday = parseInt(lastD) - thisday;
                diff = dayNum + thisday;
            }
            console.log("Day of event " + dayNum + " and day today " + thisday + " difference is " + diff);

            if (diff <= 4) {
                //red	
                if (diff >= 0) {
                    dayOfEvent[dayNum - 1].style.backgroundColor = "red";
                    if (checkRepeats(date)) {
                        var keep = dayOfEvent[dayNum - 1].getAttribute("name");
                        keep += name + ",";
                        dayOfEvent[dayNum - 1].setAttribute("name", keep);
                    }
                    else {
                        dayOfEvent[dayNum - 1].setAttribute("name", " " + name + ",");
                    }
                }
                else {
                    //date over 
                    dayOfEvent[dayNum - 1].style.color = "black";
                }
            }
            else if (diff <= 9) {
                //orange
                dayOfEvent[dayNum - 1].style.backgroundColor = "orange";
                if (checkRepeats(date)) {
                    var keep = dayOfEvent[dayNum - 1].getAttribute("name");
                    keep += name + ", ";
                    dayOfEvent[dayNum - 1].setAttribute("name", keep);
                }
                else {
                    dayOfEvent[dayNum - 1].setAttribute("name", " " + name + ",");
                }
            }
            else {
                //green
                dayOfEvent[dayNum - 1].style.backgroundColor = "green";
                if (checkRepeats(date)) {
                    var keep = dayOfEvent[dayNum - 1].getAttribute("name");
                    keep += name + ",";
                    dayOfEvent[dayNum - 1].setAttribute("name", keep);
                }
                else {
                    dayOfEvent[dayNum - 1].setAttribute("name", " " + name + ",");
                }
            }
        }
    }
}

var usedDate = [];
function checkRepeats(date) {
    if (this.usedDate.length == 0) {
        var count = 1;
        console.log("date+count " + date + count);
        this.usedDate.push(date + count);
        return false;
    }
    else {
        console.log("have length");
        for (var i = 0; i < this.usedDate.length; i++) {
            console.log(this.usedDate[i]);
            var dateVal = this.usedDate[i].split("", 10);
            var con = "";
            for (var x = 0; x < dateVal.length; x++) {
                con += dateVal[x];
            }
            dateVal = con;
            console.log(dateVal);
            if (dateVal == date) {
                console.log("ran the if");
                count = this.usedDate[i].split("")[10];
                count = parseInt(count);
                count++;
                //re push in
                this.usedDate[i] = date + count;
                return true;
            }
        }
        count = 1;
        this.usedDate.push(date + count);
        console.log(this.usedDate);
        return false;
    }
    console.log("usedDate " + this.usedDate);
}

function eventCount(dayComp) {
    if (this.usedDate.length == 0) {
        //there is no events

    }
    else {
        //there is/are events
        for (var i = 0; i < this.usedDate.length; i++) {
            var countDay = dayComp.split("-", 1);
            var vcal = document.querySelectorAll(".vcal-date");
            console.log(countDay);

            var check = vcal[parseInt(countDay) - 1].getAttribute("name");
            var divideNum = parseInt(this.usedDate[i].split("")[10]);
            console.log(check + " " + divideNum);

            if (check != null) {
                var divisibleNum = "";
                if (check.split(",")[0] == "null") {
                    divisibleNum = check.split(",").length - 1 / divideNum;
                }
                else {
                    divisibleNum = check.split(",").length / divideNum;
                }

                console.log("check " + divisibleNum)
                // check = document.getElementsByName("Event Cfort");
                var des = "";
                var holder = check.split(",");
                console.log("holder: " + holder + " Divi " + divisibleNum);
                if (check.split(",")[0] == "null") {
                    for (var x = 1; x < divisibleNum; x++) {
                        console.log("Holder in: " + holder[x]);
                        des += holder[x] + " ";
                    }
                    console.log("Equated! " + " des =" + des);
                }
                else {
                    for (var x = 0; x < divisibleNum; x++) {
                        console.log("Holder in: " + holder[x]);
                        des += holder[x] + " ";
                    }
                    console.log("Equated! " + " des =" + des);
                }
                var val = this.usedDate[i].split("", 10);

                var hold = "";
                //for (var x = 0; x < val.length; x++) {
                //    hold += val[x];
                //}
                hold = val[8] + val[9] + val[7] + val[5] + val[6] + val[4] + val[0] + val[1] + val[2] + val[3];

                console.log("val rebuilt: " + hold + " " + typeof hold);
                console.log("val of dayComp: " + dayComp + " " + typeof dayComp);

                if (dayComp === hold) {
                    console.log("val true");
                    var keystring = des.split(" ")[0] + " " + des.split(" ")[1];
                    console.log("val check: " + keystring);
                    var describe = "You have no events going on today!";

                    try {
                        var descStored = this.eventObj.Description();
                        var nameToCompare = this.eventObj.EventName();
                        console.log("DesStored: " + descStored + " name to cmp " + nameToCompare);
                        if (keystring == nameToCompare) {
                            //same string name, can proceed
                            describe = descStored;
                        }
                        else {
                            //not the same string, break out of loop
                            break
                        }
                    }
                    catch (err) {
                        console.log(err);
                    }
                    //var key = localStorage[keystring];
                    //key = JSON.parse(key);//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if (key != null) {
                    //    var describe = key.Description;
                    //}

                    //Create portion
                    var div = document.createElement("div");
                    div.setAttribute("class", "desRead" + " " + des)
                    document.getElementById("popDiv").appendChild(div);
                    var btn = document.createElement("button");
                    btn.setAttribute("class", "collapsible" + " " + des);
                    var btnText = document.createTextNode(des + " ▼");//change the word Des to the event title 
                    btn.addEventListener("click", function () {
                        this.classList.toggle("active");
                        var content = this.nextElementSibling;
                        if (content.style.display === "block") {
                            content.style.display = "none";
                        }
                        else {
                            content.style.width = "200px";
                            content.style.height = "200px";
                            content.style.display = "block";
                        }
                    });
                    btn.appendChild(btnText);
                    document.getElementsByClassName("desRead" + " " + des)[0].appendChild(btn);
                    var div1 = document.createElement("div");
                    div1.setAttribute("class", "content" + " " + des)
                    document.getElementsByClassName("desRead" + " " + des)[0].appendChild(div1);
                    var p = document.createElement("P");
                    var pText = document.createTextNode(describe);//This is where the des will be placed 
                    p.appendChild(pText);
                    document.getElementsByClassName("content" + " " + des)[0].appendChild(p);

                }
                else {
                    console.log("Not the same");
                }
            }
        }
    }
}


function writeCookie(Event, EventID, Date, Time) {
    //Date in the format of DDMMYYYY
    //Time in the format of 24hours clock HHMM
    var date, expires;
    var day = 3;
    //alert("initiated");
    if (day) {
        //alert("EventID " + EventID + " Date " + Date + " Time " + Time);
        // date = new Date();
        // date.setTime(date.getTime()+(days*24*60*60*1000));
        // expires = "; expires=" + date.toGMTString();
        var storeInfo = { "EventID": EventID, "Date": Date, "Time": Time };

        localStorage[Event] = JSON.stringify(storeInfo);
    }

    else {
        expires = "";
    }

    alert("cookies added");
    document.cookie = Event + "=" + EventID + ", " + Date + ", " + Time + " " + expire + "; path=/";
}






