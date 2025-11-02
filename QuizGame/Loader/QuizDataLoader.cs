using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizGame.Models;
using System.Text.Json;
using System.IO;

namespace QuizGame.Loader
{
    public static class QuizDataLoader
    {
        public static string findLocalFolder;
        public static string dataFolder;
        public static Quiz LoadJSON(string dataPath)
        {
            string json = File.ReadAllText(dataPath);
            Quiz quiz = JsonSerializer.Deserialize<Quiz>(json);

            if(quiz != null)
            {
                quiz.Randomizer = new Random();
                foreach(Question q in quiz.Questions)
                {
                    q.CombineFullImagePath(q.ImagePath);
                }
            }

            return quiz;
        }


        public static List<Quiz> FindLocalQuizzes(out string statusMessage)
        {
            var quizzes = new List<Quiz>();

            findLocalFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            dataFolder = Path.Combine(findLocalFolder, "QuizData");

            //Create a new QuizData folder if not find
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
                statusMessage = "There is no quiz yet. Create one in Quiz Editor.";
                return quizzes;
            }

            var jsonFileNames = Directory.GetFiles(dataFolder, "*.json");

            //Check if there is any json file in the folder
            if(jsonFileNames.Length == 0)
            {
                statusMessage = "There is no quiz yet. Create one in Quiz Editor.";
                return quizzes;
            }

            //Found json files
            foreach(var file in jsonFileNames)
            {
                try
                {
                    string jsonString = File.ReadAllText(file);
                    var quiz = JsonSerializer.Deserialize<Quiz>(jsonString);

                    if(quiz != null & !string.IsNullOrWhiteSpace(quiz.Title))
                    {
                        quizzes.Add(quiz);
                    }
                }
                catch (Exception ex)
                {
                    statusMessage = "Something went wrong with loading JSON. Please try again.";
                }
            }

            statusMessage = quizzes.Count > 0
            ? $"Loaded {quizzes.Count} quizzes successfully."
            : "No valid quiz data found.";

            return quizzes;
        }
    }
}
