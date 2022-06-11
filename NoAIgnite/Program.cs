using NoAIgnite.Exceptions;
using System;

namespace NoAIgnite
{
    public class Program
    {
        // made public for easier testing of invalid data
        public const string ErrorMessage = "Invalid data";

        static void Main(string[] args)
        {
            string message = CreateMessage(args);

            Console.WriteLine(message);
        }

        public static string CreateMessage(string[] data)
        {
            string message = string.Empty;

            try
            {
                DateRangeCreator rangeCreator = new DateRangeCreator(data[0], data[1]);
                message = rangeCreator.Range();
            }
            catch (IndexOutOfRangeException)
            {
                message = ErrorMessage;
            }
            catch (NullReferenceException)
            {
                message = ErrorMessage;
            }
            catch (InvalidDataException)
            {
                message = ErrorMessage;
            }
            catch (ArgumentOutOfRangeException)
            {
                message = ErrorMessage;
            }

            return message;
        }
    }
}
