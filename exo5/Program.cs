
using System.Text.Json;
List<Task> myListTasks = new List<Task>();
while (true)
{ 
    Console.WriteLine("appuyez sur + pour ajouter une tache, ecrivez search pour chercher une tache, ecrivez sort pour triez la list par statut ou save pour save en Json votre liste");
   switch (Console.ReadLine())
   {   
       case "+" : 
           myListTasks.Add(NewTask());
           break;
       case "search" :
           var task = SearchTask();
           if (task != null)
           {
               ChangeStatOfTask(task);
           }
           else
           {
               Console.WriteLine("task inconnu");
           }
           break;
       case "sort":
           Triez(myListTasks);
           break;
       case "save" :
           SaveToJson(myListTasks);
           break;
       default:
           Console.WriteLine("sa commence le ___ est de sorie");
           break;
   } 
}

void ChangeStatOfTask(Task mytask)
{
    Console.WriteLine("quel statut voulez vous lui donner ? (progress) (done)");
    switch (Console.ReadLine())
    {
        case "progress" :
            Console.WriteLine($"{mytask.Name} en progression");
            mytask.Advanced = Task.TASKPROGRESS.progress;
            break;
        case "done" :
            Console.WriteLine($"{mytask.Name} fini");
            mytask.Advanced = Task.TASKPROGRESS.end;
            break;
        default:
            Console.WriteLine("statut inconnu");
            break;
    }
}


void SaveToJson(List<Task> tasks)
{
    try
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(tasks, options);
        string Name = "exoList.json";
        File.WriteAllText(Name, json);
        Console.WriteLine("saved json");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

Task? SearchTask()
{
    Console.WriteLine("quel tache chercher vous ?");
    string search = Console.ReadLine();
    foreach (var task in myListTasks)
    {
        if (task.Name == search)
        {
            Console.WriteLine($"vous avez une tache  {task.Name} trouver");
            return task;
        }
    }
    
    return null;
}

Task NewTask()
{
    Task task1 = new Task(Task.TASKPROGRESS.opentask);
    return task1;
}

void Triez(List<Task> tasks)
{
    tasks.Sort(delegate(Task task1, Task task2)
    {
        if (task1.Advanced == 0 && task2.Advanced == 0) return 0;
        else if (task1.Advanced != Task.TASKPROGRESS.progress && task2.Advanced == 0) return 1;
        else if (task1.Advanced != Task.TASKPROGRESS.end && task2.Advanced == 0) return -1;
        else return task1.Advanced.CompareTo(task2.Advanced);
    });
    foreach (Task xTask in tasks) Console.WriteLine(xTask.Name + " " + xTask.Advanced);
}

public class Task
{
    public Task(TASKPROGRESS initial)
    {
        NameTache();
        Advanced = initial;
        ChangeStatus();
    }

    private void NameTache()
    {
        Console.WriteLine("quel est le nom de la tache ?");
        Name = Console.ReadLine();
    }

    public string Name { get; set; }
    public TASKPROGRESS Advanced { get; set; }


    public enum TASKPROGRESS
    {
        opentask,
        progress,
        end
    }

    public void ChangeStatus()
    {
        switch (Advanced)
        {
            case TASKPROGRESS.opentask:
                Console.WriteLine($"{Name} is opentask");
                break;
            case TASKPROGRESS.progress:
                Console.WriteLine($"{Name} is progress");
                break;
            case TASKPROGRESS.end:
                Console.WriteLine($"{Name} is end");
                break;
            default:
                Console.WriteLine($"{Name} is unknown");
                break;
        }
    }
}