var submitButName = "Button1";
var geocoder;

function DecimalToDMS(decimalNumber) {
    //var sign = false;
    //if (decimalNumber < 0) sign = true;
    var degrees = truncate(decimalNumber);
    var minutes = truncate((decimalNumber - degrees) * 60);
    var seconds = (((decimalNumber - degrees) * 60) - minutes) * 60;
    degrees = Math.abs(degrees);
    minutes = Math.abs(minutes);
    seconds = Math.abs(seconds);
    //if (sign) degrees = -degrees;
    var retval = new Array();
    retval[0] = degrees;
    retval[1] = minutes;
    retval[2] = seconds;
    retval[3] = decimalNumber < 0;
    return retval;
}

function DoDecimalToDMS(latVal,lngVal) {
    var latInDMS = DecimalToDMS(latVal);
    var lngInDMS = DecimalToDMS(lngVal);
    $("#ddNorthSouth>option:eq(1)").attr('selected', latInDMS.pop());
    $("#ddEastWest>option:eq(1)").attr('selected', lngInDMS.pop());
    $.each(latInDMS, function (key, val) {
        latInDMS[key] = val.toString();
    });
    $.each(lngInDMS, function (key, val) {
        lngInDMS[key] = val.toString();
    });
    return [latInDMS,lngInDMS];
}
var outsider = __doPostBack;
function validatePlace(latlng) {
    //$("#" + submitButName).click();return;
    //__doPostBack(submitButName, '');
    geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[1]) {
                var CountryState = GetCountryAndState(results);
                if (CountryState[0] != 'BR' || CountryState[1] != 'PB') {
                    InvalidateForm();
                    return;
                }
                outsider(submitButName, '');
            }
        } else {
            InvalidateForm();
            return;
        }

    });
}

function TrySubmitForm() {
    if (($("#ddlKmlInput option:selected").val() == 'grausminutossegundos') && ($("#txtDegNS").val() == '7') && ($("#txtMinNS").val() == '8') && ($("#txtSecNS").val() == '0.9528') && ($("#txtDegEW").val() == '36') && ($("#txtMinEW").val() == '47') && ($("#txtSecEW").val() == '15.792')) {
        alert("Informe coordenadas válidas.");  
    }

    if (($("#ddlKmlInput option:selected").val() == 'grausminutossegundos') && (($("#txtDegNS").val() != '7') || ($("#txtMinNS").val() != '8') || ($("#txtSecNS").val() != '0.9528') || ($("#txtDegEW").val() != '36') || ($("#txtMinEW").val() != '47') || ($("#txtSecEW").val() != '15.792'))) {
        if (ValidateCoords()) {
            var Lat = UnfoldConvertDegreeAngleToDouble(RetrieveLatValue());
            if ($("#ddNorthSouth option:selected").val() == 'Sul') Lat = -Lat;
            var Lng = UnfoldConvertDegreeAngleToDouble(RetrieveLngValue());
            if ($("#ddEastWest option:selected").val() == 'Oeste') Lng = -Lng;
            validatePlace(new google.maps.LatLng(Lat, Lng));
        } else {
            alert("As coordenadas não são válidas.");
        }
    }

    if ($("#ddlKmlInput option:selected").val() == 'kml') {

        if (ValidateCoords()) {
            var Lat = UnfoldConvertDegreeAngleToDouble(RetrieveLatValue());
            if ($("#ddNorthSouth option:selected").val() == 'Sul') Lat = -Lat;
            var Lng = UnfoldConvertDegreeAngleToDouble(RetrieveLngValue());
            if ($("#ddEastWest option:selected").val() == 'Oeste') Lng = -Lng;
            validatePlace(new google.maps.LatLng(Lat, Lng));
        } else {
            alert("As coordenadas não são válidas.");
        } 
    }
     
}

function getAddress(event) {
    validatePlace(event.latLng);return;
}

