using System;
using System.Collections.Generic;
using System.Diagnostics;

//Nathan Bonzo's To-Do List application.
Console.Clear();

List<(string checkOffBox, string taskName, DateTime dueDate)> todoList = new List<(string, string, DateTime)>(); // List that will contain the to-do list items.

bool exit = false; // Keep us in the while loop until user wants to exit.

LoadTasks();

const string todoListFile = "todo_list.txt";

DateTime dueDate = DateTime.Now; // Give me a baseline variable to input the date with

Console.WriteLine("Welcome to the To-Do List application! Please select an option below to get Started!");

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
    Console.Write("Enter the due date (YYYY-MM-DD): ");
    try
    {
        if (task != null) // Task could be null at times so this makes sure that if it is null, it is handled.
        {
            if (DateTime.TryParse(Console.ReadLine(), out DateTime due))
            {
                int proirityDate = todoList.FindIndex(date => date.dueDate > due);
                if (proirityDate == -1)
                {
                    todoList.Add(("[]", task, due));
                }
                else
                {
                    // Otherwise, insert the new task at the appropriate position
                    todoList.Insert(proirityDate, ("[]", task, due));
                }
                Console.WriteLine("Task added successfully!");
            }
        }
    }
    catch (Exception)
    {
        Console.WriteLine("Task failed to be added.");
    }
}

void RemoveTask()
{
    ViewTasks();

    Console.Write("Enter the number of which task you wish to remove: ");
    int removeIndex = Convert.ToInt32(Console.ReadLine());

    Debug.Assert(removeIndex >= 1 && removeIndex <= todoList.Count, "Invalid index for removing task."); // Check for a valid choice

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
// Prioritizes tasks so user can decide which one is most important
void Priority() 
{
    ViewTasks();

    Console.Write("Which task do you want to prioritize? ");
    int priorityChoice = Convert.ToInt32(Console.ReadLine());

    Debug.Assert(priorityChoice >= 1 && priorityChoice <= todoList.Count, "Invalid index for prioritizing task."); // Check for a valid choice

    if (priorityChoice >= 1 && priorityChoice <= todoList.Count)
    {
        Console.WriteLine("Priority goes to where the lower the number, the higher the priority. (E.g. 1 is the highest)");
        Console.Write("Enter the priority: ");
        int priorityOrder = Convert.ToInt32(Console.ReadLine());
        if (priorityOrder >= 1)
        {
            (string, string, DateTime) temp = todoList[priorityChoice - 1];
            todoList[priorityChoice - 1] = todoList[priorityOrder - 1];
            todoList[priorityOrder -1] = temp;
        }
    }
}

// Allows user to 'check off' a task or uncheck if they desire.
void CheckOff() 
{
    ViewTasks();

    Console.Write("Enter the number of the task you want to check off or uncheck: ");
    int checkOffItem = Convert.ToInt32(Console.ReadLine());
    if (checkOffItem >=1 && checkOffItem <= todoList.Count)
    {
        string status = todoList[checkOffItem - 1].checkOffBox;
        if (status == "[]")
        {
            todoList[checkOffItem - 1] = ("[X] ", todoList[checkOffItem - 1].taskName, todoList[checkOffItem-1].dueDate);
            Console.WriteLine("Task Checked Off.");
        }
        else if (status == "[X]")
        {
            todoList[checkOffItem - 1] = ("[X] ", todoList[checkOffItem - 1].taskName, todoList[checkOffItem-1].dueDate);
            Console.WriteLine("Task Unchecked.");
        }
    }
    else
        Console.WriteLine("Invalid input. Please enter a valid number.");
}

void ViewTasks()
{
    Console.WriteLine("Current Tasks:");
    if ( todoList.Count == 0) // If there are no tasks to display
    {
        Console.WriteLine("No tasks to display.");
    }
    else
    {
        for (int i = 0; i < todoList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {todoList[i].checkOffBox} {todoList[i].taskName} - Due Date: {todoList[i].dueDate:yyyy-MM-dd}");
        }
    }
}

// This method is to save all the tasks that were added to the todo_file so they can be saved and reused even after the program closes.
void SaveTasks()
{
    List<string> saveToFile = new List<string>();
    foreach (var task in todoList)
    {
        string line = $"{task.checkOffBox},{task.taskName},{task.dueDate:yyyy-MM-dd}";
        saveToFile.Add(line);

    }
    File.WriteAllLines(todoListFile, saveToFile);
}
// This loads any tasks that were saved into the todoList 
void LoadTasks()
{
   if (File.Exists(todoListFile))
    {
        todoList.Clear(); // Clear existing tasks before loading new ones
        foreach (string line in File.ReadAllLines(todoListFile))
        {
            string[] listParts = line.Split(',');
            DateTime dateSaved = DateTime.Parse(listParts[2]);
            if (listParts.Length == 3)
            {
                todoList.Add((listParts[0], listParts[1], dateSaved));
            }
        }
    }
}

