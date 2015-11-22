///Parse data
var Parse = require('node-parse-api').Parse;
var APP_ID = "EfLjLMR7WNu4EXtszi483xKZDrsK0n3OB7D5Bdea";
var MASTER_KEY = "qiurCgfywpjh78qKaZ6j7QBbsclnCmy0jckkWuun";
var appParse = new Parse(APP_ID, MASTER_KEY);
var ArrayL = new Array();
var express = require("express");
var app = express();
var port = 8000;
var princeID="";
 var pID="";
var url='localhost'
var levenshtein = require('fast-levenshtein');
var server = app.listen(port);
var io = require("socket.io").listen(server);


///create server
app.use(express.static(__dirname + ''));
console.log('Simple static server listening at '+url+':'+port);

app.get('', function (req, res) {
  res.setHeader('Content-Type', 'text/plain; charset=utf-8')
  res.end('YOUR SERVER IS RUNNING')

})


io.sockets.on('connection', function (socket) {



  socket.on('sendToParse', function (data) {

    appParse.insert('DigDig', { UserEmail: data.UserEmail, PaleoID:data.PaleoID, Title:data.Title, SRC:data.SRC, PaleoCount:data.PaleoCount, LineArray:data.LineArray, ImgArray:data.ImgArray}, function (err, response) {

    });
  	console.log("entry made");
  });


  socket.on('GetPaleoID', function (data) {
    var LineArrayDistance=0;
    var ParryVal="check this out";
    console.log(data.getID);
    princeID=data.getID;
    appParse.find('DigDig', {where: {PaleoID: data.getID}}, function (err, response) {
      console.log(response.results.length);
      var finalcount=0;
      



      
      
      for(i=0;i<response.results.length;i++){
       finalcount += parseFloat(response.results[i].PaleoCount);
       // console.log(response.results[i].LineArray);
        // ArrayL.push(response.results[i].LineArray);
        levenshtein.getAsync(ParryVal, response.results[i].LineArray, function (err, distance) {
          LineArrayDistance+=parseFloat(distance);
  // console.log("LDistance= "+distance);
});

      }
      



      finalcount = finalcount/response.results.length;
      finalcount = LineArrayDistance/response.results.length;
      console.log("Line Array Distance= "+LineArrayDistance);
      console.log("PaleoCount Distance= "+finalcount);
      socket.emit('FID',{ PaleoID:data.getID, finalcount: finalcount,LineArrayDistance:LineArrayDistance});
      // prince ();

    });
});



function prince (){

  appParse.find('SweetPrince', {where: {PaleoID: princeID}}, function (err, response) {

 console.log(response.results.length);
 if(response.results.length==0){
  console.log("NONE");

// appParse.insert('SweetPrince', { PaleoID: princeID,Final_PaleoCount:finalcount }, function (err, response) {
//   console.log(response);
// });

 }
 else{
  console.log("SOME");
 }
});
}

function update(){
       app.update('SweetPrince', "sUZmmYivUQ", {PaleoID:princeID,Final_PaleoCount: finalcount, Line_Array:LineArrayDistance}, function (err, response3) {
  // console.log(response);
});
}

socket.on('getFromParse', function (data) {
  appParse.find('DigDig', '', function (err, response) {
    console.log(response);
    socket.emit('toScreen',{ ParseData: response });
  });

});


socket.on('Prince', function (data) {
  


 appParse.find('SweetPrince', {where: {PaleoID: data.PaleoID}}, function (err, response) {

 console.log(response.results[0].objectId);
 pID=response.results[0].objectId;
 pID='\''+pID+'\'';
 console.log(pID);
// pID="'"+response.results[0].objectId+"'";
 if(response.results.length==0){
  console.log("NONE");

appParse.insert('SweetPrince', { PaleoID: data.PaleoID,Final_PaleoCount:data.finalcount,Line_Array:data.LineArrayDistance }, function (err, response) {
  console.log(response);
});

 }
 else{
  console.log("SOME");
  appParse.update('SweetPrince', "Fwxwzn1qFh", {Final_PaleoCount:data.finalcount,Line_Array:data.LineArrayDistance}, function (err, response) {
  console.log(response);
});
 }
});





});




});
///INSERT OBJECT
// appParse.insert('Students', { name: 'wole',age:"20" }, function (err, response) {
//   console.log(response);
// });

///FIND ONE
// appParse.find('Students', {objectId: 'rGP6EQe35h'}, function (err, response) {
//   console.log(response);
// });

///FIND MANY
// appParse.find('Students', {where: {name: 'ayo'}}, function (err, response) {
//   console.log(response);
// });

///FIND ALL
// appParse.find('Students', '', function (err, response) {
//   console.log(response);
// });

///DELEATE OBJECT
// appParse.delete('Students', 'rGP6EQe35h', function (err, response) {
//   // response = {} 
// });