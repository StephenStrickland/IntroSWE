
var classes = null;
var c = "#courseslct";
var p = "#profslct";
var sc = "#sectslct";
var sem = "#semstrslct";
var currentSelection = null;

$(document).ready(function () {
    getClasses();
    
    //setSelections("Prof", p, classes);
    //setSelections("Course", c, classes);
    //setSelections("Semester", sem, classes);
    //setSelections("Section", sc, classes);
});

$(c).change(function () {

    var elem = document.getElementById("courseslct");
    var val = elem.options[elem.selectedIndex].value;
    var ls = getObjects(currentSelection, "Course", val);
    currentSelection = ls;
    populatOptions(ls);


    console.log("value changed for select");
});

$(p).change(function () {

    var elem = document.getElementById("profslct");
    var val = elem.options[elem.selectedIndex].value;
    var ls = getObjects(currentSelection, "Prof", val);
    currentSelection = ls;

    populatOptions(ls);


    console.log("value changed for select");
});

$(sc).change(function () {

    var elem = document.getElementById("sectslct");
    var val = elem.options[elem.selectedIndex].value;
    var ls = getObjects(currentSelection, "Section", val);
    currentSelection = ls;

    populatOptions(ls);


    console.log("value changed for select");
});



$(sem).change(function () {
    var elem = document.getElementById("semstrslct");
    var val = elem.options[elem.selectedIndex].value;
    var ls = getObjects(currentSelection, "Semester", val);
    currentSelection = ls;

    populatOptions(ls);
   // console.log("value changed for select"); setSelects();
});


//$("#courseslct").change(function () { console.log("value changed for select"); setSelects(); });

function getClasses() {

    var l = null;
    $.ajax({
        url: "/Book/GetClasses",
        type: "GET",
        dataType: "json",
        
        success: function (data) {
            handleData(data);
        },
        error: function (e) {
            alert('Bid Update Failure. Please Refresh the Page.');
        }
    });

    
   
   // console.log(JSON.stringify(l));
   // return l;
};

function handleData(data) {
    classes = data;
    currentSelection = data;
    populatOptions(classes);
}

function populatOptions(list) {
    setSelections("Course", c, filterByCourse( list).sort(sortCourse));
    setSelections("Prof", p, filterByProf( list).sort(sortProf));
    setSelections("Semester", sem, filterBySemester(list).sort(sortSem));
    setSelections("Section", sc, filterBySection(list).sort(sortSection));
}

function clearSelects() {
    //getOptions(p, getProfArr(classes));
    //getOptions(s, getSectArr(classes));
    //getOptions(c, getCrseArr(classes));
    currentSelection = classes;
    unHideSelect(p);
    unHideSelect(sc);
    unHideSelect(c);
    unHideSelect(sem);

   // var ssdf = jQuery.inArray("BATES", classes);



}



function unHideSelect(elem)
{
    $(elem + " option").filter(function (val) { return true }).show(0);
    $(elem)[0].selectedIndex = 0;

}



function setSelections(type, elemName, values) {
    if ($(elemName).children().length == 0) {
        $(elemName).append("<option value='" + "'>Please select one</option>");
        for (i = 0; i < values.length; i++) {
            $(elemName).append("<option value='" + values[i][type] + "'>" + values[i][type] + "</option>");
        }
    }
    else {
        var g = getObjects(values, type, "BATES").length > 0
        $(elemName + " option").filter(function () { return getObjects(values, type, this.value).length == 0 }).hide(0);
        $(elemName + " option").filter(function () { return getObjects(values, type, this.value).length > 0 }).show(0);
       // $(elemName + " option").each(function () { alert(this.value)});
        //var n = null;
        //$(elemName).children("option").each(function () {
        //    var l = $.grep(values, function (val) { return (val[type] == this.value || val[type] == ""); })
        //    if (l.length > 0)
        //    {
        //        $(this).show(0);
        //    }
        //    else
        //    {
        //        $(this).hide(0);
        //    }
        //});

    }



}





function filterBySemester(arr) {
    var f = []
    return arr.filter(function (n) {
        return f.indexOf(n.Semester) == -1 && f.push(n.Semester)
    })
}
function filterByCourse(arr) {
    var f = []
    return arr.filter(function (n) {
        return f.indexOf(n.Course) == -1 && f.push(n.Course)
    })
}
function filterByProf(arr) {
    var f = []
    return arr.filter(function (n) {
        return f.indexOf(n.Prof) == -1 && f.push(n.Prof)
    })
}
function filterBySection(arr) {
    var f = []
    return arr.filter(function (n) {
        return f.indexOf(n.Section) == -1 && f.push(n.Section)
    })
}

function sortSection(a, b) {
    if (a.Section > b.Section) return 1;
    if (a.Section < b.Section) return -1;
    return 0;
}