function GetCountryAndState(addr_res) {
    for (var i = 0; i < addr_res.length; i++) {
        var hasCountry = "";
        var hasState = "";
        for (var j = 0; j < addr_res[i].address_components.length; j++) {
            if (hasCountry == "" && jQuery.inArray('country', addr_res[i].address_components[j].types) > -1) {
                hasCountry = addr_res[i].address_components[j].short_name;
            }
            if (hasState == "" && jQuery.inArray('administrative_area_level_1', addr_res[i].address_components[j].types) > -1) {
                hasState = addr_res[i].address_components[j].short_name;
            }
            if (hasCountry != "" && hasState != "") {
                return [hasCountry,hasState];
            }
        }
    }
    return [];
}


function ConvertDegreeAngleToDouble(deg, min, sec) {
    return deg + (min / 60) + (sec / 3600);
}

function UnfoldConvertDegreeAngleToDouble(someArr) {
    return ConvertDegreeAngleToDouble(someArr[0],someArr[1],someArr[2]);
}


function RetrieveLatValue() {
    var retval = RetrieveCoordValue('NS');
    
    return retval;
}

function RetrieveLngValue() {
    var retval = RetrieveCoordValue('EW');
    
    return retval;
}

function RetrieveCoordValue(direction) {
    return RetrieveGenericTxtValue('txt', direction);
}

function RetrieveGenericTxtValue(src, suffix) {
    var retval = [$('#' + src + 'Deg' + suffix).val(), $('#' + src + 'Min' + suffix).val(), $('#' + src + 'Sec' + suffix).val()];
    $.each(retval, function (key, val) {
        retval[key] = parseFloat(val);
    });
    return retval;
}

function ValidateCoord(txt,pattern) {
    return (txt.match(pattern)==null?false:true);
}

function ValidateCoordsFrom(Lat, Lng) {
    var latval = ValidateCoord(Lat.toString(), /^-?([1-8]?[1-9]|[1-9]0)\.{1}\d{1,6}/);
    var lngval = ValidateCoord(Lng.toString(), /^-?([1]?[1-7][1-9]|[1]?[1-8][0]|[1-9]?[0-9])\.{1}\d{1,6}/);
    return latval && lngval;
}

function ValidateCoords() {
    var Lat = UnfoldConvertDegreeAngleToDouble(RetrieveLatValue());
    if ($("#ddNorthSouth option:selected").text() == 'Sul') Lat = -Lat;
    Lat = Lat.toString();
    
    var Lng = UnfoldConvertDegreeAngleToDouble(RetrieveLngValue());
    if ($("#ddEastWest option:selected").text() == 'Oeste') Lng = -Lng;
    Lng = Lng.toString();

    var latval = ValidateCoord(Lat, /^-?([1-8]?[1-9]|[1-9]0)\.{1}\d{1,6}/);
    var lngval = ValidateCoord(Lng, /^-?([1]?[1-7][1-9]|[1]?[1-8][0]|[1-9]?[0-9])\.{1}\d{1,6}/);
    return latval && lngval;
}

/*                              validação                            */

function Validator (type) {
    // static
}

Validator.LatitudeRegex = /^-?([1-8]?[1-9]|[1-9]0)\.{1}\d{1,6}/;
Validator.LongitudeRegex = /^-?([1]?[1-7][1-9]|[1]?[1-8][0]|[1-9]?[0-9])\.{1}\d{1,6}/;

Validator.ValidateRegex = function(txt,pattern) {
    return txt.match(pattern)!=null;
};

Validator.ValidateCoords = function(Lat, Lng) {
    var latval = ValidateRegex(Lat.toString(), LatitudeRegex);
    var lngval = ValidateRegex(Lng.toString(), LongitudeRegex);
    return latval && lngval;
};

function InvalidateForm() {
    alert("As coordenadas não se encontram na Paraíba.");
}

function RetTrue(inputId) {
    if (inputId.val() != "") return true;
    return false;
}

function RetFalse(inputId) {
    return false;
}

