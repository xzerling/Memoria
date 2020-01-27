
  

  /*
    import 'ol/ol.css';
    import Map from 'ol/Map';
    import View from 'ol/View';
    
    import Draw from 'ol/interaction/Draw';
    import {Tile as TileLayer, Vector as VectorLayer} from 'ol/layer';
    import {OSM, Vector as VectorSource} from 'ol/source';

  var raster = new TileLayer({
    source: new ol.source.OSM()
    });

    var source = new VectorSource({wrapX: false});
  
    var vector = new VectorLayer({
      source: source
    });*/

  var layers = [
    new ol.layer.Tile({
      source: new ol.source.OSM()
    }),
    new ol.layer.Tile({
      //extent: [-13884991, 2870341, -7455066, 6338219],
        source: new ol.source.TileWMS({
        url: 'http://localhost:8080/geoserver/localhost/wms?',
        params: { 'LAYERS': 'localhost:imgCitra4', 'TILED': true },
        serverType: 'geoserver',
        // Countries have transparency, so do not fade tiles:
        transition: 0
      })
    })
  ];

  /*
    var raster = new ol.TileLayer({
      source: new ol.layer.OSM()
    });
    */
  
    //var source = new ol.VectorSource({wrapX: false});

  /*
    var vector = new VectorLayer({
      source: source
    });
    */
  var map = new ol.Map({
    layers: layers,
    target: 'map',
    view: new ol.View({
      center: [-35.40418, -71.63743],
      zoom: 4
    })
  });

    var typeSelect = document.getElementById('type');

    var draw; // global so we can remove it later
    function addInteraction() {
      var value = typeSelect.value;
      if (value !== 'None') {
        draw = new Draw({
          source: source,
          type: typeSelect.value
        });
        map.addInteraction(draw);
      }
  }

      /**
     * Handle change event.
     */
    typeSelect.onchange = function() {
      map.removeInteraction(draw);
      addInteraction();
    };

    addInteraction();