//start dashboard ajax
function getObjectbyId(objId) {
    var returnedObject = {};
    $.ajax({
        type: "POST",
        async: false,
        url: "Dashboard.aspx/GetObjectbyId",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ObjectId: objId }),
        success: function (result) {
            var data = result.d;
            data = $.parseJSON(data);
            if (data != "") {
                returnedObject['clientId'] = data['TavlClient'];
                returnedObject['groupId'] = data['TavlGroup'];
                returnedObject['IP'] = data['TavlIP'];
                //returnedObject = clientId + ',' + groupId + ',' + IP;
                //getObjects(clientId, groupId, IP)
            }
            else {
            }
        },
        error: function (xhr, status, error) {
        }
    })
    return returnedObject;
}

function getObjects(ClientId, GroupId, IP, map) {
    var bounds = new google.maps.LatLngBounds();
    var arraymarkers = [];
    markerClusterer = new MarkerClusterer(map);
    //var res ;
    $.ajax({
        type: "POST",
        url: "Dashboard.aspx/TavlObject",
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ClientId: ClientId, GroupId: GroupId, IP: IP }),
        success: function (result) {
            var response = result.d;
            response = $.parseJSON(response);
            if (response == "") {
            }
            else {
                $('#loading').hide();
                $.each(response, function () {
                    LoadMap(this, bounds, arraymarkers, map);
                });

                markerClusterer.addMarkers(arraymarkers, true);
                markerClusterer.setGridSize(30);
                markerClusterer.setMaxZoom(15);
                map.setCenter(bounds.getCenter());
                map.fitBounds(bounds);

                $("#example").css('visibility', 'visible');
                $("#tblexample").css("background-color", "#337ab7");

                $('#example').dataTable({
                    data: response,
                    "columns": [
                        {
                            "mData": null,
                            "mRender": function (data, type, response) {
                                return '<span class="tavldticon' + response['_speed'] + '"> </span>' + response['number'] + '<br><span style="font-size:8px;">(' + response['comment'] + ')</span>';
                            }
                        },
                    ],
                    "bLengthChange": false,
                    "oLanguage": {
                        "sSearch": '',
                    },

                    paging: false,
                    destroy: true,
                    fixedHeader: true,
                    'scrollY': '100vh',
                    "fnDrawCallback": function (oSettings) {
                        $(oSettings.nTHead).hide();
                    },
                });
                $('div.dataTables_filter input').addClass('form-control myclass');
                $('#example_filter').removeClass('dataTables_filter');
                $('#example').closest('.dataTables_scroll').css('background-color', 'white');
                $('#example').closest('.dataTables_scrollBody').css('max-height', 'calc(100vh - 12.1em)');
                $('.dataTables_info').css('font-size', '9px');
                $('.dataTables_info').css('color', 'white');
                $('.dataTables_scrollHead').css("background-color", "#337ab7");
                $('.dataTables_scrollBody').css("border", "none");
                $('table.dataTable.no-footer').css('border', 'none')
                $(".myclass").attr("placeholder", "Search");

                $('#example tbody').on('click', 'tr', function () {
                    var table = $('#example').DataTable();
                    var data = table.row(this).data();

                    var h3comment = "";
                    h3comment = "<h3 style='font-size: 10px; margin: 0px; padding: 0px;height:11px;'>" + data.comment + "</h3>";
                    var infoContent = " <div id='iw-container' class='yttt' style=' position: relative; background-color: #fefefe;padding: 0;height:auto; box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);'>" +
               "<div class='modal-header' style='background-color: #337ab7; color:white ;border-top-left-radius: 1em;border-top-right-radius: 1em;'>" +
    "<h3 style='font-size:16px;margin-bottom:0px;'>" + data.number + "</h3>" + h3comment +
    "</div>" +
              "<div class='iw-title' >" +
              "<div class='modal-body'>" +
               "<table><tr>" +
     "<tr><td><b>Date:</b></td>" +
    "<td style='padding-right:12px;'>" + data.date + "</td>" +
     "<td><b>Time:</b></td>" +
     "<td style='padding-right:12px;'>" + data.time + "</td></tr>" +
     "<tr><td><b>Speed:</b></td>" +
     "<td colspan='3' >" + data.speed + "</td></tr>" +
     "<tr><td ><b>Address</b></td><td id='map_info_window_address' colspan='3' >" +
                   "<div id='map_marker_marquee'><marquee  direction='right'>........</marquee></div>" +
                     "<div id='map_marker_address'></div>" +
                             "</td></tr>" +
      "<tr><td ><b>Lat:</b></td>" +
         "<td >" + data.latitude + "</td>" +
         "<td> <b> Long:</b></td>" +
       "<td>" + data.longitude + "</td></tr>" +
     "</table></div>" +

    "<div class='modal-footer' id='IFFFooter' style='height: 26px;background-color: #337ab7; border-bottom-left-radius: 1em;border-bottom-right-radius:1em;'>" +
            "</div>" +
     " </div>" +

    "<div style='display:none;'>" +
                         getReverseGeoCoding4Marker(data.latitude, data.longitude);
                    "</div>" +
                    "</div>";
                    var ts = $('.yttt');
                    if (ts) {
                        ts.remove();

                    }
                    var infowindow = new google.maps.InfoWindow({
                        content: infoContent,
                        position: new google.maps.LatLng(data['latitude'], data['longitude'])
                    });

                    infowindow.open(map);




                    google.maps.event.addListener(infowindow, 'domready', function () {
                        // Reference to the DIV which receives the contents of the infowindow using jQuery
                        var iwOuter = $('.gm-style-iw');

                        /* The DIV we want to change is above the .gm-style-iw DIV.
                         * So, we use jQuery and create a iwBackground variable,
                         * and took advantage of the existing reference to .gm-style-iw for the previous DIV with .prev().
                         */
                        var iwBackground = iwOuter.prev();

                        // Remove the background shadow DIV
                        iwBackground.children(':nth-child(2)').css({ 'display': 'none' });

                        // Remove the white background DIV
                        iwBackground.children(':nth-child(4)').css({ 'display': 'none' });

                        // Remove close button
                        var iwCloseBtn = iwOuter.next();
                        iwCloseBtn.css({ 'display': 'none' });

                    });



                    google.maps.event.addListener(map, "click", function (event) {
                        infowindow.close();
                    });

                    if ($(this).hasClass('selected')) {
                        $(this).removeClass('selected');
                    }
                    else {
                        table.$('tr.selected').removeClass('selected');
                        $(this).addClass('selected');
                    }
                });

            }
        },
        error: function (xhr, status, error) {
            toastr.warning('Google map is not responding while', 'Request', { positionClass: 'toast-bottom-right' });
        },
        beforeSend: function () {
            loaderIn();
        },
        complete: function () {
            loaderOut();

        },
    })
}

