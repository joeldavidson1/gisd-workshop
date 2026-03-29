# Workshop: Gradually Improving Software Design

## Before the Workshop Begins

To start working with this workshop, you should create your own copy of the GitHub repository and clone it to your local machine. This ensures you have access to *all branches* used in the lessons and can make changes independently.

### Step 1: Fork the Repository

Use your preferred git platform to make a fork. Make sure the fork includes all branches.

If you are using GitHub, you can accomplish this using the GitHub UI:
1. Navigate to the original workshop repository on GitHub.
2. Click the **Fork** button in the upper right corner.
3. Unselect the "Copy the main branch only" option. *This step is critical.*
4. Select your GitHub account as the destination for the fork.

This creates a copy of the repository in your account, including all branches.

### Step 2: Clone Your Fork Locally

1. Go to your forked repository on GitHub.
2. Click the **Code** button and copy the repository URL (HTTPS or SSH).
3. Open a terminal on your computer and run:

   ```
   git clone <your-fork-url>
   ```

4. Change into the cloned directory:

   ```
   cd <repo-name>
   ```

### Step 3: Access All Branches

By default, only the default branch is checked out. To see all branches:

1. Fetch all remote branches:

   ```
   git fetch --all --tags
   ```

2. List all branches (local and remote):

   ```
   git branch -a
   ```

3. To work on a specific lesson branch, check it out. E.g., for *Lesson #5* on branch *lesson-05-domain-modeling* execute this command:

   ```
   git switch lesson-05-domain-modeling
   ```

Repeat this for any lesson branch you want to work on.

**Tip:**  
If you want to keep your fork up to date with the original repository, you can add the original as an upstream remote:

```
git remote add upstream <original-repo-url>
git fetch upstream
```

You can then merge or rebase changes from the original repository as needed.

## Working With Lesson Branches

Each lesson in the workshop has dedicated branches. You can list all the lessons using the command:

```
git branch -a
```

These are the branches every lesson has:
- `lesson-NN-topic` - the main branch on which the lesson is developed; initially corresponds to the state of the `main` branch at the time when the lesson starts. You can make changes to this branch as you develop a solution to the lesson.
- `lesson-NN-completed` - the state of the code with the official solution applied; this branch is kept separate from the *regular* branch for the lesson, so that each attendee can develop own solution to the lesson and then compare it to the officially suggested solution (which, to be noted, might not be better than yours!).

There is a tag defined, in the format `lesson-NN-start`, which marks the initial state of the lesson's branch. You can always checkout this state using the command:

```
git switch --detach lesson-NN-start
```

