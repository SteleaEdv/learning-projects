import sys

tasks = []

def add_task(task):
    tasks.append(task)
    print(f"Task added: {task}")

def view_tasks():
    if not tasks:
        print("No tasks available.")
    else:
        print("To-Do List:")
        for i, task in enumerate(tasks, start=1):
            print(f"{i}. {task}")

def remove_task(task_number):
    try:
        task = tasks.pop(task_number - 1)
        print(f"Task removed: {task}")
    except IndexError:
        print("Invalid task number.")

def show_help():
    print("Commands:")
    print("  add <task>    - Add a new task")
    print("  view          - View all tasks")
    print("  remove <num>  - Remove a task by number")
    print("  help          - Show this help message")
    print("  exit          - Exit the program")

def main():
    print("Welcome to the To-Do List Manager!")
    show_help()

    while True:
        command = input("\nEnter a command: ").strip().split(" ", 1)
        
        if command[0] == "add":
            if len(command) > 1:
                add_task(command[1])
            else:
                print("Please provide a task to add.")
        elif command[0] == "view":
            view_tasks()
        elif command[0] == "remove":
            if len(command) > 1 and command[1].isdigit():
                remove_task(int(command[1]))
            else:
                print("Please provide a valid task number to remove.")
        elif command[0] == "help":
            show_help()
        elif command[0] == "exit":
            print("Goodbye!")
            sys.exit(0)
        else:
            print("Unknown command. Type 'help' for a list of commands.")

if __name__ == "__main__":
    main()