function GetAlertsByClient(_clId) {
    var now = new Date();
    var starttime = localStorage.getItem('LoginedTime').toLocaleString();
    var endtime = new Date(now.getTime()).toLocaleString();
    $.ajax({
        type: "POST",
        async: true,
        url: "Dashboard.aspx/NotificationsByClient",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ clientId: _clId, starttime: endtime, endtime: starttime }),
        success: function (result) {
            var data = result.d;
            if (data != "") {
                var result = JSON.parse(data);
                for (i = 0; i < result.length; ++i) {
                    var text = result[i];
                    var message = text['Message'];
                    var time = text['DateTime'];
                    var ALERT = new Date(time).toLocaleString();
                    toastr.success(message, 'ALERT! ' + ALERT, { positionClass: "toast-bottom-right" });
                    var nwdt = text['DateTime'];
                    localStorage.setItem('LoginedTime', nwdt);
                    var lt = localStorage.getItem('LoginedTime');
                }
            } else {
            }
        },
        error: function (xhr, status, error) {
        }
    })
}

function GetAlertsByGroup(groupId) {
    var now = new Date();
    var starttime = localStorage.getItem('LoginedTime').toLocaleString();
    var endtime = new Date(now.getTime()).toLocaleString();
    $.ajax({
        type: "POST",
        async: true,
        url: "Dashboard.aspx/NotificationsByGroup",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ GroupId: groupId, starttime: endtime, endtime: starttime }),
        success: function (result) {
            var data = result.d;
            if (data != "") {
                var result = JSON.parse(data);
                for (i = 0; i < result.length; ++i) {
                    var text = result[i];
                    var message = text['Message'];
                    var time = text['DateTime'];
                    var ALERT = new Date(time).toLocaleString();
                    toastr.success(message, 'ALERT! ' + ALERT, { positionClass: "toast-bottom-right" });
                }


            }
            else {
                //console.log("NO ALERT");
            }
        },
        error: function (xhr, status, error) {
            //alert(error);
        }
    })
}