function sortProf(a, b) {
    if (a.Prof > b.Prof) return 1;
    if (a.Prof < b.Prof) return -1;
    return 0;
}



function sortSem(a, b) {
    if (a.Semester > b.Semester) return 1;
    if (a.Semester < b.Semester) return -1;
    return 0;
}



function sortCourse(a, b) {
    if (a.Course > b.Course) return 1;
    if (a.Course < b.Course) return -1;
    return 0;
}




function getProfArr(obj) {
    var l = [];
    for (i = 0; i < obj.length; i++) {
        var s = obj[i].Prof;
        l.push(s);
    }
    
    return l
}

function getCrseArr(obj) {
    var l = [];
    for (i = 0; i < obj.length; i++) {
        var s = obj[i].Course;
        l.push(s);
    }
    return l
}

function getSectArr(obj) {
    var l = [];
    for (i = 0; i < obj.length; i++) {
        var s = obj[i].Section;
        l.push(s);
    }
    return l
}


function getObjects(obj, key, val) {

    var arr = $.grep(obj, function (value) { return value[key] == val; });
    return arr;
    //var objects = [];
    //for (var i in obj) {
    //    if (!obj.hasOwnProperty(i)) continue;
    //    if (typeof obj[i] == 'object') {
    //        objects = objects.concat(getObjects(obj[i], key, val));
    //    } else if (i == key && obj[key] == val) {
    //        objects.push(obj);
    //    }
    //}
    //return objects;
}



//function setSelects() {

//    //var classes = getClasses();
//    if (classes == null) getClasses();
//    //ids

//    console.log("setting vars");
//    var courseSlct = document.getElementById("courseslct");
//    var profSlct = document.getElementById("profslct");
//    var sectSlct = document.getElementById("sectslct");
//    var semstrtSlct = document.getElementById("semstrslct");
//    var semstrVal = semstrtSlct.options[semstrtSlct.selectedIndex].value;
//    var profVal = profSlct.options[profSlct.selectedIndex].value;
//    var CrseVal = courseSlct.options[courseSlct.selectedIndex].value;
//    var SectVal = sectSlct.options[sectSlct.selectedIndex].value;
//    var isCourse = CrseVal != "";
//    var isProf = profVal != "";
//    var isSection = SectVal != "";
//   //setSelections("Prof", )


//    if (isCourse && !isProf && !isSection) {
//        var vals = getObjects(classes, "Course", CrseVal);
//        setSelections("Prof", p, vals );
//        setSelections("Section", sc, vals);
//        //console.log("in case 1");
//        //var classList = getObjects(classes, "Course", CrseVal);
//        //getOptions(p, getProfArr(classList));
//        //getOptions(s, getSectArr(classList));

//    }

//    else if (!isCourse && isProf && !isSection) {

//    }

//    else if (!isCourse && !isProf && isSection) {

//    }

//    else if (isCourse && isProf && !isSection) {

//    }

//    else if (isCourse && !isProf && isSection) {

//    }
//    else if (isCourse && isProf && isSection) {

//    }
//    else if (!isCourse && isProf && isSection) {

//    }



//};


//function getOptions(element, keys)
//{
//    console.log("clearing element");
//    $(element).empty();
//    console.log("element cleared");
//    var string = "";
//    console.log("appending options");
//    $(element).append("<option value='" + "'>Please select one</option>");
//    for (i = 0; i < keys.length; i++)
//    {
//        $(element).append("<option value='" + keys[i] +"'>" + keys[i] + "</option>");
//    }



//}



//function setCourses(crse, prof, sect) {


//}

//function setProfs(prof, crseEL, sectEl, isCrse, isSect) {

//    var profVal = prof.options[prof.selectedIndex].value;
//    var classList = getObjects(classes, "Prof", prof);


//    if (isCrse)
//    {
//        getOptions(crseEL, getCrseArr(classList))
//    }
//    if (isSect)
//    {
//        getOptions(sectEl, getSectArr(classList))
//    }

//}

//function setProfs(prof, crseEL, sectEl, isCrse, isSect) {

//    var profVal = prof.options[prof.selectedIndex].value;
//    var classList = getObjects(classes, "Prof", prof);


//    if (isCrse) {
//        getOptions(crseEL, getCrseArr(classList))
//    }
//    if (isSect) {
//        getOptions(sectEl, getSectArr(classList))
//    }

//}

//function setProfs(prof, crseEL, sectEl, isCrse, isSect) {

//    var profVal = prof.options[prof.selectedIndex].value;
//    var classList = getObjects(classes, "Prof", prof);


//    if (isCrse) {
//        getOptions(crseEL, getCrseArr(classList))
//    }
//    if (isSect) {
//        getOptions(sectEl, getSectArr(classList))
//    }

//}

