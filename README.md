# Ensek

## Approach

Because this project required importing a file of unknown size, I would preferably have liked to use message queues but was convinced the development of this kind of solution would take more time than I had available for the test. As it stands, I have taken a pseudo DDD approach, using MediatR to implement in-memory event notification, and EFCore as a preferred ORM.

Normally I would use SQL Server as an RDBMS but I thought I'd switch it up and dabble in a little bit of Postgres, to learn something while doint this test.

Unfortunately I spent most of my time fighting with Postgres and beautifying my api code and didn't get to the front end. I was thinking of using Svelte (I have examples of personal projects, if interested) but would likely have used ReactJS.

## Assumptions
- Negative numbers assumed to be invalid
- Numeric length greater than 5 characters assumed to be invalid