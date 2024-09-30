# Introduction 
Provides classes for fetching data from Schoolsoft API and a small program to demonstrate how to use the classes. A license from Schoolsoft is required for this to work.

# Getting Started
The password used to connect to Schoolsoft is stored in a file named "schoolsoft_api.txt" which is located in the CredentialsDir as specified in appsettings.json.
There is a setting useCache, which if true tries to read data from disk instead of from the API. This is useful for testing and development. The program always writes data read from schoolsoft to disk,
both in json and xml format (as read from schoolsoft).

# Build and Test
Open the solution in Visual Studio and run the project. 
```

