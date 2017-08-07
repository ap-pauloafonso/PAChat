# PAChat
A simple chat written in C# using Socket. The basic idea is that the server receive the data from the client then it broadcast to all connected clients.

The default server port is 7000, however you can change it by passing the port number as argument: `Socket_Server.exe [port]`.

Things to improve:
- Server Side: For each connection the server creates a separate thread, For this project a Thread is considered “Expensive” because for the most part the thread is blocked on CPU waiting for data to arrive. So if there is a lot of users connected the server will consume a lot of memory, since each thread has its own stack.
- Client Side: The message box alert is invoked from the viewmodel which is Little out of standard in MVVM Pattern.
