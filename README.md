## Notes
I went with EF over Dapper because EF support for SQLite was better. I like EF for basic operations but as things become more complex I prefer the level of control Dapper provides.

Naming a project BLL (Business Logic Layer) feels a bit dated but was a good fit for this project.

With regards to comments, I prefer to write self documenting code and save comments for explaining business logic.

## Things I would do differently with a real project
1. I would not include the avatar image on the customers table. In my experience objects that represent a customer are used a lot and much of the time the avatar image would not be needed. Including it on that table would unnecessary overhead.
1. I would create a dedicated endpoint for fetching the customer avatar. We would likely only want to use it in a few places so it would be easier to just include a property on the customer model that is a url for fetching the avatar image. Then we could just go get it when we need it.
1. Adding the avatar api call directly in the create customer code makes us dependant on that api. If they went down we would not be able to create customers. It would be better to create the customer then generate and save the avatar afterwards.
1. Unit test coverage is very sparse. The few I created were just to demonstrate that I like to use the given/when/then approach to creating unit tests.
1. Exception handling is not robust enough. What is there is just an example of how I would handle it.
1. I probably wouldn't return exception messages on failed API calls. Sometimes it's a good practice, sometimes not.
1. I wouldn't typically use hardcoded strings for things like the avatar api. I would put those in a settings file and get them from the configurations manager.
1. My approach to model validation wouldn't scale so well. However, for the size of this project it was a nice simple solution. Same with mapping the customer db class to the customer model class.
1. The DateOnly JsonConverter requires the date string to be yyyy-MM-dd which is constrictive enough to be a pain to use.
1. Depending on the requirements I would probably be checking to make sure that a customer with a given name and DoB doesn't already exist before creating one.
1. I am letting EF make decisions about the column datatypes on the customer table that I would explicitly specify.
