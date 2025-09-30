import java.util.ArrayList;
import java.util.Random;
import java.util.Scanner;
import java.io.FileWriter;
import java.io.IOException;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class GuessingGame {

    // Variables for global game statistics
    static int totalGames = 0;
    static int totalWins = 0;
    static int totalAttempts = 0;

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        boolean playAgain = true;

        System.out.println("=== Welcome to the game: Guess the number ===");

        while (playAgain) {
            // Difficulty level selection
            int maxNumber = selectDifficulty(scanner);
            int maxAttempts = determineMaxAttempts(maxNumber);

            playGame(scanner, maxNumber, maxAttempts);

            System.out.print("Do you want to play again? (y/n): ");
            String response = scanner.nextLine().trim().toLowerCase();
            playAgain = response.equals("y");
        }

        // Show general statistics upon completion
        showStatistics();

        System.out.println("Thanks for playing!");
        scanner.close();
    }

    // Method for selecting difficulty level
    public static int selectDifficulty(Scanner scanner) {
        System.out.println("\nSelect a difficulty level:");
        System.out.println("1 - Easy (number between 1 and 50)");
        System.out.println("2 - Medium (number between 1 and 100)");
        System.out.println("3 - Difficult (number between 1 and 200)");

        int choice = 0;
        while (choice < 1 || choice > 3) {
            System.out.print("Enter your choice (1-3): ");
            try {
                choice = Integer.parseInt(scanner.nextLine());
                if (choice < 1 || choice > 3) {
                    System.out.println("Please enter a number between 1 and 3.");
                }
            } catch (NumberFormatException e) {
                System.out.println("Invalid entry. Please enter a number..");
            }
        }

        switch (choice) {
            case 1:
                System.out.println("You have selected Easy.");
                return 50;
            case 2:
                System.out.println("You have selected Medium.");
                return 100;
            case 3:
                System.out.println("You have selected Difficult.");
                return 200;
            default:
                return 100; // By default medium
        }
    }

    // Determines the maximum number of attempts by rank
    public static int determineMaxAttempts(int maxNumber) {
        if (maxNumber <= 50) {
            return 7;
        } else if (maxNumber <= 100) {
            return 5;
        } else {
            return 10;
        }
    }

    // Main method of the game
    public static void playGame(Scanner scanner, int maxNumber, int maxAttempts) {
        Random random = new Random();
        int secretNumber = random.nextInt(maxNumber) + 1;
        int attempts = 0;
        ArrayList<Integer> guessHistory = new ArrayList<>();
        boolean guessedCorrectly = false;

        System.out.println("\nI have chosen a number between 1 and " + maxNumber + ". Guess which one it is!");
        System.out.println("Have " + maxAttempts + " attempts to get it right.");

        while (attempts < maxAttempts) {
            System.out.print("Attempt #" + (attempts + 1) + ": ");

            int guess = readGuess(scanner, maxNumber);
            guessHistory.add(guess);
            attempts++;

            if (guess == secretNumber) {
                System.out.println("Correct! You won in " + attempts + " attempt!");
                guessedCorrectly = true;
                break;
            } else if (guess < secretNumber) {
                System.out.println("Too low. Try a higher number..");
            } else {
                System.out.println("Too high. Try a lower number..");
            }
        }

        if (!guessedCorrectly) {
            System.out.println("Sorry, you didn't guess the number. It was: " + secretNumber);
        }

        System.out.println("Your attempts: " + guessHistory);

        // Update global statistics
        totalGames++;
        if (guessedCorrectly) {
            totalWins++;
        }
        totalAttempts += attempts;

        // Save result to file
        saveGameResult(guessedCorrectly, attempts, secretNumber, guessHistory, maxNumber);
    }

    // Read a valid number from the user within the allowed range
    public static int readGuess(Scanner scanner, int maxNumber) {
        int guess = -1;
        while (guess < 1 || guess > maxNumber) {
            try {
                guess = Integer.parseInt(scanner.nextLine());
                if (guess < 1 || guess > maxNumber) {
                    System.out.println("Please enter a number between 1 and " + maxNumber + ".");
                    System.out.print("Attempt: ");
                }
            } catch (NumberFormatException e) {
                System.out.println("Invalid input. Please enter a number.");
                System.out.print("Attempt: ");
            }
        }
        return guess;
    }

    // Save game results to a file
    public static void saveGameResult(boolean win, int attempts, int number, ArrayList<Integer> history, int maxNumber) {
        try {
            FileWriter writer = new FileWriter("C:\\Users\\frank\\Documents\\Applied-Programming-Projects\\AppliedProgrammingGuessingGame\\Game_Result.txt", true);


            // Current date and time
            LocalDateTime now = LocalDateTime.now();
            DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");

            writer.write("Date: " + now.format(formatter) + "\n");
            writer.write("Level (max number): " + maxNumber + "\n");
            writer.write("Won: " + win + "\n");
            writer.write("Secret number: " + number + "\n");
            writer.write("Attempts: " + attempts + "\n");
            writer.write("Record: " + history + "\n");
            writer.write("------------\n");
            writer.close();
        } catch (IOException e) {
            System.out.println("Error saving result: " + e.getMessage());
        }
    }

    // Display overall statistics at the end of games
    public static void showStatistics() {
        System.out.println("\n--- Game Statistics ---");
        System.out.println("Games played: " + totalGames);
        System.out.println("Games won: " + totalWins);
        if (totalGames > 0) {
            double winRate = (double) totalWins / totalGames * 100;
            double avgAttempts = (double) totalAttempts / totalGames;
            System.out.printf("Winning percentage: %.2f%%\n", winRate);
            System.out.printf("Average attempts per game: %.2f\n", avgAttempts);
        }
        System.out.println("-----------------------------");
    }
}