This will bring your Git repository into a detached HEAD state (you aren't on a branch). If you want to save your work starting from this point, create a new local branch immediately:

```
git switch -c my-lesson-NN-solution
```

## Lesson 01 - Everything Is an Object

In this exercise, we start from a very simple class, which exposes several properties. All properties require validation, and so it sneaks into the class, making it unbearably complex. And, on top of complexity, we find out that great deal of validation logic is repeatable. We will need to add it to numerous other classes in the code base, too.

The way out from this unwanted situation will be in understanding that all concepts in a business application warrant a type of their own. Validation, transforms, state management - all that belongs in the type that represents a concept. Any other type would simply contain an instance of the other type and, in that way, gain access to all these.

## Lesson 02 - Removing Branching

In this exercise, we analyze what happens when a single class does two things. All operations require validation, so to discover whether the operation is allowed in the current state of the object. On many occasions, there will also be a nullable field/property on the class, where the missing value acts as a showstopper for certain operations.

This situation is bad on several accounts. Branching around the object's state becomes omnipresent in many methods, needlessly cluttering code and complicating control flow. Methods now have a tendency to throw exceptions, indicating that they are not truly doing what their name communicates. Last but not least, there will inevitably be a combinatorial explosion of tests that now must cover all the combinations of meanings the object might have.

The solution to this problem is in polymorphism. The simplest of all implementations (not always the best one, though) is to split the class into several variants via class inheritance. As the result, all control flows will be straightforward again, and with no superficial branching. The branching will be done by selecting one concrete type or the other.

## Lesson 03 - Implement Defensive Design

In this exercise, we observe the complexity that sneaks into a class implementation, often in form of branching instructions and the use of boolean flags. Such code often defends through throwing exceptions in a rigid control flow. Subsequent rqeuirements are hard to implement due to the complex control flow incurred by the defensive branching.

The solution to this problem is to offload defense to strategies and to the compiler. We choose compile-time analysis every time when there is an option to describe the constraints with types. Let an invalid state impossible via the typed assignment checks.

The remaining verifications and validations can be offloaded to dynamically injected stategies and delegates. The defending class would invoke the strategies to obtain valid state, without knowing what "valid" means in terms of business rules. This coding pattern allows us to inject different concrete strategies depending on the deployment, application request, and other contextual information, while keeping the class implementation complexity at a minimum.

## Lesson 04 - Enforcing Object Validity

One of the principal rules of software design is that every object should be valid on the outset and, if mutable, remain valid after each operation applied to it. Validity violations are sometimes obvious, but sometimes subtle and hard to capture. How can we ensure that the class implementation is correct and safe, then? We can use the idea of class invariants to formalize validity.

A class invariant is a boolean condition that must be true on a new object, and must remain true after each operation on the object. This simple model lets us analyze types, especially the mutable ones, and discover possible runtime issues much easier than by other, more traditional means.

## Lesson 05 - Using Value Objects

Values are omnipresent in our code. Yet, not all values are the same. Some values are of primitive types: int, string, Guid, etc. Each of these is a value type in C#, which grants it some basic properties. Those include immutability and equality comparison, as two fundamental elements in software engineering. Can we attain the same level of conformance with reference types, too?

The answer is yes, and the method to achieve that is through the design of so-called value objects. A value object can be either a value type or a reference type in C#. What makes it stand apart is that it is immutable, and it implements equality members: Equals, GetHashCode, implements IEquatable generic interface, overloads equality and inequality operators.

Once you design a value object, its use is indistinguishable from the use of a plain number or a string. Modern C# will help you with designing value objects through the use of record classes and record structs. There are a few things to keep in mind to make them perfect, but the main body of work to turn a type into a proper value object would be conducted for you by the compiler.

## Lesson 06 - Favoring Immutable Objects

In this lesson, we are investigating the design limitations when we try to achieve two fundamental goals: Encapsulation, and state evolution. It is common to expect objects to evolve their contained values over time. On the other hand, it is common to protect contained objects via encapsulation. The two concepts clash when encapsulation impedes access to mutable state during an operation that requires that access.

A striking solution to this probelm is to use immutable objects inside a larger object. Encapsulation rules for immutable objects are much easier compared to mutable state. You can freely share immutable objects, even publicly, because nobody can change their state. State evolution happens by instantiating new immutable objects and replacing the old ones. The entire operation still remains safe, in the sense that it is possible to validate all changes and retain encapsulation of mutable structures.

## Lesson 07 - Using Design by Contract

Design by Contract was introduced by Bertrand Meyer and first explained to a wide audience in his seminal book, Object-Oriented Software Construction. The idea is deceptively simple: define Boolean conditions that must always evaluate to true. Each condition is either a precondition, a postcondition, or a class invariant. A methdod defines preconditions and, whenever the caller satisfies them, the method guarantees to meet postconditions. Class invariants must always be satisfied after the exeuction of any constructor or method.

By defining these formal conditions, class encapsulation becomes a mathematically precise concept that can be turned into executable code. Beyond capturing the virtues of encapsulation, Design by Contract serves as a powerful tool for ensuring overall software correctness and resilience.

## Lesson 08 - Designing Monadic Types

Monads are the principal tool in functional programming. Despite being object-oriented-first language, C# has a long track record of adopting functional programming concepts, monads included. Built-in types such as `IEnumearble<T>`, `Func<T>`, `Task<T>`, `Nullable<T>`, are all monadic in their nature.

By learning what makes a type to become a monad, and how to effectively employ a monad in code, you will learn much more about object-oriented design. Namely, operations on monads are highly composable, and so they force the programmer to separate a large operation into smaller responsibilities. This lesson will prove once again what many programmers already know: that the best object-oriented code is functional code.

## Lesson 09 - Doing Embarrassingly Simple Design

By advancing the previous designs even futher, we reach a world where state never changes, and invalidity is quite literally unrepresentable. We learn to stop apologizing for the memory overhead of new objects, to see how trivial our most complex logic becomes when we stop managing side effects.

By applying these principles to the entire domain model, you will witness a "magic trick" of software engineering: half of the codebase will simply evaporate. The defensive guards, the null checks, the synchronization locks, the "dirty" flags that clutter modern enterprise code, all are revealed as unnecessary baggage in a deeply immutable design.

This lesson proves that when you move validation to the gates of construction and transitions to pure monadic functions, the resulting code isn't just better. It becomes embarrassingly simple.

## Lesson 10 - Manage the Separation of Concerns

Once a business model is developed, it is all too easy to continue adding responsibilities into it. We must resist this urge and decide where to make a cut. Additional responsibilities would end up in new types, sometimes entire hierarchies of types. New classes might reference the old ones, making it one giant business model but still, each individual type would hold only one responsibility, if possible.

The resulting model separates types into distinct namespaces/packages, so that each namespace groups types related by around common responsibility. This strategy opens the opportunity to separate entire responsibilities into individual projects. This level of flexibility is impossible in designs where one domain class is managing several responsibilities.