function GetNotificationByClient(clientId) {
    var now = new Date();
    var currentTime = new Date(now.getTime()).toLocaleString();
    var oneHourBefore = new Date(now.getTime() - (1 * 1000 * 60 * 60)).toLocaleString();
    $.ajax({
        type: "POST",
        async: true,
        url: "Dashboard.aspx/NotificationsByClient",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ clientId: clientId, starttime: oneHourBefore, endtime: currentTime }),
        success: function (result) {
            var trHTML = '';
            var data = result.d;
            if (data != "") {
                console.log(data);
                var result = JSON.parse(data);
                $('#tblnotify').empty();
                for (i = 0; i < result.length; ++i) {
                    var text = result[i];
                    var message = text['Message'];
                    var Category = text['CategoryName'];
                    var temptime = text['DateTime'];
                    var time = new Date(temptime).toLocaleString();
                    trHTML += '<tr><td>' + '<div class="row"><div class="col-sm-2"><img class="notify-img" src="' + Category + '" /></div><div class="col-sm-10"><p class="notifyMessage"> ' + message + ' </p> <br /> <p style="font-size: 9px; float:left"><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp &nbsp ' + time + ' </p></div></div> ' + '</td></tr>';
                }
            }
            else {
                $('#tblnotify').empty();
                trHTML += '<tr><td>' + '<div class="row"> <h4> No Alerts Found! </h4></div> ' + '</td></tr>';
            }
            $('#tblnotify').append(trHTML);
        },

        error: function (xhr, status, error) {
        }
    })
}

function GetNotificationByGroup(groupId) {
    var now = new Date();
    var currentTime = new Date(now.getTime()).toLocaleString();
    var oneHourBefore = new Date(now.getTime() - (1 * 1000 * 60 * 60)).toLocaleString();
    $.ajax({
        type: "POST",
        async: true,
        url: "Dashboard.aspx/NotificationsByGroup",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ GroupId: groupId, starttime: oneHourBefore, endtime: currentTime }),
        success: function (result) {
            var trHTML = '';
            var data = result.d;
            if (data != "") {
                var result = JSON.parse(data);
                $('#tblnotify').empty();
                for (i = 0; i < result.length; ++i) {
                    var text = result[i];
                    var message = text['Message'];
                    var Category = text['CategoryName'];
                    var temptime = text['DateTime'];
                    var time = new Date(temptime).toLocaleString();
                    trHTML += '<tr><td>' + '<div class="row"><div class="col-sm-2"><img src="'+ Category +'"/></div><div class="col-sm-10"><p class="notifyMessage"> ' + message + ' </p> <br /> <p style="font-size: 9px; float:left" ><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp &nbsp ' + time + ' </p></div></div> ' + '</td></tr>';
                }
            }
            else {
                $('#tblnotify').empty();
                trHTML += '<tr><td>' + '<div class="row"> <h4> No Alerts Found! </h4></div> ' + '</td></tr>';
            }
            $('#tblnotify').append(trHTML);
        },
        error: function (xhr, status, error) {
        }
    })
}

function notificationCount() {
    var now = new Date();
    var oneHourbftime = new Date(now.getTime() - (1.5 * 1000 * 60 * 60)).toLocaleString();
    var clientId = $('#MainContent_ddlclient').val();
    $.ajax({
        type: "POST",
        async: true,
        url: "Dashboard.aspx/NotificationsByClient",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ clientId: clientId, lastTime: oneHourbftime }),
        success: function (result) {
            var data = result.d;
            if (data != "") {
                var result = JSON.parse(data);
                $('#notifCount').text = result;
                console.log(result);
            }
        },
        error: function (xhr, status, error) {
        }
    })
}
//end dashboard ajax