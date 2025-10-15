# Overview

This project is a **Tic Tac Toe (Three in a Row)** game implemented in **C#** that can be played either through a **graphical user interface (GUI)** or directly in the **console**. 

The main objective of this project was to expand my knowledge and skills as a software engineer by exploring multiple aspects of C# development, including:
- Event-driven programming with **Windows Forms**
- File handling in C#
- Calling native Windows APIs with **P/Invoke**
- Object-oriented design and code organization
- User input validation
- Application control flow

After each game, the user is asked if they want to play again, and the software offers a choice between playing in the GUI or in the console. Each game result (who won, when it happened, and which interface was used) is automatically saved to a text file (`game_results.txt`) in the root directory of the project.

[Software Demo Video] https://vimeo.com/1127363361?fl=ip&fe=ec

# Development Environment

- **IDE:** Visual Studio 2022  
- **Language:** C# (.NET 9.0)  
- **UI Framework:** Windows Forms (WinForms)  
- **Operating System:** Windows 11  
- **External Libraries / APIs:**  
  - `kernel32.dll` via **P/Invoke** (for console allocation in WinForms apps)  
  - Standard .NET libraries: `System.IO`, `System.Runtime.InteropServices`, `System.Windows.Forms`

# Useful Websites

- 📚 **Windows Forms (GUI Programming)**  
  https://learn.microsoft.com/en-us/dotnet/desktop/winforms/  
  Official Microsoft documentation on how to use Windows Forms for building desktop applications.

- 🛠️ **System.Windows.Forms Reference**  
  https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms  
  Full API documentation for the Windows Forms namespace.

- 💾 **File I/O in C#**  
  https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-write-text-to-a-file  
  Guide on how to write and read text files in C#.

- 🔌 **Calling native Windows APIs using P/Invoke**  
  https://learn.microsoft.com/en-us/dotnet/standard/native-interop/pinvoke  
  Official Microsoft guide on Platform Invocation Services to call unmanaged functions.

- 🧠 **AllocConsole and FreeConsole (kernel32.dll)**  
  https://www.pinvoke.net/default.aspx/kernel32/AllocConsole.html  
  https://www.pinvoke.net/default.aspx/kernel32/FreeConsole.html  
  Community-sourced documentation and examples on how to allocate and free the console from a WinForms app.

- 💬 **MessageBox in Windows Forms**  
  https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.messagebox  
  Documentation and usage examples of MessageBox for dialogs.

- 💡 **Stack Overflow**  
  https://stackoverflow.com  
  Used for debugging and researching specific issues such as WinForms event handling, file paths, or nullability warnings in C#.

# Future Work

- Add a scoreboard to track cumulative wins between X and O.
- Implement an AI opponent with selectable difficulty levels.
- Improve visual design of the GUI (colors, layout, animations).
- Add support for custom player names.
- Export game results to CSV or JSON formats.
- Implement unit tests for game logic.
- Improve file handling with better path resolution and permissions checks.

