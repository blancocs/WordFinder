# WordFinder

this repository has a NET 6 solution to solve Qu Challenge.

to solve the problem, i thought about using tghe DFS (depth-first-search) algorithm, but to be honest, looking for a more efficient algorithm and i found
aho-corasick.

After some examples i found some libreries to implement this algorithm, i didn't want to use them, so i took a look on their github repositories to understand them better.
i was able to understood it and implement the structure of the classes i was needing.

after creating some test cases to valid the happy path, i added differents validation rules using FluentValidation.

Finally, i've included multitask using task library, and using ConcurrentDictionary to prevent concurrency problems when wanting to save or increment repeated words.


To solved this challange i tried to use:

-Onion Clean Architecture with CQRS pattern.
-MediatR library to implement mediator pattern.
-validations using FluentValidation.


for test cases (test are grouped by component):
-FluentAssertions
-Xunit
-Shouldy


Test scenearios:
-matrix size greater than 64x64
-matrix is null
-matrix is empty.
-matrix rows don't have same size.
-wordstream not null.
-Find method returns expected words.
-Find method returns empty List <string>; 

