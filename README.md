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