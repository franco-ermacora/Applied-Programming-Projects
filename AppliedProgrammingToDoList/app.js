// global variables
let currentFilter = 'all'; // Filtro inicial: todas las tareas
let tasks = JSON.parse(localStorage.getItem('tasks')) || [];

// Load tasks on startup
loadTasks();

// Event listeners
document.getElementById("addTaskBtn").addEventListener("click", addTask);
document.getElementById("taskInput").addEventListener("keypress", function (e) {
    if (e.key === "Enter") {
        addTask();
    }
});

document.getElementById("filterAll").addEventListener("click", () => setFilter('all'));
document.getElementById("filterCompleted").addEventListener("click", () => setFilter('completed'));
document.getElementById("filterPending").addEventListener("click", () => setFilter('pending'));
document.getElementById("toggleThemeBtn").addEventListener("click", toggleTheme);

// New funtions
function updateDateTime() {
    const datetimeElement = document.getElementById('datetime');
    if (datetimeElement) {
        const now = new Date();
        const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric'};
        const formattedDate = now.toLocaleDateString('en-EN', options);
        datetimeElement.textContent = formattedDate.charAt(0).toUpperCase() + formattedDate.slice(1);
    }
}

// Call new funtions
document.addEventListener('DOMContentLoaded', () => {
    updateDateTime();
    setInterval(updateDateTime, 30000); // Update every 30 seconds
});


// Principal funtions
function addTask() {
    const taskInput = document.getElementById("taskInput");
    const taskValue = taskInput.value.trim();

    if (taskValue) {
        const newTask = {
            text: taskValue,
            completed: false
        };

        tasks.push(newTask);
        taskInput.value = "";
        saveTasks();
        loadTasks();
    }
}

function saveTasks() {
    localStorage.setItem('tasks', JSON.stringify(tasks));
}

function loadTasks() {
    const taskList = document.getElementById("taskList");
    taskList.innerHTML = ""; // Clear list

    // Filter tasks based on the current filter
    const filteredTasks = tasks.filter(task => {
        if (currentFilter === 'all') return true;
        if (currentFilter === 'completed') return task.completed;
        if (currentFilter === 'pending') return !task.completed;
    });

    filteredTasks.forEach((task, index) => {
        const taskItem = document.createElement("li");

        taskItem.innerHTML = `
            <input type="checkbox" class="task-checkbox" ${task.completed ? "checked" : ""} />
            <span class="task-text">${task.text}</span>
            <button class="delete-btn">X</button>
        `;

        // Appearance animation
        taskItem.style.animation = "fadeIn 0.5s forwards";

        // Mark as completed
        taskItem.querySelector(".task-checkbox").addEventListener("change", function () {
            task.completed = !task.completed;
            saveTasks();
            loadTasks();
        });

        // Detele Ask
        taskItem.querySelector(".delete-btn").addEventListener("click", function () {
            tasks.splice(index, 1);
            saveTasks();
            loadTasks();
        });

        taskList.appendChild(taskItem);
    });
}

function setFilter(filter) {
    currentFilter = filter;
    loadTasks();
}

function toggleTheme() {
    document.body.classList.toggle('dark-mode');
    document.querySelector('.container').classList.toggle('dark-mode');

    const isDarkMode = document.body.classList.contains('dark-mode');
    document.getElementById('toggleThemeBtn').textContent = isDarkMode ? 'Light Mode' : 'Dark Mode';
}
