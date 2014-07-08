<!doctype html>
<html>
<head>
    @ModelType WCMApp.BasicForm
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>E-SEWO</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    @Scripts.Render("~/Scripts/js/jscolor/jscolor.js")
    @Scripts.Render("~/Scripts/js/AJAX.js")
    @Scripts.Render("~/Scripts/js/misc.js")
    @Scripts.Render("~/Scripts/js/wcm.js")
    @Scripts.Render("~/Scripts/js/SEWO.js")
    @Scripts.Render("~/Scripts/js/Media.js")
    @Styles.Render("~/Content/normalize.css")
    @Styles.Render("~/Content/FormClasses.css")
    @Styles.Render("~/Content/Media.css")
</head>

<body onload="mobileView()">
    <table>
        <tr>
            <td width="25px"></td>
            <td width="45px"></td>
            <td width="96px"></td>
            <td width="168px"></td>
            <td width="168px"></td>
            <td width="72px"></td>
            <td width="72px"></td>
            <td width="144px"></td>
            <td width="36px"></td>
            <td width="72px"></td>
            <td width="96px"></td>
            <td width="48px"></td>
            <td width="48px"></td>
        </tr>
        <tr class="dkgrey whitetext">
            <td colspan="13" style="text-align: center;"><h1>Emergency Work Order (EWO)</h1></td>
        </tr>
        <tr class="ltgrey">
            <td colspan="3" height="10px">EWO Number</td>
            <td>Department</td>
            <td>Machine</td>
            <td colspan="2">Type of Failure</td>
            <td>Shift</td>
            <td colspan="2">Date Occurred</td>
            <td>Date Started</td>
            <td colspan="2">Date Finished</td>
        </tr>
        <tr>
            <td colspan="3"><input name="ID" type="text" value="@Model.Fields("ID")" /></td>
            <td>
                <select name="Department" onchange="getZones(getSelectText(this))">
                    <option>Department</option>
                    @Html.Raw(ViewData("DepartmentOptions"))
                </select>
            </td>
            <td>
                <select name="Machine"></select> <!-- onchange="getSubsystems(getSelectText(this))"-->
            </td>
            <td colspan="2">
                <select name="Shift">
                    <option value="">Shift</option>
                    @Html.Raw(ViewData("ShiftOptions"))
                </select>
            </td>
            <td>
                <select name="FailureType">
                    <option value="">Failure Type</option>
                    @Html.Raw(ViewData("FailureTypeOptions"))
                </select>
            </td>
            <td colspan="2"><input name="DateOccurred" type="date" value="@Model.Fields("ID")" /></td>
            <td><input name="DateStarted" type="date" value="@Model.Fields("ID")" /></td>
            <td colspan="2"><input name="DateCompleted" type="date" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr class="ltgrey">
            <td colspan="3" height="10px">Maintenance Person(s)</td>
            <td>Operation</td>
            <td>Sub-system</td>
            <td colspan="3">Component</td>
            <td colspan="2">Time Occurred</td>
            <td>Time Started</td>
            <td colspan="2">Time Finished</td>
        </tr>
        <tr>
            <td colspan="3"><input name="MaintPersons" type="text" value="@Model.Fields("ID")" /></td>
            <td>
                <select name="Zone" onchange="getMachines(getSelectText(this))">
                </select>
            </td>
            <td>
                <select name="Subsystem"><!-- onchange="getComponents(getSelectText(this))"-->
                </select>
            </td>
            <td colspan="3">
                <select name="Component">
                </select>
            </td>
            <td colspan="2"><input name="TimeOccurred" type="time" value="@Model.Fields("ID")" /></td>
            <td><input name="TimeStarted" type="time" value="@Model.Fields("ID")" /></td>
            <td colspan="2"><input name="TimeCompleted" type="time" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td colspan="6" rowspan="2" height="150px"><input name="FailureDescription" type="text" value="@Model.Fields("ID")" /><!-- Description of failure and sketch (if possible) --></td>
            <td colspan="6"><input name="ActivitiesDescription" type="text" value="@Model.Fields("ID")" /><!-- Description of activites --></td>
            <td rowspan="2">Check if temporary repair.</td>
        </tr>
        <tr>
            <td colspan="6"><input name="SpareParts" type="text" value="@Model.Fields("ID")" /><!-- Spare parts used (part number if available). --></td>
        </tr>
        <tr class="ltgrey">
            <td colspan="13" height="10px">Analysis of root causes</td>
        </tr>
        <tr class="ltgrey">
            <td colspan="8" height="30px">5W + 1H Analysis</td>
            <td colspan="5">List of possible causes in order you have approached</td>
        </tr>
        <tr>
            <td rowspan="20" class="dkgrey whitetext">D<br />e<br />f<br />i<br />n<br />i<br />t<br />i<br />o<br />n<br /> <br />o<br />f<br /> <br />p<br />r<br />o<br />b<br />l<br />e<br />m<br /> <br />a<br />n<br />d<br /> <br />a<br />n<br />a<br />l<br />y<br />s<br />i<br />s<br /> <br />o<br />f<br /> <br />r<br />o<br />o<br />t<br /> <br />c<br />a<br />u<br />s<br />e<br />s</td>
            <td colspan="7" height="30px"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td class="ltgrey">1</td>
            <td colspan="4"><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td colspan="7" height="30px"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td class="ltgrey">2</td>
            <td colspan="4"><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td colspan="7" height="30px"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td class="ltgrey">3</td>
            <td colspan="4"><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td colspan="7" height="30px"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td class="ltgrey">4</td>
            <td colspan="4"><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td colspan="7" height="30px"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td class="ltgrey">5</td>
            <td colspan="4"><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td colspan="7" height="30px"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td class="ltgrey">6</td>
            <td colspan="4"><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td colspan="6" class="ltgrey" height="30px">Check on Possible Causes (Should only be one)</td>
            <td colspan="6" class="ltgrey">Type of Root Cause</td>
        </tr>
        <tr>
            <td class="ltgrey" height="30px">1</td>
            <td colspan="5"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td colspan="6" rowspan="2">
                <select name="RootCause" onchange="getEWOSecondaryRootCauses(getSelectText(this))">
                    @Html.Raw(ViewData("RootCauseOptions"))
                </select>
            </td>
        </tr>
        <tr>
            <td class="ltgrey" height="30px">2</td>
            <td colspan="5"><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td class="ltgrey" height="30px">3</td>
            <td colspan="5"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td colspan="6" rowspan="5">
                <select name="SecondaryRootCause" onchange="getEWOActions(getSelectText(this))"></select>
            </td>
        </tr>
        <tr>
            <td class="ltgrey" height="30px">4</td>
            <td colspan="5"><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td class="ltgrey" height="30px">5</td>
            <td colspan="5"><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td class="ltgrey" height="30px">6</td>
            <td colspan="5"><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td class="ltgrey" height="30px">No.</td>
            <td colspan="5"><input name="" type="text" value="@Model.Fields("ID")" placeholder="Enter Cause Number"/></td>
        </tr>
        <tr>
            <td colspan="6" class="ltgrey" height="10px">5 Why Analysis</td>
            <td colspan="6" rowspan="10">Sketch if necessary</td>
        </tr>
        <tr>
            <td colspan="6" height="20px"><input name="" type="text" value="@Model.Fields("ID")" /><!-- Why 1 --></td>
        </tr>
        <tr>
            <td colspan="6" height="20px"><input name="" type="text" value="@Model.Fields("ID")" /><!-- Why 2 --></td>
        </tr>
        <tr>
            <td colspan="6" height="20px"><input name="" type="text" value="@Model.Fields("ID")" /><!-- Why 3 --></td>
        </tr>
        <tr>
            <td colspan="6" height="20px"><input name="" type="text" value="@Model.Fields("ID")" /><!-- Why 4 --></td>
        </tr>
        <tr>
            <td colspan="6" height="20px"><input name="" type="text" value="@Model.Fields("ID")" /><!-- Why 5 --></td>
        </tr>
        <tr>
            <td rowspan="11" class="dkgrey whitetext">A<br />c<br />t<br />i<br />o<br />n<br />s<br /> <br />T<br />a<br />k<br />e<br />n</td>
            <td colspan="4" class="ltgrey" height="10px">Actions Against Root Causes</td>
            <td class="ltgrey">Who</td>
            <td class="ltgrey">When</td>
        </tr>
        <tr>
            <td height="20px" class="ltgrey">1</td>
            <td colspan="3"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td height="20px" class="ltgrey">2</td>
            <td colspan="3"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td height="20px" class="ltgrey">3</td>
            <td colspan="3"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td colspan="4" class="ltgrey" height="10px">Actions To Sustain Equipment Conditions</td>
            <td class="ltgrey">Who</td>
            <td class="ltgrey">When</td>
            <td colspan="6" rowspan="4">
                <select name="Action" onchange="getEWOPillar(getSelectText(this))">
                </select>
            </td>
        </tr>
        <tr>
            <td height="20px" class="ltgrey">1</td>
            <td colspan="3"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td height="20px" class="ltgrey">2</td>
            <td colspan="3"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td height="20px" class="ltgrey">3</td>
            <td colspan="3"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
        </tr>
        <tr>
            <td height="20px" class="ltgrey">4</td>
            <td colspan="3"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td colspan="6">
                <select name="Pillar">
                </select>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="ltgrey" height="10px">Analysis performed by</td>
            <td colspan="3" class="ltgrey">Results</td>
            <td class="ltgrey">Date</td>
            <td colspan="2" class="ltgrey">Checked by</td>
            <td colspan="3" class="ltgrey">Signature</td>
            <td class="ltgrey">Date</td>
        </tr>
        <tr>
            <td colspan="2" height="20px"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td colspan="3"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="date" value="@Model.Fields("ID")" /></td>
            <td colspan="2"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td colspan="3"><input name="" type="text" value="@Model.Fields("ID")" /></td>
            <td><input name="" type="date" value="@Model.Fields("ID")" /></td>
        </tr>
    </table>
    <script>

    </script>
</body>
</html>