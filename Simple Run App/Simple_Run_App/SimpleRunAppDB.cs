using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Simple_Run_App
{
    public class SimpleRunAppDB
    {
        readonly SQLiteAsyncConnection database;

        public SimpleRunAppDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ExerciseTable>().Wait();
            database.CreateTableAsync<CoordinatesTable>().Wait();
        }

        public Task<List<ExerciseTable>> GetItemsAsync()
        {
            return database.Table<ExerciseTable>().ToListAsync();
        }

        public Task<List<ExerciseTable>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<ExerciseTable>("SELECT * FROM [ExerciseTable] WHERE [Done] = 0");
        }

        public Task<ExerciseTable> GetItemAsync(int id)
        {
            return database.Table<ExerciseTable>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }


        public Task<int> SaveItemAsync(ExerciseTable item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ExerciseTable item)
        {
            return database.DeleteAsync(item);
        }
    }
}
