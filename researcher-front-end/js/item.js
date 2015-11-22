Parse.initialize("EfLjLMR7WNu4EXtszi483xKZDrsK0n3OB7D5Bdea", "bZ6wJgfR9sulzlnrnN3sRawM5pFIzkgStHC76KDc");

var DigDig = Parse.Object.extend("DigDig");
var query = new Parse.Query(DigDig);
var id = getParameterByName("id");
query.equalTo("PaleoID", id).descending("PaleoCount");

query.find({
  success: function(results) {
    document.getElementById('title').innerText = results[0].get('Title');
    var image = document.createElement('img');
    image.src = results[0].get('SRC');
    document.getElementById('container-item').appendChild(image);

    if (results[0].get('ImgArray') != undefined) {
      var list = document.createElement('ul');
      document.getElementById('container-item').appendChild(document.createElement('h1').appendChild(document.createTextNode("Image Array: ")));
      for (var i = 0; i < results.length; i++) {
        var object = results[i];
        var item = document.createElement('li');
        item.appendChild(document.createTextNode(object.get('ImgArray') + " (reported by " + object.get('UserEmail') + ")."))
        list.appendChild(item);
      }
      document.getElementById('container-item').appendChild(list);
    }

    if (results[0].get('LineArray') != undefined) {
      var list = document.createElement('ul');
      document.getElementById('container-item').appendChild(document.createElement('h1').appendChild(document.createTextNode("Line Array: ")));
      for (var i = 0; i < results.length; i++) {
        var object = results[i];
        var item = document.createElement('li');
        item.appendChild(document.createTextNode(object.get('LineArray') + " (reported by " + object.get('UserEmail') + ". Confidence: " + object.get('PaleoCount') / results.length * 100 + "%)."))
        list.appendChild(item);
      }
      document.getElementById('container-item').appendChild(list);
    }
  },
  error: function(error) {
    alert("Error: " + error.code + " " + error.message);
  }
});

function getParameterByName(name) {
  name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
  var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
  results = regex.exec(location.search);
  return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}