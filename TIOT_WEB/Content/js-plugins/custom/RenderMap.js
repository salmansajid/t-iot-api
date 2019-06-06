function LoadMap(markers, bounds, arraymarkers, map) {
    var iconURLPrefixGreen = '../Content/images/Markers/green_9.png';
    var iconURLPrefixRed = '../Content/images/Markers/red_9.png';
    //var icons = [iconURLPrefix + 'red_p.png', iconURLPrefix + 'green_9.png'];

    var currWindow = false;
    if (markers.speed > 3) {
        var image = new google.maps.MarkerImage(iconURLPrefixGreen, null, null, null, new google.maps.Size(55, 55));
    }
    else {
        var image = new google.maps.MarkerImage(iconURLPrefixRed, null, null, null, new google.maps.Size(55, 55));
    }


    var data = markers;

    if ((data.latitude != 0.0 && data.longitude != 0.0) || (data.latitude != 0 && data.longitude != 0)) {
        var myLatlng = new google.maps.LatLng(data.latitude, data.longitude);
        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            title: data.number,
            speed: data.speed,
            icon: image,
        });
        arraymarkers.push(marker);

        bounds.extend(marker.getPosition());
        (function (marker, data) {


            var g_InfoWindow = new google.maps.InfoWindow();
            // Attaching a click event to the current marker
            google.maps.event.addListener(marker, "click", function (e) {
                map.setZoom(18);
                if (currWindow) {
                    g_InfoWindow.close();
                }

                var h3comment = "";
                h3comment = "<h3 style='font-size: 10px; margin: 0px; padding: 0px;height:11px;'>" + data.comment + "</h3>";
                var infoContent = " <div id='iw-container' style=' position: relative; background-color: #fefefe;padding: 0;height:auto; box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);'>" +
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

"<div class='modal-footer' id='IFfooter' style='height: 26px;background-color: #337ab7; border-bottom-left-radius: 1em;border-bottom-right-radius:1em;'>" +
        "</div>" +
 " </div>" +

"<div style='display:none;'>" +
                     getReverseGeoCoding4Marker(data.latitude, data.longitude);
                "</div>" +
                "</div>";

                currWindow = g_InfoWindow;
                g_InfoWindow.setContent(infoContent)

                google.maps.event.addListener(g_InfoWindow, 'domready', function () {
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

                g_InfoWindow.open(map, marker);

            });
            google.maps.event.addListener(map, "click", function (event) {
                g_InfoWindow.close();
            });
        })(marker, data);
    }

}


function getReverseGeoCoding4Marker(lat, lng) {
    setTimeout(function () {
        var address = getReverseGeocodingData(lat, lng);
        $('#map_marker_marquee').hide();
        //if (address) {
        //    $('#map_marker_address').text(address);

        //}
        //else {
        //    $('#map_marker_address').text('Address not found!.');

        //}

    }, 1000);
}
function getReverseGeocodingData(lat, lng) {

    var address = null;
    var latlng = new google.maps.LatLng(lat, lng);
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[1]) {
                /*map.setZoom(11);
                marker = new google.maps.Marker({
                    position: latlng,
                    map: map
                });
                infowindow.setContent(results[1].formatted_address);
                infowindow.open(map, marker);*/
                address = results[1].formatted_address;
                $('#map_marker_address').text(address.substring(0, 50) + "...");
                $("#map_info_window_address").attr("title", address);
            } else {
                address = 'No results found';
                $('#map_marker_address').text(address);
                $("#map_info_window_address").attr("title", address);
            }
        } else {
            address = 'Geocoder failed due to: ' + status + ".";
            $('#map_marker_address').text(address);
            $("#map_info_window_address").attr("title", address);
        }
        return address;
    });

}

//function deleteMarkers() {
//    if (arraymarkers) {
//        clearMarkers();
//    }
//    arraymarkers = [];
//}
//function clearMarkers() {
//    setAllMap(null);
//}
//function setAllMap(_map) {
//    console.log("_map : " + _map);
//    for (var i = 0; i < arraymarkers.length; i++) {
//        arraymarkers[i].setMap(null);
//    }

//}



