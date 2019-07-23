# PaymentGateway

I have added TODOs where I made suggestions for more, or had questions for stakeholders. 

I've implemented basic authentication, but not using TDD. I understand about salting and hashing and how to store credentials. Ideally though I would like to implement JWT authentication (I was just too lazy). Any base64 username and password combination will work, like tom:oliver

I've used a concurrent in memory repository. I would use Sql for this but would not use entity, the project just doesn't seem big enough to warrent it. In the past I've created hybrid stores for additional speed (i.e. an in memory which passes through to a database)

I've tried to follow and Onion based architecture, with my domain in models, and everything building from them.

The API is restful, and returns the location of the created resource.

The solution does have a docker file, however docker for windows does not work on Windows 10 Home as it requires the hyper-v. I played around with docker toolbox a bit to try to get that to work but decided it would take too long.

A note on unit tests, I always add _Tests to the end of a class name to stop name conflicts. I also make test classes for interfaces abstract, and any class that implements that interface must pass those basic tests. I find it works well for enforcing Liskov Substitution. I am not religious though about this, and would adopt and TDD strategy provided.

There may be some namespaces that are not correct, I'm used to resharper which can do it all for you, but I don't have it at home.

I've created a basic integration test project, a real one would have many test conditions, and some more structure.
