using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    internal class SQLDataAccess
    {
        public static void AddFlashcard(Flashcard card)
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                con.Execute("insert into flashcards(stackid,front,back) values (@StackID,@Front,@Back)", card);
            }
        }
        public static void AddStack(Stack stack)
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                con.Execute("insert into Stacks(Name) values (@Name)", stack);
            }
        }
        public static void AddStudySession(StudySession session)
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                con.Execute("insert into Study(StackID,Date,Score) values (@StackID,@Date,@Score)", session);
            }
        }
        public static List<Flashcard> LoadCards()
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                var output = con.Query<Flashcard>("select * from Flashcards", new DynamicParameters());
                return output.ToList();
            }
        }
        public static List<Flashcard> LoadCards(Stack stack)
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                var output = con.Query<Flashcard>("select * from Flashcards where StackID = @StackID", stack);
                return output.ToList();
            }
        }
        public static List<Flashcard> LoadCards(int stackId)
        {
            Stack stack = new Stack();
            stack.StackID = stackId;
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                var output = con.Query<Flashcard>("select * from Flashcards where StackID = @StackID", stack);
                return output.ToList();
            }
        }
        public static List<Stack> LoadStacks()
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                var output = con.Query<Stack>("select * from Stacks", new DynamicParameters());
                return output.ToList();
            }
        }
        public static List<Stack> LoadStack(Stack stack)
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                var output = con.Query<Stack>("select * from Stack where Name = @Name", stack);
                return output.ToList();
            }
        }
        public static List<Stack> LoadStack(int stackId)
        {
            Stack stack = new Stack();
            stack.StackID = stackId;
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                var output = con.Query<Stack>("select * from Stacks where StackID = @StackID", stack);
                return output.ToList();
            }
        }
        public static List<StudySession> LoadSessions()
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                var output = con.Query<StudySession>("select * from Study", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void UpdateCard(Flashcard card)
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                con.Execute("update Flashcards set StackID = @StackID, Front = @Front, Back = @Back where FlashcardID = @FlashcardID", card);
            }
        }
        public static void UpdateStack(Stack stack)
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                con.Execute("update Stacks set Name = @Name where StackID = @StackID", stack);
            }
        }
        public static void DeleteCard(Flashcard card)
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                con.Execute("delete from Flashcards where FlashcardID = @FlashcardID", card);
            }
        }
        public static void DeleteStack(Stack stack)
        {
            using (IDbConnection con = new SqlConnection(LoadConnectionString()))
            {
                con.Execute("delete from Stacks where StackID = @StackID", stack);
                con.Execute("delete from Flashcards where StackID = @StackID", stack);
                con.Execute("delete from Study where StackID = @StackID", stack);
            }
        }
 
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
