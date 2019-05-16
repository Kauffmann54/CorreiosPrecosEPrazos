# Correios Preços e Prazos

Firebase REST API wrapper for the .NET & Xamarin.
Changes are sent to all subscribed clients automatically, so you can update your clients in realtime from the backend.
  
IMPORTANT : v1 docs moved here.
Installation (NuGet)
//**Install v2**
Install-Package FireSharp

//**Install v1**
Install-Package FireSharp -Version 1.1.0
Usage
FirebaseClient uses Newtonsoft.Json by default.
How can I configure FireSharp?

  IFirebaseConfig config = new FirebaseConfig
  {
     AuthSecret = "your_firebase_secret",
     BasePath = "https://yourfirebase.firebaseio.com/"
  };
IFirebaseClient  client = new FirebaseClient(config);
So far, supported methods are :
Set
var todo = new Todo {
                name = "Execute SET",
                priority = 2
            };
SetResponse response = await _client.SetAsync("todos/set", todo);
Todo result = response.ResultAs<Todo>(); //The response will contain the data written
Push
 var todo = new Todo {
                name = "Execute PUSH",
                priority = 2
            };
PushResponse response =await  _client.PushAsync("todos/push", todo);
response.Result.name //The result will contain the child name of the new data that was added
Get
 FirebaseResponse response = await _client.GetAsync("todos/set");
 Todo todo=response.ResultAs<Todo>(); //The response will contain the data being retreived
Update
var todo = new Todo {
                name = "Execute UPDATE!",
                priority = 1
            };

FirebaseResponse response =await  _client.UpdateAsync("todos/set", todo);
Todo todo = response.ResultAs<Todo>(); //The response will contain the data written
Delete
FirebaseResponse response =await  _client.DeleteAsync("todos"); //Deletes todos collection
Console.WriteLine(response.StatusCode);
Listen Streaming from the REST API
EventStreamResponse response = await _client.OnAsync("chat", (sender, args, context) => {
       System.Console.WriteLine(args.Data);
});

//Call dispose to stop listening for events
response.Dispose();
Release Notes
2.1
Firesharp now is a Net Standard library, so it's available for every platform.
2.0
Use Microsoft HTTP Client Libraries instead of RestSharp
FireSharp is now Portable Library
Supports Streaming from the REST API (Firebase REST endpoints support the EventSource / Server-Sent Events protocol.)
It is fully asynchronous and designed to be non-blocking
More information about Firebase and the Firebase API is available at the official website.
