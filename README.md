# User - Permission Assignment

Welcome to my small assignment!
In this assignment, I have built a minimal version of a production-like project with a little implementation of Domain-driven Design and Unit tests.

## How the project is implemented

For storing staff information, I have implemented a regular tree with each node storing their children nodes. 
With this tree, the **time complexity** of **searching** is **O(n)** since looking for a staff's permissions needs to search for its all children.
For **inserting** a new node, the time complexity is **O(1)**, but with **removing** a node it is **O(n)**.
The space complexity of this tree is, of course, O(n).

You can find the input data from **Data folder** with **input.txt** for the staffs's information and **commands.txt** for dynamic adding/ removing permissions and querying individual user's permission.

## Lauching Guide

The project is implemented on .NET Core 3.1. Double-click on **PermissionsAssignment.sln** to open the solution. It can be runned easily with Visual Studio.

**BUT** if you do not have .NET Core 3.1 on your machine or simply you just don't want to install it just for testing this assignment, don't worry. There is also a containerized-version of thif assignment on Docker Hub. Please run:
```bash
docker run --rm hunghx12/pemissionassignment:latest
```
to verify the output of the program.

Please feel free to give me advice so I can improve my coding skill.
