using System;
using System.Collections.Generic;
//Nathan Bonzo's To-Do List application.
Console.Clear();

List<string> todoList = new List<string>(); // List that will contain the to-do list items.

bool exit = false;

LoadTasks();
Console.WriteLine("Welcome to the To-Do List application! Please select an option below to get Started!");
const string todoListFile = "todo_list.txt";
while (!exit)
    {
        // Our menu that the user will see and understand what they should do.
        Console.WriteLine(@"
1. Add Task
2. Remove Task
3. Prioritize Task
4. Check Off or Uncheck Task
5. View Tasks
6. Exit Application"); 
       Console.Write("What would you like to do: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddTask();
                break;
            case "2":
                RemoveTask();
                break;
            case "3":
                Priority();
                break;
            case "4":
                CheckOff();
                break;
            case "5":
                ViewTasks();
                break;
            case "6":
                SaveTasks();
                exit = true;
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }

        Console.WriteLine();
    }



void AddTask()
{
    Console.Write("Enter the task: ");
    string? task = Console.ReadLine();
    try
    {
        if (task != null)
        {
            todoList.Add("[] " + task);
            Console.WriteLine("Task added successfully!");
        }
    }
    catch (Exception)
    {
        Console.WriteLine("Task failed to be added.");
    }
}

void RemoveTask()
{
    if (todoList.Count == 0) // For if there are no tasks
    {
        Console.WriteLine("No tasks to remove.");
        return;
    }

    Console.WriteLine("Current Tasks:");
    for (int i = 0; i < todoList.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {todoList[i]}");
    }

        Console.Write("Enter the number of which task you wish to remove: ");
        int removeIndex = Convert.ToInt32(Console.ReadLine());
        if (removeIndex >= 1 && removeIndex <= todoList.Count)
        {
            todoList.RemoveAt(removeIndex - 1);
            Console.WriteLine("Task removed successfully!");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
void Priority()
{
    if (todoList.Count == 0)
    {
        Console.WriteLine("No tasks to prioritize.");
        return;
    }
    Console.WriteLine("Current Tasks:");
    for (int i = 0; i < todoList.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {todoList[i]}");
    }
    Console.Write("Which task do you want to prioritize? ");
    int priorityChoice = Convert.ToInt32(Console.ReadLine());
    if (priorityChoice >= 1 && priorityChoice <= todoList.Count)
    {
        Console.WriteLine("Priority goes to where the lower the number, the higher the priority. (E.g. 1 is the highest)");
        Console.Write("Enter the priority: ");
        int priorityOrder = Convert.ToInt32(Console.ReadLine());
        if (priorityOrder >= 1)
        {
            string temp = todoList[priorityChoice - 1];
            todoList[priorityChoice - 1] = todoList[priorityOrder - 1];
            todoList[priorityOrder -1] = temp;
            //todoList.RemoveAt(priorityChoice - 1);
            //todoList.Insert(priorityOrder - 1, todoList[priorityChoice - 1]);
            //todoList[priorityOrder - 1] = priorityOrder + ". " + todoList[priorityChoice - 1];
        }
    }
}


void CheckOff()
{
    if (todoList.Count == 0)
    {
        Console.WriteLine("No tasks to check off or uncheck.");
        return;
    }
    Console.WriteLine("Current Tasks:");
    for (int i = 0; i < todoList.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {todoList[i]}");
    }
    Console.Write("Enter the number of the task you want to check off or uncheck: ");
    int checkOffItem = Convert.ToInt32(Console.ReadLine());
    if (checkOffItem >=1 && checkOffItem <= todoList.Count)
    {
        if (todoList[checkOffItem - 1].Contains("[]"))
        {
            todoList[checkOffItem - 1] = "[X] " + todoList[checkOffItem - 1].Substring(3);
            Console.WriteLine("Task Checked Off.");
        }
        else if (todoList[checkOffItem - 1].Contains("[X]"))
        {
            todoList[checkOffItem - 1] = "[ ] " + todoList[checkOffItem - 1].Substring(3);
            Console.WriteLine("Task Unchecked.");
        }
    }
    else
        Console.WriteLine("Invalid input. Please enter a valid number.");
}

void ViewTasks()
{
    Console.WriteLine("Current Tasks:");
    if ( todoList.Count == 0)
    {
        Console.WriteLine("No tasks to display.");
    }
    else
    {
        for (int i = 0; i < todoList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {todoList[i]}");
        }
    }
}

void SaveTasks()
{
    File.WriteAllLines(todoListFile, todoList);
}

void LoadTasks()
{
    if (File.Exists(todoListFile))
    {
        todoList = new List<string>(File.ReadAllLines(todoListFile));
    }
}