var cepRegex = /[0-9][0-9][0-9][0-9][0-9]-[0-9][0-9][0-9]/;
var licitaRegex = /[0-9][0-9][0-9]([0-9])*\/[0-9][0-9][0-9][0-9]/;
var cpfRegex = /[0-9][0-9][0-9]\.[0-9][0-9][0-9]\.[0-9][0-9][0-9]-[0-9][0-9]/;
var cnpjRegex = /[0-9][0-9]\.[0-9][0-9][0-9]\.[0-9][0-9][0-9]\/[0-9][0-9][0-9][0-9]-[0-9][0-9]/;
var telRegex = /\([0-9][0-9]\)\ [0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]/;
var yearRegex = /[0-9][0-9][0-9][0-9]/;

function ValidateCep(inputId) {
    var curVal = inputId.val();
    if (curVal.match(cepRegex) != null) return true;
    return false;
}

function ValidateLicitacao(inputId) {
    var curVal = inputId.val();
    if (curVal.match(licitaRegex) != null) return true;
    return false;
}

function ValidateCpf(inputId) {
    var curVal = inputId.val();
    if (curVal.match(cpfRegex) != null) return true;
    return false;
}

function ValidateCnpj(inputId) {
    var curVal = inputId.val();
    if (curVal.match(cnpjRegex) != null) return true;
    return false;
}

function ValidateCpfCnpj(inputId) {
    if ($("#ddlFisicaOuJuridica").val()[0] == 'J') return ValidateCnpj(inputId);
    return ValidateCpf(inputId);
}

function ValidateEmail(inputId) {
    
}

function ValidateTel(inputId) {
    var curVal = inputId.val();
    if (curVal.length == 14 && curVal != "(__) ____-____" && curVal.match(telRegex) != null) return true;
    return false;
}

function ValidateYear(inputId) {
    var curVal = inputId.val();
    if (curVal.length == 4 && curVal.match(yearRegex)!=null) return true;
    return false;
}
var unfocusedInput = "#ABADB3", focusedDivOk = "#c3e094", focusedInputOk = "#96BC58", transpDiv = "transparent", focusedDiv = "#DADBDE";
function ValidateForm(inputId, lostFocus/*=false*/, cbFunc) {
    lostFocus = (typeof lostFocus !== 'undefined' ? lostFocus : false);
    var targ = $(inputId);
    var tagName = targ.attr('id') + '_tick';
    var imgNode = $('<img src="./img/tick.png" />');

    var spanNode = $('<span style="margin:0 0 -2px 6px"></span>').html(imgNode);
    spanNode.attr('id', tagName);
    var jqSpanSelector = "#" + tagName;
    if (!lostFocus) {
        if (cbFunc(targ)) {
            targ.css("border-color", focusedInputOk);
            targ.parent().css("border-color", focusedDivOk);
            if ($(jqSpanSelector).length == 0) targ.parent().after(spanNode);
        } else {
            targ.css("border-color", unfocusedInput);
            targ.parent().css("border-color", focusedDiv);
            if ($(jqSpanSelector).length > 0) $(jqSpanSelector).detach();
        }
    } else {
        targ.parent().css("border-color", transpDiv);
        if (cbFunc(targ)) {
            targ.css("border-color", focusedInputOk);
            if ($(jqSpanSelector).length == 0) targ.parent().after(spanNode);
        } else {
            targ.css("border-color", unfocusedInput);
            if ($(jqSpanSelector).length > 0) $(jqSpanSelector).detach();
        }
    }
}

function HighlightForm(inputId, lostFocus/*=false*/) {
    lostFocus = (typeof lostFocus !== 'undefined' ? lostFocus : false);
    var targ = $(inputId);
    if (!lostFocus) {
        targ.css("border-color", unfocusedInput);
        targ.parent().css("border-color", focusedDiv);
    } else {
        targ.parent().css("border-color", transpDiv);
        targ.css("border-color", unfocusedInput);
    }
}